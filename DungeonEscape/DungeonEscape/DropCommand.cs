using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class DropCommand : Command {
        public DropCommand() : base("drop", "Command for dropping a specified item.", "drop [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Drop(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
