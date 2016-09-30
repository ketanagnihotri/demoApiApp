using System;
using System.Linq;
using kenobi.TripsExtension.TestDataProvider;
using kenobi.TripsExtension.TestDataProvider.DataProviders;
using Kenobi.Common.TenantConfiguration.DataProvider;
using Kenobi.TripsExtension.Core.BusinessLogic;
using Kenobi.TripsExtension.Core.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Moq;
using Tavisca.TravelNxt.TripDetailsService.Proxy;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class SwapTenantConfigurationProviderTests
    {
        private static ITestDataProvider _dataProvider;
        private ISwapTenantConfigurationProvider _swapTenantConfigurationProvider;
        private Mock<ITenantConfigurationProviderV1> _mockTenantConfigurationProvider;
        public SwapTenantConfigurationProviderTests()
        {
            WebhookContainer.SetLocatorWithContainer();
            _dataProvider = ServiceLocator.Current.GetInstance<ITestDataProvider>();
            SetMocking();
        }

        private void SetMocking()
        {
            _mockTenantConfigurationProvider = new Mock<ITenantConfigurationProviderV1>();
            _mockTenantConfigurationProvider.Setup(x =>
                    x.GetClassicSupplierIdByTenantIdPlatformSupplierId(It.IsAny<string>(), It.IsAny<string>(),
                        It.IsAny<string>()))
                .Returns(() => Int32.MaxValue);

            _mockTenantConfigurationProvider.Setup(
                x => x.GetClassicPosIdByPlatformPosId(It.IsAny<string>(), It.IsAny<string>())).Returns(() => Int32.MaxValue);

            _swapTenantConfigurationProvider = new SwapTenantConfigurationProvider(_mockTenantConfigurationProvider.Object);
        }

        [Fact]
        public void SwapTenantConfiguration_ValidInput_Success()
        {
            var request = _dataProvider.GetSwapTenantConfigurationRequest();
            var response =  _swapTenantConfigurationProvider.SwapTenantConfiguration(request, _dataProvider.GetTenantId());
            Assert.NotNull(response);
            Assert.NotNull(response.Pos);
            request = _dataProvider.GetSwapTenantConfigurationRequest();
            Assert.NotEqual(request.Pos.PosId, response.Pos.PosId);
        }

        [Fact]
        public void SwapTenantConfiguration_InValidTripFolderInput_Fail()
        {
            var response = _swapTenantConfigurationProvider.SwapTenantConfiguration(null, _dataProvider.GetTenantId());
            Assert.Null(response);
        }

        [Fact]
        public void SwapTenantConfiguration_InValidTripFolder_PosInput_Fail()
        {
            var request = _dataProvider.GetSwapTenantConfigurationRequest();
            request.Pos = null;
            var response = _swapTenantConfigurationProvider.SwapTenantConfiguration(request, _dataProvider.GetTenantId());
            Assert.Null(response.Pos);
        }

        [Fact]
        public void SwapTenantConfiguration_InValidTripFolder_FareSourceInput_Fail()
        {
            var request = _dataProvider.GetSwapTenantConfigurationRequest();
            var hoteProduct = request.Products.FirstOrDefault(x => x.GetType() == typeof (HotelTripProduct)) as HotelTripProduct;
            if (hoteProduct != null) hoteProduct.HotelItinerary.HotelFareSource = null;
            var response = _swapTenantConfigurationProvider.SwapTenantConfiguration(request, _dataProvider.GetTenantId());
            var hoteProductResponse = response.Products.FirstOrDefault(x => x.GetType() == typeof(HotelTripProduct)) as HotelTripProduct;
            Assert.NotNull(hoteProductResponse);
            Assert.Null(hoteProductResponse.HotelItinerary.HotelFareSource);
        }
    }
}
