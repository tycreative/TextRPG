using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class Game {
        private static View view;
        private Player player;
        private bool playing;

        public static View View { set { view = value; } }

        public Game() {
            NotificationCenter.Instance.AddObserver("PlayerDied", PlayerDied);
            NotificationCenter.Instance.AddObserver("PlayerMoved", PlayerMoved);
            playing = false;
        }

        // Upon Game Start
        public void Start() {
            view = new TitleView();
            Display.Success("Welcome to DungeonEscape! Please enter your desired username:");
            player = new Player(GetName(), World.Instance.Start, new Inventory(new List<Item>() { Creator.Items[0], Creator.Items[4], Creator.Items[11] }), 42);
            World.Instance.Player = player;
            playing = true;
        }

        // Get Username from Player
        public string GetName() {
            while (true) {
                Console.Clear();
                view.Render();
                Display.Render();
                Console.Write(" >: ");
                string name = Console.ReadLine();
                if (name.Trim().Length < 3) {
                    Display.Error("Username must be at least 3 characters long.");
                } else if (name.Trim().Length > 24) {
                    Display.Error("Username cannot be that long.");
                } else return name;
            }
        }

        // Game Loop
        public void Run() {
            player.State = new NormalState(player);
            bool running = true;

            while (running && playing) {
                Console.Clear();
                view.Render();
                Display.Render();
                Console.Write(" >: ");

                Command command = Parser.Parse(Console.ReadLine());
                if (command != null) running = command.Execute(player);
                else Display.Error("Command not recognized.");
            }
        }

        // Upon Game End
        public void End() {
            view = new TitleView(16);
            Console.Clear();
            view.Render();
            Display.Render();
            Console.WriteLine(" Thanks for playing!");
        }

        // Upon Player death notification
        public void PlayerDied(Notification notification) {
            playing = false;
        }

        // Upon PlayerMoved notification
        public void PlayerMoved(Notification notification) {
            if (player.Area == World.Instance.End) {
                Display.Success("Congratulations you won!");
                playing = false;
            }
        }
    }
}
