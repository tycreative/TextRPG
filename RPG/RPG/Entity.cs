using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public abstract class Object {
        private string name = "Object";
        private string desc = "An unknown object.";

        public string Name { get {return name;} }
        public string Desc { get {return desc;} }

        public Object(string name, string desc) {
            this.name = name;
            this.desc = desc;
        }

        public override string ToString() {
            return Name + ": " + Desc;
        }
    }
}
