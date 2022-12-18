using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class InventoryCommand : Command {
        public InventoryCommand() : base("inventory", "Command for viewing your inventory.", "inventory") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) Game.View = new InventoryView(player);
            else Console.WriteLine("Improper usage, try: " + Usage);
            return true;
        }
    }
}
