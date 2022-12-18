using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class Notification {
        public string Name { get; set; }
        public Object Object { get; set; }
        public Dictionary<string, Object> Info { get; set; }

        public Notification(string name = "Notification", Object obj = null, Dictionary<string, Object> info = null) {
            Name = name;
            Object = obj;
            Info = info;
        }
    }

    public class NotificationCenter {
        private Dictionary<string, EventContainer> observers;
        private static NotificationCenter instance;

        public static NotificationCenter Instance {
            get {
                if (instance == null) instance = new NotificationCenter();
                return instance;
            }
        }

        public NotificationCenter() {
            observers = new Dictionary<string, EventContainer>();
        }

        private class EventContainer {
            private event Action<Notification> Observer;

            public EventContainer() { }

            public void AddObserver(Action<Notification> observer) {
                Observer += observer;
            }

            public void RemoveObserver(Action<Notification> observer) {
                Observer -= observer;
            }

            public void SendNotification(Notification notification) {
                Observer(notification);
            }

            public bool IsEmpty() {
                return Observer == null;
            }
        }

        public void AddObserver(string name, Action<Notification> observer) {
            if (!observers.ContainsKey(name)) observers[name] = new EventContainer();
            observers[name].AddObserver(observer);
        }

        public void RemoveObserver(string name, Action<Notification> observer) {
            if (observers.ContainsKey(name)) {
                observers[name].RemoveObserver(observer);
                if (observers[name].IsEmpty()) observers.Remove(name);
            }
        }

        public void PostNotification(Notification notification) {
            if (observers.ContainsKey(notification.Name)) observers[notification.Name].SendNotification(notification);
        }
    }
}
