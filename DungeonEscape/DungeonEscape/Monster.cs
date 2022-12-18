using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class Monster : Character {
        public Monster(string name, string desc, double hp, int exp, int str, int coins, Area area, Inventory inv) : base(name, desc, hp, exp, str, coins, area, inv, ConsoleColor.DarkRed) {
            this.area.Type = new MonsterArea(this);
        }

        // Update area to be MonsterArea when moved
        public override void Move(Area area) {
            this.area.Type = null;
            base.Move(area);
            this.area.Type = new MonsterArea(this);
        }

        // Drop items on death
        public void OnDeath() {
            foreach (Item item in Inventory.Items) area.Inventory.Add(item);
            Inventory.Items.Clear();
        }

        // Display random intro message
        private string[] intro = {"You dare approach me?", "Who are you to disturb me?", "Who goes there?", "Well, well, well..."};
        public void SayIntro() {
            Display.Title($"{name.ToUpper()}: {intro[Globals.RANDOM.Next(intro.Length)]}", color);
        }

        // Display random taunt message
        private string[] taunt = {"Is that all you got?", "This won't take much longer.", "You were a fool to challenge me.", "Enjoy your final moments alive.", "This is fun."};
        public void SayTaunt() {
            Display.Title($"{name.ToUpper()}: {taunt[Globals.RANDOM.Next(taunt.Length)]}", color);
        }

        // Display random loss response message
        private string[] loss = {"Seems I underestimated you...", "Where did I go wrong?", "How could this happen?", "Impossible.", "Argggggghhhhhh!"};
        public void SayLoss() {
            Display.Title($"{name.ToUpper()}: {loss[Globals.RANDOM.Next(loss.Length)]}", color);
        }

        // Display random win response message
        private string[] win = {"That'll teach you.", "Maybe next time...", "I knew you were a fool.", "Enjoy the afterlife.", "Another corpse for my collection."};
        public void SayWin() {
            Display.Title($"{name.ToUpper()}: {win[Globals.RANDOM.Next(win.Length)]}", color);
        }

        public override string ToString() { // 20 + 12
            return $"{Name, -32}Experience: {exp, -12:##0}Strength: {str, -11:##0}{$"Coins: {FormatCoins}", 10}";
        }
    }
}