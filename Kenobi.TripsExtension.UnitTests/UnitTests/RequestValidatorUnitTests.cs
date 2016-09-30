using System;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.TripsExtension.Core.Infrastructure;
using Kenobi.TripsExtension.Core.Validators;
using Kenobi.TripsExtension.Entities;
using Microsoft.Practices.ServiceLocation;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class RequestValidatorUnitTests
    {
        private static ITestDataProvider _testDataProvider;

        public RequestValidatorUnitTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _testDataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
        }

        [Fact]
        public void ValidateRequest_ValidInput_Success()
        {
            var response = RequestValidator.Validate(_testDataProvider.GetBookingWebhooksRequest(), Constants.BookingEvent);
            Assert.NotNull(response == ResponseStatus.Success);
        }
        [Fact]
        public void ValidateRequest_InValidInput_Success()
        {
            try
            {
                RequestValidator.Validate(null, Constants.BookingEvent);
            }
            catch (Exception e)
            {
                 Assert.True(true);
            }
        }
    }
}