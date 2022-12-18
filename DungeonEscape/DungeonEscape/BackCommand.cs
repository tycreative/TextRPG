using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class BackCommand : Command {
        public BackCommand() : base("", "Command for checking current state.", "") { }

        public override bool Execute(Player player) {
            player.State.Back();
            return true;
        }
    }
}
