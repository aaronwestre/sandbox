using System.Reactive;
using System.Reactive.Subjects;
using Xunit;

namespace reactivex
{
    public class ChainingObservables
    {
        [Fact]
        public void Chain_observables_and_observers()
        {
            var instigator = new Subject<int>();
            var relayer = new Subject<int>();
            var receiver = new Subject<int>();
            var instigatorToRelayer = instigator.Subscribe(relayer);
            var relayerToReceiver = relayer.Subscribe(receiver);
            var observedValue = 0;
            var observer = Observer.Create((int nextValue) => observedValue = nextValue);
            var subscription = receiver.Subscribe(observer);
            instigator.OnNext(996);
            instigator.OnNext(997);
            instigator.OnNext(998);
            instigator.OnNext(999);
            Assert.Equal(999, observedValue);
        }
        
        [Fact]
        public void Chain_subjects_and_observers()
        {
            var instigator = new Subject<int>();
            var relayer = new Subject<int>();
            var receiver = new Subject<int>();
            var relayerObserver = Observer.Create((int nextValue) => relayer.OnNext(nextValue));
            var instigatorToRelayer = instigator.Subscribe(relayerObserver);
            var receiverObserver = Observer.Create((int nextValue) => receiver.OnNext(nextValue));
            var relayerToReceiver = relayer.Subscribe(receiverObserver);
            var observedValue = 0;
            var observer = Observer.Create((int nextValue) => observedValue = nextValue);
            var subscription = receiver.Subscribe(observer);
            instigator.OnNext(996);
            instigator.OnNext(997);
            instigator.OnNext(998);
            instigator.OnNext(999);
            Assert.Equal(999, observedValue);
        }
    }
}