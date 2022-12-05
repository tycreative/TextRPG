using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class Character : Object {
        protected Room room;
        protected Inventory inventory;
        protected double maxHealth;
        protected double health;
        protected int experience;
        protected int strength;
        protected int coins;

        public Room Room {
            get { return room; }
            set { room = value; }
        }

        public Inventory Inventory {
            get { return inventory; }
        }

        public double MaxHealth {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public double Health {
            get { return health; }
            set { health = value; }
        }

        public int Experience {
            get { return experience; }
            set { experience = value; }
        }

        public int Strength {
            get { return strength; }
            set { strength = value; }
        }

        public int Coins {
            get { return coins; }
            set { coins = value; }
        }

        public string FormatCoins {
            get { return $"{coins, 2:$##,##0}"; }
        }

        // Merchant creation
        public Character(string name, string desc, Room room, Inventory inventory, int coins) : this(name, desc, room, inventory, 100, 0, 0, coins) { }

        // Enemy creation
        public Character(string name, string desc, Room room, double health, int experience, int strength, int coins) : this(name, desc, room, new Inventory(), health, experience, strength, coins) { }

        // Player creation
        public Character(string name, string desc, Room room, Inventory inventory, double health, int experience, int strength, int coins) : base(name, desc) {
            if (room != null) room.Character = this;
            this.room = room;
            this.inventory = inventory;
            maxHealth = health;
            this.health = health;
            this.experience = experience;
            this.strength = strength;
            this.coins = coins;
        }

        // Return false if heal cannot be applied (already at full health)
        public bool Heal(double amount) {
            if (health < maxHealth) {
                health += amount;
                if (health > maxHealth) health = maxHealth;
                return true;
            } return false;
        }

        // Return false if character dies
        public bool Harm(double amount) {
            health -= amount;
            if (health > 0) return true;
            health = 0;
            return false;
        }
    }
}