using System;
using System.Collections.Generic;
using System.Reactive.Disposables;

namespace reactivex
{
    public class SimpleObservable<T> : IObservable<T>
    {
        public T Value { get; private set; }
        
        public void Update(T value)
        {
            Value = value;
            NotifyObservers();
        }
        
        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);
            observer.OnNext(Value);
            return Disposable.Create(() => Unsubscribe(observer));
        }

        public void Unsubscribe(IObserver<T> observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }
        
        public SimpleObservable(T initialValue)
        {
            Value = initialValue;
            _observers = new List<IObserver<T>>();
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(Value);
            }
        }

        private List<IObserver<T>> _observers;
    }
}