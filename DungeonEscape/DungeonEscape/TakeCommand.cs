using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class TakeCommand : Command {
        public TakeCommand() : base("take", "Command for taking a specified item.", "take [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Take(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
