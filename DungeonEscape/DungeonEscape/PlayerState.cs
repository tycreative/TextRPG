using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public abstract class PlayerState {
        protected Player player;

        public virtual void Use(string name) {
            player.Use(name);
        }

        public virtual void Trade() {
            Display.Warning("No merchant is present.");
        }

        public abstract void Back();

        public virtual void Buy(string name) {
            Display.Warning("No merchant is present to buy from.");
        }

        public virtual void Sell(string name) {
            Display.Warning("No merchant is present to sell to.");
        }
    }

    // State for when player is wandering around, optional merchant for when one present in area
    public class NormalState : PlayerState {
        private Merchant merchant;

        public NormalState(Player player, Merchant merchant = null) {
            this.player = player;
            this.merchant = merchant;
            Parser.NormalState();
            if (merchant != null) Parser.MerchantArea();
            else Display.Title("", ConsoleColor.Gray);
            Game.View = new AreaView(player, player.Area);
        }

        // Switch to trading state (TradeCommand) if applicable
        public override void Trade() {
            if (merchant != null) player.State = new TradingState(player, merchant); 
            else base.Trade();
        }

        public override void Back() {
            Display.Title("", ConsoleColor.Gray);
            player.State = new NormalState(player, merchant);
        }

        public override void Buy(string name) {
            if (merchant != null) Display.Warning("Use 'trade' to interact with a merchant.");
            else base.Buy("");
        }

        public override void Sell(string name) {
            if (merchant != null) Display.Warning("Use 'trade' to interact with a merchant.");
            else base.Sell("");
        }
    }

    // State for when player is trading with a merchant
    public class TradingState : PlayerState {
        private Merchant merchant;

        public TradingState(Player player, Merchant merchant) {
            this.player = player;
            this.merchant = merchant;
            Parser.TradingState();
            Game.View = new TradingView(player, merchant);
            merchant.SayGreeting();
        }

        // Buy an item from a merchant (BuyCommand)
        public override void Buy(string name) {
            Item item = merchant.Inventory.Find(name);
            if (item != null) {
                if (player.Coins >= item.Value) {
                    if (item.Weight + player.Inventory.Weight <= player.Capacity) {
                        if (merchant.Inventory.Remove(item) && player.Inventory.Add(item)) {
                            player.Coins -= item.Value;
                            merchant.SayBought();
                            Display.Success($"Bought '{item.Name}' from merchant.");
                        } else Display.Warning($"Unable to buy '{item.Name}' from merchant.");
                    } else Display.Warning("Item is too heavy to take.");
                } else Display.Warning("Insufficient funds available.");
            } else Display.Warning("Unable to find the specified item.");
        }

        // Sell an item to a merchant (SellCommand)
        public override void Sell(string name) {
            Item item = player.Inventory.Find(name);
            if (item != null) {
                if (merchant.Coins >= item.Value / 2) {
                    if (player.Inventory.Remove(item) && merchant.Inventory.Add(item)) {
                        merchant.Coins -= item.Value / 2;
                        player.Coins += item.Value / 2;
                        merchant.SaySold();
                        Display.Success($"Sold '{item.Name}' to merchant for ${item.Value / 2} coins.");
                    } else Display.Warning($"Unable to sell '{item.Name}' to merchant.");
                } else Display.Warning("Merchant has insufficient funds available.");
            } else Display.Warning("Unable to find the specified item.");
        }

        // Return from trading state (BackCommand)
        public override void Back() {
            merchant.SayGoodbye();
            player.State = new NormalState(player, merchant);
        }
    }

    // State for when player is fighting a monster
    public class FightingState : PlayerState {
        private Monster monster;

        public FightingState(Player player, Monster monster) {
            this.player = player;
            this.monster = monster;
            Parser.FightingState();
            Game.View = new FightingView(player, monster);
            monster.SayIntro();
        }

        // Mimic turn-based combat for any valid use action (UseCommand)
        public override void Use(string name = "") {
            if (player.Use(name, monster)) {
                if (!monster.Alive) { // Check if monster died
                    monster.OnDeath();
                    monster.SayLoss();
                    player.Area.Type = null;
                } else if (!player.Alive) { // Check if player died
                    monster.SayWin();
                    NotificationCenter.Instance.PostNotification(new Notification("PlayerDied")); // Tell Game that player died
                } else monster.SayTaunt();
            } else Display.Warning("Unable to find the specified item.");
        }

        // Return from monster engagement if monster is dead (BackCommand)
        public override void Back() {
            if (!monster.Alive) player.State = new NormalState(player);
            else player.State = new FightingState(player, monster);
        }
    }
}