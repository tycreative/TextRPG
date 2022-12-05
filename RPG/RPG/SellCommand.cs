using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class SellCommand : Command {
        public SellCommand() : base("sell", "Command for selling a specified item.", "sell [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Sell(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
