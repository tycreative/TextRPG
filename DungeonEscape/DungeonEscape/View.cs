using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    // State Design Pattern
    public abstract class View {
        // Template Design Pattern
        public abstract void Header();
        public abstract void Main();
        public virtual void Footer() {
            Display.Span('=');
            Display.Span();
        }

        public void Render() {
            Header();
            Main();
            Footer();
        }
    }

    // Title Screen View
    public class TitleView : View {
        private int padding;

        public TitleView(int padding = 9) {
            this.padding = padding;
        }

        public override void Header() {
            Display.Span();
            Display.Span('=');
            Display.Span();
        }

        public override void Main() {
            Display.FullScreen(new string[] {
                "______                                    _____                         ",
                "|  _  \\                                  |  ___|                        ",
                "| | | |_   _ _ __   __ _  ___  ___  _ __ | |__ ___  ___ __ _ _ __   ___ ",
                "| | | | | | | '_ \\ / _` |/ _ \\/ _ \\| '_ \\|  __/ __|/ __/ _` | '_ \\ / _ \\",
                "| |/ /| |_| | | | | (_| |  __/ (_) | | | | |__\\__ \\ (_| (_| | |_) |  __/",
                "|___/  \\__,_|_| |_|\\__, |\\___|\\___/|_| |_\\____/___/\\___\\__,_| .__/ \\___|",
                "                    __/ |                                   | |         ",
                "                   |___/                                    |_|         "
            }, padding); // 9 default, 15 for end
        }

        public override void Footer() {
            Display.Span();
            Display.Span('=');
            Display.Span();
        }
    }

    // Commands Help View
    public class HelpView : View {
        public HelpView() {
            Display.Info("Showing your available commands, hit 'ENTER' to return to previous screen.");
        }

        public override void Header() { }

        public override void Main() {
            Display.FullScreen(Parser.ListCommands(), 5);
        }
    }

    // Inventory Contents View
    public class InventoryView : View {
        private Player player;

        public InventoryView(Player player) {
            this.player = player;
            Display.Info("Showing your inventory contents, hit 'ENTER' to return to previous screen.");
        }

        public override void Header() { }

        public override void Main() {
            Display.FullScreen(player.Inventory.ListContents($"{player.Name}'s Inventory", player.FormatCapacity, true), 5);
        }
    }

    // Area Information View
    public class AreaView : View {
        private Player player;
        private Area area;

        public AreaView(Player player, Area area) {
            this.player = player;
            this.area = area;
            Display.Info("What would you like to do? Type 'help' for available commands.");
        }

        public override void Header() {
            Display.Character(player);
            Display.Span('=');
            Display.Output(area.Name);
            Display.Output(area.Desc + (area.Type != null ? $" {area.Type.Desc}" : ""));
            Display.Span('=');
        }

        public override void Main() {
            Display.SplitScreen(area.Inventory.ListContents($"Contents of {area.Name}", ""), area.GetExits());
        }
    }

    // Trading Screen View
    public class TradingView : View {
        private Player player;
        private Merchant merchant;

        public TradingView(Player player, Merchant merchant) {
            this.player = player;
            this.merchant = merchant;
            Display.Info("Interacting with the merchant. Type 'help' for available commands.");
        }

        public override void Header() {
            Display.Character(player);
            Display.Span('=');
        }

        public override void Main() {
            Display.SplitScreen(merchant.Inventory.ListContents("Merchant's Inventory", merchant.FormatCoins), player.Inventory.ListContents($"{player.Name}'s Inventory", player.FormatCapacity), 8);
        }
    }

    // Fighting Screen View
    public class FightingView : View {
        private Player player;
        private Monster monster;

        public FightingView(Player player, Monster monster) {
            this.player = player;
            this.monster = monster;
            Display.Info("Fighting with the monster. Type 'help' for available commands.");
        }

        public override void Header() {
            Display.Character(player);
            Display.Span('=');
            Display.Character(monster);
            Display.Span('=');
        }

        public override void Main() {
            Display.SplitScreen(player.Inventory.ListWieldables(), player.Inventory.ListConsumables());
        }
    }
}