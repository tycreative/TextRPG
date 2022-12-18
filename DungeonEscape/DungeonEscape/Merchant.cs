using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class Merchant : Character {
        public Merchant(string name, int coins, Area area, Inventory inv) : base(name, "A trader who buys and sells goods.", 100, 0, 0, coins, area, inv, ConsoleColor.DarkGreen) {
            this.area.Type = new MerchantArea(this);
        }

        // Update area to be MerchantArea when moved
        public override void Move(Area area) {
            this.area.Type = null;
            base.Move(area);
            this.area.Type = new MerchantArea(this);
        }

        // Display random greeting message
        private string[] hello = {"Hello there, see something you like?", "Welcome, what can I get ya?", "Greetings, looking for something?"};
        public void SayGreeting() {
            Display.Title($"{name.ToUpper()}: {hello[Globals.RANDOM.Next(hello.Length)]}", color);
        }

        // Display random bought response message
        private string[] buy = {"An excellent purchase!", "One of my best sellers.", "That will come in handy!", "Great find!", "One of my favorites."};
        public void SayBought() {
            Display.Title($"{name.ToUpper()}: {buy[Globals.RANDOM.Next(buy.Length)]}", color);
        }

        // Display random sell response message
        private string[] sell = {"I've been looking for that.", "That will come in handy!", "One of my favorites.", "Where did you find that?", "I'm gonna go broke."};
        public void SaySold() {
            Display.Title($"{name.ToUpper()}: {sell[Globals.RANDOM.Next(sell.Length)]}", color);
        }

        // Display random goodbye message
        private string[] bye = {"Please come again!", "Until we meet again!", "Take care!", "Come back soon!", "You know where to find me."};
        public void SayGoodbye() {
            Display.Title($"{name.ToUpper()}: {bye[Globals.RANDOM.Next(bye.Length)]}", color);
        }
    }
}