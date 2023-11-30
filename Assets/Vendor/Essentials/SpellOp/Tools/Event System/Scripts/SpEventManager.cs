using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellOp.Tools.Events
{
    public struct SpGameEvent
    {
        public string EventName { get; private set; }

        public SpGameEvent(string eventName)
        {
            EventName = eventName;
        }

        private static SpGameEvent _e;

        public static void Trigger(string eventName)
        {
            _e.EventName = eventName;
            SpEventManager.TriggerEvent(_e);
        }
    }
    
    public static class SpEventManager
    {
        private static Dictionary<Type, List<ISpEventListenerBase>> _eventListeners;

        static SpEventManager()
        {
            _eventListeners = new Dictionary<Type, List<ISpEventListenerBase>>();
        }

        public static void AddListener<SpEvent>(ISpEventListener<SpEvent> listener) where SpEvent:struct
        {
            var eventType = typeof(SpEvent);
            if (!_eventListeners.ContainsKey(eventType))
            {
                _eventListeners.Add(eventType,new List<ISpEventListenerBase>());
            }

            if (!SubscriptionExists(eventType, listener))
            {
                _eventListeners[eventType].Add(listener);
            }

        }


        public static void RemoveListener<SpEvent>(ISpEventListener<SpEvent> listener) where SpEvent:struct
        {
            var eventType = typeof(SpEvent);
            if(!_eventListeners.ContainsKey(eventType)) return;

            var listenersList = _eventListeners[eventType];

            var maxIndex = listenersList.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                if(i >= listenersList.Count) continue;
                
                if (listenersList[i] == listener)
                {
                    listenersList.Remove(listenersList[i]);

                    if (listenersList.Count == 0)
                    {
                        _eventListeners.Remove(eventType);
                    }
                    return;
                }
            }
            
        }
        
        


        private static bool SubscriptionExists(Type type, ISpEventListenerBase receiver)
        {
            return _eventListeners.TryGetValue(type, out var receivers) && receivers.Any(t => t == receiver);
        }


        public static void TriggerEvent<SpEvent>(SpEvent e) where SpEvent : struct
        {
            var eventType = typeof(SpEvent);
            if(!_eventListeners.TryGetValue(eventType,out var receivers)) return;

            var maxIndex = receivers.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                if (i >= receivers.Count) continue;
                
                (receivers[i] as ISpEventListener<SpEvent>)!.OnSpEvent(e);
            }
           
        }

    }
    
}