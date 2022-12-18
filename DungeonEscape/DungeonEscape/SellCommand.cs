using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class SellCommand : Command {
        public SellCommand() : base("sell", "Command for selling a specified item.", "sell [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.State.Sell(FormatArgs);
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
