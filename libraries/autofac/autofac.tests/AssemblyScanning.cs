using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Autofac;
using mathers;

namespace autofac.tests
{
    public class AssemblyScanning
    {
        [Fact]
        public void ResolveAllImplementationsOfAnInterfaceInAnAssembly()
        {
            var a = 5.0f;
            var b = 2.0f;
            var expectedAnswers = new[] {7.0f, 3.0f, 10.0f, 2.5f}; //Adder, Subtractor, Multiplier, Divider
            var answers = new List<float>();
            
            var mathersAssembly = typeof(Mather).Assembly;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(mathersAssembly).AsImplementedInterfaces();
            var container = builder.Build();
            
            using (var scope = container.BeginLifetimeScope())
            {
                var allMathers = scope.Resolve<IEnumerable<Mather>>();
                foreach (var mather in allMathers)
                {
                    var result = mather.Operate(a, b);
                    answers.Add(result);
                }
            }

            var allCorrect = answers.All(answer => expectedAnswers.Any(expected => Math.Abs(answer - expected) < float.Epsilon));
            Assert.True(allCorrect);
        }
    }
}
