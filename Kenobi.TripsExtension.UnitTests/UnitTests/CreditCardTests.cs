using System;
using System.Linq;
using kenobi.TripsExtension.TestDataProvider;
using Kenobi.TripsExtension.TripsRepository.Model;
using Kenobi.TripsExtension.TripsRepository.Repository;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class CreditCardTests
    {
        [Fact]
        public void TestCreditCard_AnyNumber_Success()
        {
            WebhookContainer.SetLocatorWithContainer();
            SupplierCard creditCard = new SupplierCard();
            TripProvider provider = new TripProvider("Demo");
            creditCard.Number = "5567***7310";

            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "4444XXXXXXXX1111";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "444444XXXXXXXX11";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "4444XXXXXXXXXX11";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "44XXXXXXXXXXXX11";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "444444XXXXXXXXXX";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "XXXXXXXXXXXXX111";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));

            creditCard.Number = "XXXXXXXXXXXXXXXX";
            creditCard = provider.UpdateSupplierCard(creditCard);
            Assert.NotNull(creditCard);
            Assert.True(creditCard.Number.Substring(0, 6).All(char.IsDigit));
            Assert.True(creditCard.Number.Substring(creditCard.Number.Length - 4).All(char.IsDigit));
        }
    }
}
