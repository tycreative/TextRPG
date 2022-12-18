using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public abstract class Character : Entity {
        protected Area area;
        protected Inventory inv;
        protected ConsoleColor color;
        protected double health;
        protected double hp;
        protected int exp;
        protected int str;
        protected int coins;

        public Area Area { get { return area; } set { area = value; } }
        public Inventory Inventory { get { return inv; } }
        public ConsoleColor Color { get { return color; } }
        public double Health { get { return health; } set { health = value; } }
        public double HP { get { return hp; } set { hp = value; } }
        public bool Alive { get { return hp > 0; } }
        public int Experience { get { return exp; } set { exp = value; } }
        public int Strength { get { return str; } set { str = value; } }
        public int Coins { get { return coins;} set { coins = value; } }
        // Used for displaying variables
        public string FormatHealth { get { return $"{hp, 2:##0.00}/{health, 2:##0.00}"; } }
        public string FormatCoins { get { return $"{coins, 2:$##,##0}"; } }

        public Character(string name, string desc, double hp, int exp, int str, int coins, Area area = null, Inventory inv = null, ConsoleColor color = ConsoleColor.Gray) : base(name, desc) {
            this.area = area;
            this.inv = inv != null ? inv : new Inventory();
            this.color = color;
            health = hp;
            this.hp = hp;
            this.exp = exp;
            this.str = str;
            this.coins = coins;
        }

        // Heal character by provided amount as long as under max health (returns false otherwise)
        public bool Heal(double amount) {
            if (hp < health) {
                hp = Math.Min(hp + amount, health);
                return true;
            } return false;
        }

        // Harm character by provided amount as long as over 0 (returns false if character died)
        public bool Harm(double amount) {
            hp = Math.Max(hp - amount, 0);
            return hp > 0;
        }

        // Used for moving character around
        public virtual void Move(Area area) {
            this.area = area;
        }
    }
}
