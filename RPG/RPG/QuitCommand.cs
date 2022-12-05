using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class QuitCommand : Command {
        public QuitCommand() : base("quit", "Command to exit the game.", "quit") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) return false;
            else Console.WriteLine("Improper usage, try: " + Usage);
            return true;
        }
    }
}
