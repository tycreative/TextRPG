using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class UnlockCommand : Command {
        public UnlockCommand() : base("unlock", "Command for unlocking door in specified direction.", "unlock [direction]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Unlock(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
