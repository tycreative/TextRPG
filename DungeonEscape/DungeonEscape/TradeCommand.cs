using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class TradeCommand : Command {
        public TradeCommand() : base("trade", "Command for trading with a merchant.", "trade") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) player.State.Trade();
            else Display.Error("Improper usage, try: " + Usage);
            return true;
        }
    }
}
