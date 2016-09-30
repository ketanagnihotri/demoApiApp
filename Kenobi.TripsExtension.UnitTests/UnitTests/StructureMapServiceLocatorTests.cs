using System;
using Kenobi.TripsExtension.Core.DependencyInjection;
using Microsoft.Practices.ServiceLocation;
using Tavisca.Frameworks.Logging;
using Xunit;

namespace Kenobi.TripsExtension.UnitTests.UnitTests
{
    public class StructureMapServiceLocatorTests
    {
        private readonly StructureMapServiceLocator _structureMapServiceLocator;

        public StructureMapServiceLocatorTests()
        {
            _structureMapServiceLocator = new StructureMapServiceLocator(StructureMapContainerProvider.GetContainer());
            ServiceLocator.SetLocatorProvider(() => _structureMapServiceLocator);
        }

        [Fact]
        public void GetAllInstance_Success()
        {
            var instance = _structureMapServiceLocator.GetAllInstances<ILogger>();
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetAllInstanceByType_Success()
        {
            var instance = _structureMapServiceLocator.GetAllInstances(typeof (ILogger));
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetInstance_Success()
        {
            var instance = _structureMapServiceLocator.GetInstance<ILogger>();
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetInstanceByType_Success()
        {
            var instance = _structureMapServiceLocator.GetInstance(typeof (ILogger));
            Assert.NotNull(instance);
        }

        [Fact]
        public void GetInstanceException_Fail()
        {
            Assert.ThrowsAny<Exception>(_structureMapServiceLocator.GetInstance<IDependancyException>);
        }
    }
}