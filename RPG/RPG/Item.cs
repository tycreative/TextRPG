using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public abstract class Item : Object {
        private double weight;
        private int value;
        private bool interactable;

        public double Weight {
            get { return weight; }
        }

        public int Value {
            get { return value; }
            set { this.value = value; }
        }

        public bool Interactable {
            get { return interactable; }
        }

        public Item(string name, string desc, double weight, int value, bool interactable) : base(name, desc) {
            this.weight = weight;
            this.value = value;
            this.interactable = interactable;
        }

        public abstract void Use(Player player);
    }

    public class Wieldable : Item {
        private double damage;
        private int uses;

        public double Damage {
            get { return damage; }
        }

        public Wieldable(string name, string desc, double weight, int value, double damage, int uses) : base(name, desc, weight, value, true) {
            this.damage = damage;
            this.uses = uses;
            Value += uses;
        }

        public override void Use(Player player) {
            if (player.Room.Character != null && player.Room.Character is Enemy && player.Room.Character.Health > 0) {
                uses -= 1;
                Value -= 1;
                if (uses <= 0) player.Inventory.Remove(Name);
                if (!player.Room.Character.Harm(player.Strength + damage)) { // killed the enemy
                    player.Experience += player.Room.Character.Experience;
                    player.Coins += player.Room.Character.Coins;
                    Display.Success($"Damaged {player.Room.Character.Name} for {player.Strength + damage} HP, killing it. You gained {player.Room.Character.Experience} experience and {player.Room.Character.Coins} coins.");
                } else if (!player.Harm(player.Room.Character.Strength)) { // you died
                    Display.Error($"Damaged {player.Room.Character.Name} for {player.Strength + damage} HP. {player.Room.Character.Name} damaged you for {player.Room.Character.Strength} HP, killing you.");
                    NotificationCenter.Instance.PostNotification(new Notification("PlayerDied"));          
                } else Display.Info($"Damaged {player.Room.Character.Name} for {player.Strength + damage} HP. {player.Room.Character.Name} damaged you for {player.Room.Character.Strength} HP.");
            } else Display.Warning("Unable to use weapon in current situation."); 
        }

        public override string ToString() {
            // name, weight, cost, damage, durability
            return $"| {Value, 5:$##0} | {damage, 6:##0.00} | {Weight, 6:##0.00} | {uses, 4:###0} |";
        }
    }

    public class Consumable : Item {
        private double health;

        // Potions
        public Consumable(string name, string desc, double weight, int value, double health) : base(name, desc, weight, value, true) {
            this.health = health;
        }

        public override void Use(Player player) {
            if (player.Heal(health) && player.Inventory.Remove(Name)) Display.Success("Restored health by " + health + " HP.");
            else Display.Warning("Unable to restore health.");

            if (player.Room.Character != null && player.Room.Character is Enemy && player.Room.Character.Health > 0) {
                if (!player.Harm(player.Room.Character.Strength)) { // you died
                    NotificationCenter.Instance.PostNotification(new Notification("PlayerDied"));
                    Display.Error($"Restored health by {health} HP, but {player.Room.Character.Name} damaged you for {player.Room.Character.Strength} HP, killing you.");
                } else Display.Success($"Restored health by {health} HP, but {player.Room.Character.Name} damaged you for {player.Room.Character.Strength} HP.");
            }
        }

        public override string ToString() {
            return $"| {Value, 5:$##0} | {health, 6:##0.00} | {Weight, 6:##0.00} |";
        }
    }

    public class Miscellaneous : Item {
        // Decor items / valuables
        public Miscellaneous(string name, string desc, double weight, int value, bool interactable) : base(name, desc, weight, value, interactable) {}

        public override void Use(Player player) {
            Display.Warning("This item cannot be used.");
        }

        public override string ToString() {
            // name, weight, cost, count
            return $"| {Value, 5:$##0} | {Weight, 6:##0.00} |";
        }
    }
}