using Autofac;
using sounders;
using Xunit;

namespace autofac.tests
{
    public class DependecyInjection
    {
        [Fact]
        public void ResolveDependenciesAutomatically()
        {
            var expectedSound = "beepchirpbuzz";
            var actualSound = string.Empty;
            
            var soundersAssembly = typeof(SoundAggregator).Assembly;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(soundersAssembly).AsImplementedInterfaces();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var soundAggregator = scope.Resolve<SoundAggregator>();
                actualSound = soundAggregator.MakeSound();
            }
            
            Assert.Equal(expectedSound, actualSound);
        }
    }
}