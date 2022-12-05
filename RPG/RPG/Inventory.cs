using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG {
    public class Inventory {
        private List<Item> items;

        public double Weight {
            get {
                double total = 0.0;
                foreach (Item item in items) total += item.Weight;
                return total;
            }
        }

        public List<Item> Items {
            get { return items; }
        }

        public Inventory() {
            items = new List<Item>();
        }

        public bool Add(Item item) {
            items.Add(item);
            return Find(item.Name) != null;
        }

        public bool Remove(string name) {
            Item item = Find(name);
            if (item == null) return false;
            else items.Remove(item);
            return true;
        }

        public Item Find(string name) {
            return items.Find(item => string.Equals(item.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public string[] ListWieldables(bool details = false) {
            IEnumerable<Wieldable> wieldables = items.OfType<Wieldable>().OrderByDescending(w => w.Damage);
            List<string> contents = new List<string>();
            if (details) {
                contents.Add($"| {"WIELDABLE", -29} | {"DESCRIPTION", -49} | VALUE | DAMAGE | WEIGHT | USES |");
                if (wieldables.Count() > 0) foreach (Wieldable w in wieldables) contents.Add($"| {w.Name, -29} | {w.Desc, -49} {w}");
                else contents.Add($"| No contents. {new string(' ', 102)}|");
            } else {
                contents.Add($"| {"WIELDABLE", -21} | VALUE | DAMAGE | WEIGHT | USES |");
                if (wieldables.Count() > 0) foreach (Wieldable w in wieldables) contents.Add($"| {w.Name, -21} {w}");
                else contents.Add($"| No contents. {new string(' ', 42)}|");
            } return contents.ToArray();
        }

        public string[] ListConsumables(bool details = false) {
            IEnumerable<Consumable> consumables = items.OfType<Consumable>().GroupBy(c => c.Name).Select(c => c.First());
            List<string> contents = new List<string>();
            // string name, string desc, int value, double health, double weight, int count
            if (details) {
                contents.Add($"| {"CONSUMABLE", -29} | {"DESCRIPTION", -49} | VALUE | HEALTH | WEIGHT | AMT. |");
                if (consumables.Count() > 0) foreach (Consumable c in consumables) contents.Add($"| {c.Name, -29} | {c.Desc, -49} {c} {items.Where(x => x.Name.Equals(c.Name)).Count(), 4:###0} |");
                else contents.Add($"| No contents. {new string(' ', 102)}|");
            } else {
                contents.Add($"| {"CONSUMABLE", -21} | VALUE | HEALTH | WEIGHT | AMT. |");
                if (consumables.Count() > 0) foreach (Consumable c in consumables) contents.Add($"| {c.Name, -21} {c} {items.Where(x => x.Name.Equals(c.Name)).Count(), 4:###0} |");
                else contents.Add($"| No contents. {new string(' ', 42)}|");
            } return contents.ToArray();
        }

        public string[] ListMiscellaneous(bool details = false) {
            IEnumerable<Miscellaneous> miscellaneous = items.OfType<Miscellaneous>().GroupBy(m => m.Name).Select(m => m.First());
            List<string> contents = new List<string>();
            // string name, string desc, int value, double weight, int count
            if (details) {
                contents.Add($"| {"MISCELLANEOUS", -29} | {"DESCRIPTION", -58} | VALUE | WEIGHT | AMT. |");
                if (miscellaneous.Count() > 0) foreach (Miscellaneous m in miscellaneous) contents.Add($"| {m.Name, -29} | {m.Desc, -58} {m} {items.Where(x => x.Name.Equals(m.Name)).Count(), 4:###0} |");
                else contents.Add($"| No contents. {new string(' ', 102)}|");
            } else {
                contents.Add($"| {"MISCELLANEOUS", -30} | VALUE | WEIGHT | AMT. |");
                if (miscellaneous.Count() > 0) foreach (Miscellaneous m in miscellaneous) contents.Add($"| {m.Name, -30} {m} {items.Where(x => x.Name.Equals(m.Name)).Count(), 4:###0} |");
                else contents.Add($"| No contents. {new string(' ', 42)}|");
            } return contents.ToArray();
        }

        public string[] ListContents(string name, string info, bool details = false) {
            List<string> contents = new List<string>();
            contents.Add(details ? $"{name, -59}{info, 59}" : $"{name, -29}{info, 29}");
            //contents.Add(details ? $"|{new string('-', 116)}|" : $"|{new string(' ', 56)}|");
            contents.AddRange(ListWieldables(details));
            contents.Add(details ? $"|{new string('-', 116)}|" : $"|{new string(' ', 56)}|");
            contents.AddRange(ListConsumables(details));
            contents.Add(details ? $"|{new string('-', 116)}|" : $"|{new string(' ', 56)}|");
            contents.AddRange(ListMiscellaneous(details));
            return contents.ToArray();
        }
    }
}