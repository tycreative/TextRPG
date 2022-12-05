using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class MoveCommand : Command {
        public MoveCommand() : base("move", "Command for moving in a specified direction.", "move [direction]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Move(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
