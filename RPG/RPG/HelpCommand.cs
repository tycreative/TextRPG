using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class HelpCommand : Command {
        public HelpCommand() : base("help", "Command for revealing details of a specified command.", "help [command (optional)]") { }

        public override bool Execute(Player player) {
            if (args.Length < 2) {
                Display.DisplayState = new HelpState();
                Display.Info("Showing your available commands, hit 'ENTER' to return to previous screen.");
            } else if (args.Length >= 2) {
                Parser.Commands.TryGetValue(Concat(), out var command);
                if (command != null) Display.Info(command.Name + " (" + command.Usage + "): " + command.Desc);
                else Display.Warning("Unable to find specified command.");
            }
            else Console.WriteLine("Improper usage, try: " + Usage);
            return true;
        }
    }
}
