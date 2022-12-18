using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    // Item abstract parent class
    public abstract class Item : Entity {
        protected double weight;
        protected bool takeable;

        public double Weight { get { return weight; } }
        public abstract int Value { get ;}
        public bool Takeable { get { return takeable; } }

        public Item(string name, string desc, double weight, bool takeable = true) : base(name, desc) {
            this.weight = weight;
            this.takeable = takeable;
        }

        public abstract void Use(Character user, Character foe);
    }

    // Wieldable items (weapons) that have damage and number of uses
    public class Wieldable : Item {
        private double damage;
        private int uses;

        public override int Value { get { return (int)damage + uses; } }
        public double Damage { get { return damage; } }

        public Wieldable(string name, string desc, double weight, double damage, int uses) : base(name, desc, weight) {
            this.damage = damage;
            this.uses = uses;
        }

        // Use item on foe, foe can strike user if alive
        public override void Use(Character user, Character foe) {
            if (foe != null && foe.Alive) {
                uses -= 1;
                if (uses <= 0) user.Inventory.Remove(this); // Remove item if out of uses
                if (!foe.Harm(user.Strength + damage)) { // Foe has been slain
                    user.Experience += foe.Experience;
                    user.Coins += foe.Coins;
                    Display.Success($"Damaged {foe.Name} for {user.Strength + damage} HP, killing it. {user.Name} gained {foe.Experience} experience and ${foe.Coins} coins. Hit 'ENTER' to continue.");
                } else if (!user.Harm(foe.Strength)) { // User has been slain
                    Display.Error($"Damaged {foe.Name} for {user.Strength + damage} HP. {foe.Name} damaged you for {foe.Strength} HP, killing you.");
                } else Display.Info($"Damaged {foe.Name} for {user.Strength + damage} HP. {foe.Name} damaged you for {foe.Strength} HP.");
            } else Display.Info("You swing wildly through the air.");
        }

        public override string ToString() {
            return $"| {Value, 5:$##0} | {damage, 6:##0.00} | {Weight, 6:##0.00} | {uses, 4:###0} |";
        }
    }

    // Consumable items (potions) that restore character's health
    public class Consumable : Item {
        private double health;

        public override int Value { get { return (int)health; } }

        public Consumable(string name, string desc, double weight, double health) : base(name, desc, weight) {
            this.health = health;
        }

        // Restores user health, foe can strike user if fighting
        public override void Use(Character user, Character foe) {
            if (user.Inventory.Remove(this) && user.Heal(health)) {
                if (foe != null && foe.Alive) {
                    if (!user.Harm(foe.Strength)) Display.Error($"Restored health by {health} HP, but {foe.Name} damaged you for {foe.Strength} HP, killing you."); // User has been slain
                    else Display.Info($"Restored health by {health} HP, but {foe.Name} damaged you for {foe.Strength} HP.");
                } else Display.Success($"Restored health by {health} HP.");
            } else Display.Warning("Unable to restore health.");
        }

        public override string ToString() {
            return $"| {Value, 5:$##0} | {health, 6:##0.00} | {Weight, 6:##0.00} |";
        }
    }

    // Miscellaneous and decorative items, takeable bool assignable
    public class Miscellaneous : Item {
        private int value;

        public override int Value { get { return value; } }

        public Miscellaneous(string name, string desc, double weight, int value, bool takeable) : base(name, desc, weight, takeable) {
            this.value = value;
        }

        // If fighting, throw item at foe, foe can strike user back
        public override void Use(Character user, Character foe) {
            if (user.Inventory.Remove(this) && user.Area.Inventory.Add(this)) {
                if (foe != null && foe.Alive) {
                    if (!user.Harm(foe.Strength)) Display.Error($"You threw threw the item at {foe.Name}, annoying it. {foe.Name} damaged you for {foe.Strength} HP, killing you."); // User has been slain
                    else Display.Info($"You threw the item at {foe.Name}, annoying it. {foe.Name} damaged you for {foe.Strength} HP.");
                } else Display.Info("You throw the item into the air and watch as it falls onto the floor.");
            } else Display.Warning("This item cannot be used.");
        }

        public override string ToString() {
            return $"| {Value, 5:$##0} | {Weight, 6:##0.00} |";
        }
    }
}