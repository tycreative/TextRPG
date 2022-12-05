using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public abstract class DisplayState {
        public abstract void View(Player player);
    }

    public class StartState : DisplayState {
        public override void View(Player player) {
            Display.FullView(new string[] {
                "______                                    _____                         ",
                "|  _  \\                                  |  ___|                        ",
                "| | | |_   _ _ __   __ _  ___  ___  _ __ | |__ ___  ___ __ _ _ __   ___ ",
                "| | | | | | | '_ \\ / _` |/ _ \\/ _ \\| '_ \\|  __/ __|/ __/ _` | '_ \\ / _ \\",
                "| |/ /| |_| | | | | (_| |  __/ (_) | | | | |__\\__ \\ (_| (_| | |_) |  __/",
                "|___/  \\__,_|_| |_|\\__, |\\___|\\___/|_| |_\\____/___/\\___\\__,_| .__/ \\___|",
                "                    __/ |                                   | |         ",
                "                   |___/                                    |_|         "
            });
            Display.HorizontalBar('=');
            Display.HorizontalBar(' ');
        }
    }

    public class HelpState : DisplayState {
        public override void View(Player player) {
            Display.FullView(Parser.ListCommands(), 4);
            Display.HorizontalBar('=');
            Display.HorizontalBar(' ');
        }
    }

    public class InventoryState : DisplayState {
        public override void View(Player player) {
            Display.FullView(player.Inventory.ListContents(player.Name + "'s Inventory", player.FormatCapacity, true), 4);
            Display.HorizontalBar('=');
            Display.HorizontalBar(' ');
        }
    }

    public class ScanState : DisplayState {
        public override void View(Player player) {
            Display.InfoBar(player.ToString());
            Display.HealthBar(player.Health, player.MaxHealth);
            Display.HorizontalBar('=');
            Display.InfoBar(player.Room.Name + ": " + player.Room.Desc);
            Display.InfoBar("NPC: " + (player.Room.Character == null ? "None" : player.Room.Character.Name));
            Display.HorizontalBar('=');
            Display.SplitView(player.Room.Inventory.ListContents(player.Room.Name + " Contents", ""), player.Room.FormatExits());
            Display.HorizontalBar('=');
            Display.HorizontalBar(' ');
        }
    }

    public class TradingState : DisplayState {
        public override void View(Player player) {
            Display.InfoBar(player.ToString());
            Display.HealthBar(player.Health, player.MaxHealth);
            Display.HorizontalBar('=');
            Display.InfoBar(player.Room.Name + ": " + player.Room.Desc);
            Display.InfoBar("NPC: " + player.Room.Character.Name);
            Display.HorizontalBar('=');
            Display.SplitView(player.Room.Character.Inventory.ListContents(player.Room.Character.Name + " Contents", player.Room.Character.FormatCoins),player.Inventory.ListContents(player.Name + "'s Inventory",  player.FormatCoins));
            Display.HorizontalBar('=');
            Display.HorizontalBar(' ');
        }
    }

    public class FightingState : DisplayState {
        public override void View(Player player) {
            Display.InfoBar(player.ToString());
            Display.HealthBar(player.Health, player.MaxHealth);
            Display.HorizontalBar('=');
            Display.InfoBar(player.Room.Character.ToString(), ConsoleColor.Red);
            Display.HealthBar(player.Room.Character.Health, player.Room.Character.MaxHealth);
            Display.HorizontalBar('=');
            Display.SplitView(player.Inventory.ListWieldables(), player.Inventory.ListConsumables());
            Display.HorizontalBar('=');
            Display.HorizontalBar(' ');
        }
    }
}