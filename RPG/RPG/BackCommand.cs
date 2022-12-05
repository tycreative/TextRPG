using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class BackCommand : Command {
        public BackCommand() : base("back", "Command for backtracking.", "back") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) player.Back();
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
