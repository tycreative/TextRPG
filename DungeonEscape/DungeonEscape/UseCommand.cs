using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class UseCommand : Command {
        public UseCommand() : base("use", "Command for using a specified item.", "use [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 1) player.State.Use(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
