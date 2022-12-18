using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class UnlockCommand : Command {
        public UnlockCommand() : base("unlock", "Command for unlocking door in specified direction.", "unlock [direction]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Unlock(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
