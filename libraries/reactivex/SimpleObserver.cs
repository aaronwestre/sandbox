using System;

namespace reactivex
{
    public class SimpleObserver<T> : IObserver<T>
    {
        public T Value { get; private set; }
        
        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(T value)
        {
            Value = value;
        }

        public SimpleObserver(T initialValue)
        {
            Value = initialValue;
        }
    }
}