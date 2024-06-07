using System;
using CodeBase.EventBus.Signals;
using CodeBase.Services;

namespace CodeBase.EventBus
{
    public interface IEventBus : IService
    {
        void Subscribe<TSignal>(Action<TSignal> callback, int priority = 0) where TSignal : class, ISignal;
        void Invoke<TSignal>(TSignal signal) where TSignal : class, ISignal;
        void Unsubscribe<TSignal>(Action<TSignal> callback) where TSignal : class, ISignal;
    }
}