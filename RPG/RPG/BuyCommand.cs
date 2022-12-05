using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class BuyCommand : Command {
        public BuyCommand() : base("buy", "Command for buying a specified item.", "buy [item name]") { }

        public override bool Execute(Player player) {
            if (args.Length >= 2) player.Buy(Concat());
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
