using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class InventoryCommand : Command {
        public InventoryCommand() : base("inventory", "Command for viewing your inventory.", "inventory") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) {
                Display.DisplayState = new InventoryState();
                Display.Info("Showing your inventory contents, hit 'ENTER' to return to previous screen.");
            }
            else Console.WriteLine("Improper usage, try: " + Usage);
            return true;
        }
    }
}
