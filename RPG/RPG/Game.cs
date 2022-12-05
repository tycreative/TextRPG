using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {

    public static class Globals {
        public static readonly int SCREEN_WIDTH = 120;
        public static readonly int SCREEN_HEIGHT = 30;
        public static readonly int VERTICAL_MARGINS = 0;
        public static readonly int HORIZONTAL_MARGINS = 1;
        public static readonly Dictionary<string, string> DIRECTIONS = new Dictionary<string, string>{ 
            { "north", "south" },
            { "south", "north" },
            { "east", "west" },
            { "west", "east" }
        };
    }

    public class Game {
        private Player player;
        private bool playing;

        public Game() {
            playing = false;
            NotificationCenter.Instance.AddObserver("PlayerDied", PlayerDied);
            NotificationCenter.Instance.AddObserver("PlayerMoved", PlayerMoved);
        }

        public void Start() {
            Display.DisplayState = new StartState();
            Display.Success("Welcome to DungeonEscape! Please enter your desired username:");
            player = new Player(GetName(), GameWorld.Instance.Entrance, new Inventory(), 64);
            player.Inventory.Add(new Wieldable("Stone Sword", "A simple sword made of stone.", 15, 10, 15, 10));
            player.Inventory.Add(new Consumable("Simple Potion", "Potion that restores 10 health.", 0.5, 10, 10));
            player.Inventory.Add(new Miscellaneous("Amulet", "A small piece of jewelry.", 1.4, 15, true));
            playing = true;
        }

        public string GetName() {
            Console.Clear();
            Display.DisplayState.View(null);
            Display.Output();
            Console.Write(" >: ");
            while (true) {
                string name = Console.ReadLine();
                if (name.Trim().Length < 3) {
                    Display.Error("Username must be at least 3 characters long.");
                } else if (name.Trim().Length > 24) {
                    Display.Error("Username cannot be that long.");
                } else return name;
                Console.Clear();
                Display.DisplayState.View(null);
                Display.Output();
                Console.Write(" >: ");
            }
        }

        public void Play() {
            player.Status();
            Display.Info("What would you like to do? Type 'help' for more options.");

            bool running = true;
            while (running && playing ) {
                Console.Clear();
                Display.DisplayState.View(player);
                Display.Output();
                Console.Write(" >: ");

                Command command = Parser.Parse(Console.ReadLine());
                if (command != null) running = command.Execute(player);
                else Display.Error("Command not recognized.");
            }
        }

        public void PlayerDied(Notification notification) {
            playing = false;
        }

        public void PlayerMoved(Notification notification) {
            Player player = (Player)notification.Object;
            if (player.Room == GameWorld.Instance.Exit) {
                Display.Success("Congratulations you won!");
                playing = false;
            }
        }

        public void End() {
            Display.DisplayState = new StartState();
            Console.Clear();
            Display.DisplayState.View(player);
            Display.Output();
            Console.WriteLine(" Thanks for playing!");
        }
    }
}
