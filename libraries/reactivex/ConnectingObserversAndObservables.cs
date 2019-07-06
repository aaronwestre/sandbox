using Xunit;

namespace reactivex
{
    public class ConnectingObserversAndObservables
    {
        [Fact]
        public void Observer_has_default_value()
        {
            var observer = new SimpleObserver<int>(0);
            Assert.Equal(0, observer.Value);
        }

        [Fact]
        public void Observable_has_default_value()
        {
            var observable = new SimpleObservable<int>(1);
            Assert.Equal(1, observable.Value);
        }

        [Fact]
        public void Observer_gets_updated_upon_subscription()
        {
            var observable = new SimpleObservable<int>(1);
            var observer = new SimpleObserver<int>(0);
            observable.Subscribe(observer);
            Assert.Equal(1, observer.Value);
        }

        [Fact]
        public void Observer_gets_updated_when_observable_is_updated()
        {
            var observable = new SimpleObservable<int>(1);
            var observer = new SimpleObserver<int>(0);
            observable.Subscribe(observer);
            observable.Update(2);
            observable.Update(3);
            observable.Update(4);
            Assert.Equal(4, observer.Value);
        }

        [Fact]
        public void Observer_receives_no_updates_after_unsubscribing()
        {
            var observable = new SimpleObservable<int>(1);
            var observer = new SimpleObserver<int>(0);
            var subscription = observable.Subscribe(observer);
            observable.Update(2);
            observable.Update(3);
            subscription.Dispose();
            observable.Update(4);
            Assert.Equal(3, observer.Value);
        }
    }
}
