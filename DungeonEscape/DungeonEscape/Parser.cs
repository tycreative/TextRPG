using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public static class Parser {
        private static readonly Command[] general = { new HelpCommand(), new InventoryCommand(), new UseCommand(), new UpgradeCommand(), new QuitCommand() };
        private static readonly Command[] normal = { new MoveCommand(), new UnlockCommand(), new DropCommand(), new TakeCommand() };
        private static readonly Command[] trading = { new BuyCommand(), new SellCommand() };
        private static Dictionary<string, Command> commands = new Dictionary<string, Command>();

        // Update available commands
        public static void UpdateCommands(Command[] update) {
            commands.Clear();
            foreach (Command command in general) commands[command.Name] = command;
            foreach (Command command in update) commands[command.Name] = command;
        }

        // Update available commands based on player state
        public static void NormalState() { UpdateCommands(normal); } // Update commands for normal state
        public static void MerchantArea() { commands["trade"] = new TradeCommand(); } // Add trade command
        public static void TradingState() { UpdateCommands(trading); } // Update commands for trading state
        public static void FightingState() { UpdateCommands(general); } // Update commands for fighting state

        // Parse command input
        public static Command Parse(string input) {
            if (input.Length == 0) return new BackCommand(); // Revert to previous display (if applicable)

            Command command = null;
            string[] args = input.ToLower().Trim().Split(" ");
            if (args.Length > 0) {
                commands.TryGetValue(args[0], out command);
                if (command != null) command.Args = args;
            } return command;
        }

        // Display help info for command
        public static void GetHelp(string name) {
            commands.TryGetValue(name, out var command);
            if (command != null) Display.Info(command.ToString());
            else Display.Warning("Unable to find specified command.");
        }

        // Return string list of available commands
        public static string[] ListCommands() {
            List<string> contents = new List<string>();
            contents.Add("Available commands:");
            contents.Add($"| {"USAGE", -30} | {"DESCRIPTION", -76} |");
            foreach (Command cmd in commands.Values) contents.Add($"| {cmd.Usage, -30} | {cmd.Desc, -76} |");
            return contents.ToArray();
        }
    }
}
