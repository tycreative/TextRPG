using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class UseCommand : Command {
        public UseCommand() : base("use", "Command for using a specified item.", "use [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) player.Use();
            else if (args.Length >= 2) player.Use(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
