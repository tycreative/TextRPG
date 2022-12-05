using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class Notification {
        public string Name {
            get; set;
        }

        public Object Object {
            get; set;
        }

        public Dictionary<string, Object> Info {
            get; set;
        }

        public Notification(string name = "Notification", Object obj = null, Dictionary<string, Object> info = null) {
            Name = name;
            Object = obj;
            Info = info;
        }
    }
}
