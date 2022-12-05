using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public static class Parser {
        private static readonly Command[] general = { new HelpCommand(), new InventoryCommand(), new UseCommand(), new RefreshCommand(), new QuitCommand() };
        private static readonly Command[] normal = { new DropCommand(), new TakeCommand(), new TradeCommand(), new MoveCommand(), new BackCommand(), new UnlockCommand(), new LevelCommand() };
        private static readonly Command[] trading = { new BuyCommand(), new SellCommand(), new MoveCommand(), new BackCommand() };

        public static void SetCommands(Command[] allowed) {
            commands.Clear();
            foreach (Command command in general) commands[command.Name] = command;
            foreach (Command command in allowed) commands[command.Name] = command;
        }

        public static void NormalState() {
            SetCommands(normal);
        }

        public static void TradingState() {
            SetCommands(trading);
        }

        public static void FightingState() {
            SetCommands(new Command[0]);
        }

        private static Dictionary<string, Command> commands = new Dictionary<string, Command>();

        public static Dictionary<string, Command> Commands {
            get { return commands; }
            set { commands = value; }
        }

        public static Command Parse(string input) {
            Command command = null;
            string[] args = input.ToLower().Split(" ");

            if (args.Length > 0) {
                Commands.TryGetValue(args[0], out command);
                if (command != null) command.Args = args;
            }
            return command;
        }

        public static string[] ListCommands() {
            List<string> contents = new List<string>();
            contents.Add("Available commands:");
            contents.Add($"| {"NAME", -12} | {"DESCRIPTION", -64} | {"USAGE", -30} |");
            foreach (Command command in commands.Values) {
                if (command.Name != "") contents.Add(command.ToString());
            }
            return contents.ToArray();
        }
    }
}