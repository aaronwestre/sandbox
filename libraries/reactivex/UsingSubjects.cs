using System.Reactive;
using System.Reactive.Subjects;
using Xunit;

namespace reactivex
{
    public class UsingSubjects
    {
        [Fact]
        public void Pushing_manually_using_Subject()
        {
            var numbers = new Subject<int>();
            var observedValue = 0;
            var observer = Observer.Create<int>(state => observedValue = state);
            numbers.Subscribe(observer);
            numbers.OnNext(1);
            Assert.Equal(1, observedValue);
            numbers.OnNext(2);
            Assert.Equal(2, observedValue);
        }

        [Fact]
        public void Ensuring_all_values_are_received_using_ReplaySubject()
        {
            var numbers = new ReplaySubject<int>();
            var observedValue = 0;
            var observer = Observer.Create<int>(state => observedValue = state);
            numbers.OnNext(1);
            Assert.Equal(0, observedValue);
            numbers.OnNext(2);
            numbers.Subscribe(observer);
            Assert.Equal(2, observedValue);
        }

        [Fact]
        public void Ensuring_that_a_default_is_received_using_BehaviorSubject()
        {
            var numbers = new BehaviorSubject<int>(3);
            var observedValue = 0;
            var observer = Observer.Create<int>(state => observedValue = state);
            numbers.Subscribe(observer);
            Assert.Equal(3, observedValue);
        }
    }
}