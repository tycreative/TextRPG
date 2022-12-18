using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class HelpCommand : Command {
        public HelpCommand() : base("help", "Command for revealing details of a specified command.", "help [optional command]") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) Game.View = new HelpView();
            else if (args.Length >= 2) Parser.GetHelp(FormatArgs);
            else Console.WriteLine("Improper usage, try: " + Usage);
            return true;
        }
    }
}