using System;
using Xunit;

namespace StructureMapFuncDemo.Tests
{
    public class RegistryContainerTests
    {
        [Fact]
        public void ShouldResolveISimpleRegisterService()
        {
            var container = new RegistryContainer();
            container.Configure();

            var service = container.GetInstance<ISimpleRegisterService>();

            Assert.NotNull(service);
            Assert.IsType<SimpleRegisterService>(service);
        }

        [Fact]        
        public void ShouldNotResolveIComplexRegisterService()
        {
            var container = new RegistryContainer();
            container.Configure();            

            Assert.Throws<StructureMap.StructureMapBuildPlanException>(() => container.GetInstance<IComplexRegisterService>());
        }

        [Fact]
        public void ShouldResolveModifiedComplexRegisterService()
        {
            var container = new RegistryContainer();
            container.Configure();

            var service = container.GetInstance<IModifiedComplexRegisterService>();

            Assert.NotNull(service);
            Assert.IsType<ModifiedComplexRegisterService>(service);
        }

        [Fact]
        public void ShouldPassParametersToComplexRegisterProfiler()
        {
            var container = new RegistryContainer();
            container.Configure();

            var service = container.GetInstance<IModifiedComplexRegisterService>();

            var expectedJob = new Job {Id = 10001, Name = "Software Developer"};
            var expectedCandidate = new Candidate {Id = 20001, Name = "Mr Gru"};

            var profiler = service.CreateProfiler(expectedJob, expectedCandidate);

            Assert.Same(expectedJob, profiler.Job);
            Assert.Same(expectedCandidate, profiler.Candidate);
        }
    }
}
