using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
        public void Driving_an_observable_explicitly()
        {
            var broadcaster = new Subject<int>();
            var observedValue = 0;
            var observer = Observer.Create((int nextValue) => observedValue = nextValue);
            var subscription = broadcaster.AsObservable().Subscribe(observer);
            broadcaster.OnNext(996);
            broadcaster.OnNext(997);
            broadcaster.OnNext(998);
            broadcaster.OnNext(999);
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