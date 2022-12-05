using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class LevelCommand : Command {
        public LevelCommand() : base("level", "Command for leveling up a specified stat.", "level [stat]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Level(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
