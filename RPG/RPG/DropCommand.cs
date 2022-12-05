using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class DropCommand : Command {
        public DropCommand() : base("drop", "Command for dropping a specified item.", "drop [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Drop(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}