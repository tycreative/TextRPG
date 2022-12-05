using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class Enemy : Character {

        public Enemy(string name, string desc, Room room, double health, int experience, int strength, int coins) : base(name, desc, room, new Inventory(), health, experience, strength, coins) {
            // Add random items to enemy inventory?
        }

        public new bool Harm(double amount) {
            base.Harm(amount);
            if (Health <= 0) {
                foreach (Item item in Inventory.Items) {
                    Room.Inventory.Add(item);
                }
                return false;
            }
            return true;
        }
    }
}
