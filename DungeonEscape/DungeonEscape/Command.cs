using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public abstract class Command : Entity {
        protected string[] args;
        private string usage;

        public string[] Args { get { return args; } set { args = value; } }
        public string Usage { get { return usage; } }
        // Format for processing
        public string FormatArgs { get { return string.Join(" ", args, 1, args.Length - 1); } }

        public Command(string name, string desc, string usage) : base(name, desc) {
            this.usage = usage;
        }

        // Format for display
        public override string ToString() {
            return $"{usage}: {desc}";
        }

        public abstract bool Execute(Player player);
    }
}
