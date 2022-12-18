using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonEscape {
    public class Inventory {
        private List<Item> items;
        private int count;

        // Return total inventory weight
        public double Weight => items.Sum(i => i.Weight);
        public List<Item> Items { get { return items; } }

        public Inventory() {
            items = new List<Item>();
            count = 0;
        }

        public Inventory(List<Item> items) {
            this.items = items;
            count = items.Count;
        }

        // Adds an item to the inventory and checks if item was added
        public bool Add(Item item) {
            items.Add(item);
            if (items.Count != count + 1) return false;
            count += 1;
            return true;
        }

        // Removes an item from the inventory and checks if item was removed
        public bool Remove(Item item) {
            items.Remove(item);
            if (items.Count != count - 1) return false;
            count -= 1;
            return true;
        }

        // Try to find item in inventory
        public Item Find(string name) {
            return items.Find(item => string.Equals(item.Name, name.Trim(), StringComparison.OrdinalIgnoreCase)); // Compare ignoring case
        }

        // Returns a formatted string list of the wieldables, optional details (for full screen)
        public string[] ListWieldables(bool details = false) {
            IEnumerable<Wieldable> wieldables = items.OfType<Wieldable>().OrderByDescending(w => w.Damage);
            List<string> contents = new List<string>();
            if (details) {
                contents.Add($"| {"WIELDABLE", -29} | {"DESCRIPTION", -49} | VALUE | DAMAGE | WEIGHT | USES |");
                foreach (Wieldable w in wieldables) contents.Add($"| {w.Name, -29} | {w.Desc, -49} {w}");
            } else {
                contents.Add($"| {"WIELDABLE", -21} | VALUE | DAMAGE | WEIGHT | USES |");
                foreach (Wieldable w in wieldables) contents.Add($"| {w.Name, -21} {w}");
            }
            if (wieldables.Count() == 0) contents.Add($"| No contents. {new string(' ', details ? 102 : 42)}|");
            return contents.ToArray();
        }

        // Returns a formatted string list of the consumables, optional details (for full screen)
        public string[] ListConsumables(bool details = false) {
            IEnumerable<Consumable> consumables = items.OfType<Consumable>().GroupBy(c => c.Name).Select(c => c.First());
            List<string> contents = new List<string>();
            if (details) {
                contents.Add($"| {"CONSUMABLE", -29} | {"DESCRIPTION", -49} | VALUE | HEALTH | WEIGHT | AMT. |");
                foreach (Consumable c in consumables) contents.Add($"| {c.Name, -29} | {c.Desc, -49} {c} {items.Where(x => x.Name.Equals(c.Name)).Count(), 4:###0} |");
            } else {
                contents.Add($"| {"CONSUMABLE", -21} | VALUE | HEALTH | WEIGHT | AMT. |");
                foreach (Consumable c in consumables) contents.Add($"| {c.Name, -21} {c} {items.Where(x => x.Name.Equals(c.Name)).Count(), 4:###0} |");
            }
            if (consumables.Count() == 0) contents.Add($"| No contents. {new string(' ', details ? 102 : 42)}|");
            return contents.ToArray();
        }

        // Returns a formatted string list of the miscellaneous items, optional details (for full screen)
        public string[] ListMiscellaneous(bool details = false) {
            IEnumerable<Miscellaneous> miscellaneous = items.OfType<Miscellaneous>().GroupBy(m => m.Name).Select(m => m.First());
            List<string> contents = new List<string>();
            if (details) {
                contents.Add($"| {"MISCELLANEOUS", -29} | {"DESCRIPTION", -58} | VALUE | WEIGHT | AMT. |");
                foreach (Miscellaneous m in miscellaneous) contents.Add($"| {m.Name, -29} | {m.Desc, -58} {m} {items.Where(x => x.Name.Equals(m.Name)).Count(), 4:###0} |");
            } else {
                contents.Add($"| {"MISCELLANEOUS", -30} | VALUE | WEIGHT | AMT. |");
                foreach (Miscellaneous m in miscellaneous) contents.Add($"| {m.Name, -30} {m} {items.Where(x => x.Name.Equals(m.Name)).Count(), 4:###0} |");
            }
            if (miscellaneous.Count() == 0) contents.Add($"| No contents. {new string(' ', details ? 102 : 42)}|");
            return contents.ToArray();
        }

        // Returns a formatted string list of all inventory contents, optional details (for full screen)
        public string[] ListContents(string leftTitle, string rightTitle, bool details = false) {
            List<string> contents = new List<string>();
            contents.Add(details ? $"{leftTitle, -59}{rightTitle, 59}" : $"{leftTitle, -29}{rightTitle, 29}");
            contents.AddRange(ListWieldables(details));
            contents.Add(details ? $"|{new string('-', 116)}|" : $"|{new string(' ', 56)}|");
            contents.AddRange(ListConsumables(details));
            contents.Add(details ? $"|{new string('-', 116)}|" : $"|{new string(' ', 56)}|");
            contents.AddRange(ListMiscellaneous(details));
            return contents.ToArray();
        }
    }
}
