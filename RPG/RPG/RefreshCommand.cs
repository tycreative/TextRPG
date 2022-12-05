using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class RefreshCommand : Command {
        public RefreshCommand() : base("", "", "") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) player.Status();
            return true;
        }
    }
}
