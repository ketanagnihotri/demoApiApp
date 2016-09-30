using System;
using System.Collections.Generic;
using System.Linq;
using Kenobi.TripsExtension.TripsRepository.Util;
using proxy = Tavisca.TravelNxt.TripDetailsService.Proxy;

namespace Kenobi.TripsExtension.TripsRepository.Parsers
{
    internal static class PaymentParser
    {
        static readonly string DisplayCurrency = Config.DisplayCurrency;
        internal static List<proxy.Payment> Parse(List<Model.BookingTransaction> transactions, List<Model.Voucher> vouchers, IDictionary<int, string> dictProductRphBookingidMapping)
        {

            List<proxy.Payment> lstPayment = new List<proxy.Payment>();

          //  List<Model.Card> distinctCardList = transactions.GroupBy(x => x.Card.Number).
          //                                    Select(y => new { y.Key, y.transactionId });
            Dictionary<string, List<Model.BookingTransaction>> dictCardTransactions = new Dictionary<string, List<Model.BookingTransaction>>();

            foreach (var bookingTransaction in transactions)
            {

                if (dictCardTransactions.ContainsKey(bookingTransaction.Card.Number))
                    dictCardTransactions[bookingTransaction.Card.Number].Add(bookingTransaction);
                else
                    dictCardTransactions.Add(bookingTransaction.Card.Number, new List<Model.BookingTransaction> { bookingTransaction });
            }
            int rph = 0;
            foreach (var tran in dictCardTransactions)
            {
                DateTime expiryDate;
                var chargeTransaction = GetChargeTransactions(tran.Value, dictProductRphBookingidMapping);
                proxy.Payment payment = new proxy.CreditCardPayment
                {
                    ChargeTransactions = chargeTransaction,
                    Amount = chargeTransaction != null && chargeTransaction.Count > 0
                        ? new proxy.Money
                        {
                            Amount = chargeTransaction.Sum(chargeAmount => chargeAmount.ChargedAmount.Amount),
                            Currency = chargeTransaction.FirstOrDefault().ChargedAmount.Currency,
                            DisplayCurrency = DisplayCurrency
                        }
                        : null,
                    BillingAddress = new proxy.Address // required for trips engine as null check is missing in trips
                    {
                        City = new proxy.City()
                    },
                    CardType = proxy.CreditCardType.Personal,
                    NameOnCard = tran.Value[0].Card.NameOnCard,
                    CardMake = new proxy.CreditCardMake
                    {
                        Code = tran.Value[0].Card.IssuedBy,
                        Name = tran.Value[0].Card.IssuedBy
                    },
                    ExpiryMonthYear =
                        DateTime.TryParse(tran.Value[0].Card.ExpiryDate, out expiryDate) ? expiryDate : DateTime.Now,
                    Number = tran.Value[0].Card.Number,
                    Rph = rph,
                };
                rph++;
                lstPayment.Add(payment);
            }

            //foreach (Model.Card card in distinctCardList)
            //{
            //    DateTime expiryDate;
            //    var chargeTransaction = GetChargeTransactions(transactions, dictProductRphBookingidMapping);
            //    proxy.Payment payment = new proxy.CreditCardPayment
            //    {
            //        ChargeTransactions = chargeTransaction,
            //        Amount = chargeTransaction != null && chargeTransaction.Count > 0 ?
            //        new proxy.Money { Amount = chargeTransaction.Sum(chargeAmount => chargeAmount.ChargedAmount.Amount), Currency = chargeTransaction.FirstOrDefault().ChargedAmount.Currency, DisplayCurrency = DisplayCurrency }
            //        : null,
            //        BillingAddress = new proxy.Address  // required for trips engine as null check is missing in trips
            //        {
            //            City = new proxy.City()
            //        },
            //        CardType = new proxy.CreditCardType(),
            //        NameOnCard = card.NameOnCard,
            //        CardMake = new proxy.CreditCardMake
            //        {
            //            Code = card.IssuedBy,
            //            Name = card.IssuedBy
            //        },
            //        ExpiryMonthYear = DateTime.TryParse(card.ExpiryDate, out expiryDate) ? expiryDate : DateTime.Now,
            //        Number = card.Number,
            //        Rph = rph,
            //    };
            //    rph++;
            //    lstPayment.Add(payment);
            //}

            return lstPayment;
        }

        private static List<proxy.ChargeTransaction> GetChargeTransactions(List<Model.BookingTransaction> transactions, IDictionary<int, string> dictProductRphBookingidMapping)
        {
            return transactions.ConvertAll(modelTransaction => new proxy.ChargeTransaction
            {
                TransactionId = modelTransaction.TransactionId,
                ChargedAmount = new proxy.Money { Amount = modelTransaction.Amount, Currency = modelTransaction.Currency, DisplayAmount = modelTransaction.Amount, DisplayCurrency = DisplayCurrency },
                ChargeStatus = string.IsNullOrEmpty(modelTransaction.Status) ? proxy.ChargeStatus.Initialized : GetChargeStatus(modelTransaction.Status),
                ProviderTransactionId = modelTransaction.ProviderTransactionId,
                ChargeBreakups = modelTransaction.PaymentBreakup != null ? GetChargeBreakups(modelTransaction.PaymentBreakup, dictProductRphBookingidMapping, modelTransaction.Currency) : new List<proxy.ChargeBreakup>(),
            }).ToList();
        }

        private static proxy.ChargeStatus GetChargeStatus(string status)
        {
            proxy.ChargeStatus chargeStatus;

            if (status.Equals("Authorized", StringComparison.InvariantCultureIgnoreCase))
                chargeStatus = proxy.ChargeStatus.Authorized;
            else if (status.Equals("Settled", StringComparison.InvariantCultureIgnoreCase))
                chargeStatus = proxy.ChargeStatus.Charged;
            else if (status.Equals("Void", StringComparison.InvariantCultureIgnoreCase))
                chargeStatus = proxy.ChargeStatus.Cancelled;
            else if (status.Equals("Refunded", StringComparison.InvariantCultureIgnoreCase))
                chargeStatus = proxy.ChargeStatus.Refunded;
            else
                chargeStatus = proxy.ChargeStatus.Initialized;

            return chargeStatus;
        }

        private static List<proxy.ChargeBreakup> GetChargeBreakups(List<Model.PaymentBreakup> paymentBreakup, IDictionary<int, string> dictProductRphBookingidMapping, string currency)
        {
            List<proxy.ChargeBreakup> lstChargeBreakup = new List<proxy.ChargeBreakup>();
            foreach (var modelpaymentBreakup in paymentBreakup)
            {
                proxy.ChargeBreakup proxyBreakup = new proxy.ChargeBreakup
                {
                    ChargedAmount = new proxy.Money { Amount = modelpaymentBreakup.Amount, Currency = currency, DisplayAmount = modelpaymentBreakup.Amount, DisplayCurrency = DisplayCurrency },
                    ProductRph = dictProductRphBookingidMapping.FirstOrDefault(x => x.Value == modelpaymentBreakup.BookingId).Key// GetProductRphForBooking(dictProductRphAdditionInfo,modelpaymentBreakup.BookingId),
                };
                lstChargeBreakup.Add(proxyBreakup);
            }
            return lstChargeBreakup;
        }
    }
}
