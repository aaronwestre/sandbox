using System;
using System.Reactive;
using System.Reactive.Linq;
using Microsoft.Reactive.Testing;
using Xunit;

namespace reactivex
{
    public class UsingObservableFactoryMethods
    {
        [Fact]
        public void Using_Generate_to_create_a_counter()
        {
            var counter = Observable.Generate(
                1,
                state => state < 1000,
                state => state + 1,
                state => state);
            var observedValue = 0;
            var observer = Observer.Create<int>(state => observedValue = state);
            counter.Subscribe(observer);
            Assert.Equal(999, observedValue);
        }

        [Fact]
        public void Using_Interval_to_create_a_periodic_signal()
        {
            var scheduler = new TestScheduler();
            var interval = Observable
                .Interval(TimeSpan.FromSeconds(1.0), scheduler)
                .Take(5);
            var observedValue = 0;
            var observer = Observer.Create<long>(tick => observedValue = (int)tick);
            interval.Subscribe(observer);
            scheduler.Start();
            Assert.Equal(4, observedValue);
        }
    }
}