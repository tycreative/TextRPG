using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class UpgradeCommand : Command {
        public UpgradeCommand() : base("upgrade", "Command for upgrading a specified stat.", "upgrade [stat]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Upgrade(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
