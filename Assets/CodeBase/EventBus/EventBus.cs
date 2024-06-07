using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.EventBus.Signals;
using UnityEngine;

namespace CodeBase.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<CallbackWithPriority>> _signalCallbacks = new Dictionary<Type, List<CallbackWithPriority>>();

        public void Subscribe<TSignal>(Action<TSignal> callback, int priority = 0) where TSignal : class, ISignal
        {
            Type signalType = typeof(TSignal);

            if (_signalCallbacks.TryGetValue(signalType, value: out List<CallbackWithPriority> signalCallback))
                signalCallback.Add(new CallbackWithPriority(priority, callback));
            else
            {
                _signalCallbacks.Add(typeof(TSignal), new List<CallbackWithPriority>()
                { 
                    new CallbackWithPriority(priority, callback)
                });
            }

            _signalCallbacks[signalType] = _signalCallbacks[signalType].OrderByDescending(x => x.Priority).ToList();
        } 

        public void Invoke<TSignal>(TSignal signal) where TSignal : class, ISignal
        {
            Type key = typeof(TSignal);
            if (!_signalCallbacks.TryGetValue(key, out List<CallbackWithPriority> signalCallback)) 
                return;
            foreach (Action<TSignal> callback in signalCallback.Select(obj => obj.Callback as Action<TSignal>)) 
                callback?.Invoke(signal);
        } 

        public void Unsubscribe<TSignal>(Action<TSignal> callback) where TSignal : class, ISignal
        {
            Type key = typeof(TSignal);
            if (_signalCallbacks.ContainsKey(key))
            {
                CallbackWithPriority callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null) 
                    _signalCallbacks[key].Remove(callbackToDelete);
            }
            else
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
        }
    }
}