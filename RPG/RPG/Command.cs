using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public abstract class Command : Object {
        protected string[] args;
        private string usage;

        public string[] Args {
            get { return args; }
            set { args = value; }
        }

        public string Usage {
            get { return usage; }
        }

        public Command(string name, string desc, string usage) : base(name, desc) {
            this.usage = usage;
        }

        public string Concat() {
            return string.Join(" ", args, 1, args.Length - 1);
        }

        public override string ToString() {
            return $"| {Name, -12} | {Desc, -64} | {Usage, -30} |";
        }

        public abstract bool Execute(Player player);
    }
}