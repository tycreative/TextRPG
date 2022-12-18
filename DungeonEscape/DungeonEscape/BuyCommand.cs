using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class BuyCommand : Command {
        public BuyCommand() : base("buy", "Command for buying a specified item.", "buy [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.State.Buy(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
