using Ship.Shared.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Ship.Game.Event
{
    public class EventManager : MonoBehaviour
    {

        public static EventManager instance;

        public delegate void EventHandlerFunction(EventObject eventObject);
        private Dictionary<int, Dictionary<EEventType, EventHandlerFunction>> listeners;
        private Queue<EventObject> events;

        private void Awake()
        {
            Log.debug("EventManager.Awake");
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Log.error("Instance already exists, destroying object!");
                Destroy(this);
            }

            listeners = new Dictionary<int, Dictionary<EEventType, EventHandlerFunction>>();
            events = new Queue<EventObject>();
        }

        void Update()
        {
            while(events.Count != 0)
            {
                EventObject evtObj = events.Dequeue();
                EEventType eventType = evtObj.GetEventType();
                foreach(var entry in listeners.Values)
                {
                    if(entry.ContainsKey(eventType))
                    {
                        entry[eventType](evtObj);
                    }
                }
            }
        }

        public void AddListener(Object obj, EEventType eventType, EventHandlerFunction eventHandlerFunction)
        {
            int hashCode = obj.GetHashCode();
            if (!listeners.ContainsKey(hashCode))
            {
                listeners.Add(hashCode, new Dictionary<EEventType, EventHandlerFunction>());
            }

            listeners[hashCode].Add(eventType, eventHandlerFunction);
        }

        public void RemoveListener(Object obj, EEventType eventType, EventHandlerFunction eventHandlerFunction)
        {
            int hashCode = obj.GetHashCode();
            if (listeners.ContainsKey(hashCode) && listeners[hashCode].ContainsKey(eventType))
            {
                listeners[hashCode].Remove(eventType);
            }
        }

        public void RemoveAllListeners(Object obj)
        {
            int hashCode = obj.GetHashCode();
            if (listeners.ContainsKey(hashCode))
            {
                listeners.Remove(hashCode);
            }
        }

        public void PushEvent(EventObject eventObject)
        {
            events.Enqueue(eventObject);
        }
    }
}