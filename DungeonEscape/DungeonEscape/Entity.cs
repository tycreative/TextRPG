using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    // Basic class that holds name and description of object
    public abstract class Entity {
        protected string name;
        protected string desc;

        public string Name { get { return name; } }
        public string Desc { get { return desc; } }

        protected Entity(string name, string desc) {
            this.name = name;
            this.desc = desc;
        }
    }
}
