using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public static class Creator {
        // Items
        private static Item[] items = new Item[] {
            new Wieldable("Stone Sword", "A simple sword made of stone.", 15, 15, 10),
            new Wieldable("Iron Sword", "A sturdy sword made of iron.", 20, 30, 15),
            new Wieldable("Steel Sword", "A robust sword made of steel.", 24, 60, 30),
            new Wieldable("Titanium Sword", "A strong sword made of titanium.", 20, 75, 50),
            new Consumable("Simple Potion", "Potion that restores 10 health.", 0.5, 10),
            new Consumable("Normal Potion", "Potion that restores 25 health.", 1, 25),
            new Consumable("Strong Potion", "Potion that restores 50 health.", 1.8, 50),
            new Consumable("Ultra Potion", "Potion that restores 100 health.", 2.5, 100),
            new Miscellaneous("Dusty Book", "An old book covered in dust.", 1.4, 2, true),
            new Miscellaneous("Rusty Nail", "Just an iron nail rusted through.", 0.6, 1, true),
            new Miscellaneous("Goblet", "An old goblet used for drinking.", 3.4, 5, true),
            new Miscellaneous("Amulet", "A small piece of jewelry.", 1.4, 15, true),
            new Miscellaneous("Necklace", "A fancy necklace of jewels.", 6.8, 20, true),
            new Miscellaneous("Skull", "A skull of some kind, possibly human.", 14, 2, false),
            new Miscellaneous("Bone", "An old and fractured bone.", 3.4, 1, false),
            new Miscellaneous("Shard", "A discarded shard of something.", 4.5, 2, false)
        };
        public static Item[] Items { get { return items; } }
        // Generate given amount of items (common < 50 value, rare < 70 value, epic >= 70)
        public static List<Item> GenItems(int count, int value, bool decor = false) {
            List<Item> generated = new List<Item>();
            while (generated.Count < count) {
                Item item = items[Globals.RANDOM.Next(items.Length)];
                if (item.Value < value && (item.Takeable || decor)) generated.Add(item);
            } return generated;
        }

        // Areas
        private static Area[] areas = new Area[] {
            new Area("Exit", "Dungeon Exit"),                                                           // 0
            new Area("Chamber", "An old chamber with a huge double door to the west."),                 // 1
            new Area("Tower", "A large, circular room that seems to stretch to the heavens."),          // 2
            new Area("Old Corridor", "A narrow and dimly lit corridor that is slowly decaying away."),  // 3
            new Area("Courtyard", "A small courtyard smothered by a thick fog."),                       // 4
            new Area("Lair", "An eerie, compact room with old books and vials scattered about."),       // 5
            new Area("Office", "An oval room with the remnants of chairs and bookcases strewn about."), // 6
            new Area("Library", "A vast room with empty, crumbling bookshelves reaching to the sky."),  // 7
            new Area("Hallway", "A wide, dimly lit hallway with torn tapestries on either side."),      // 8
            new Area("Solitary Cell", "A small, cold room with very little light."),                    // 9
            new Area("Holding Cell", "An old, damp cell with decaying walls."),                         // 10
            new Area("Lavatory", "A disgusting and cramped room with odd noises and smells.")           // 11
        };
        public static Area[] Areas { get { return areas; } }

        // Monsters
        private static Dictionary<string, string> monsterTypes = new Dictionary<string, string>() {
            { "Golem", "A solid creature of stone and wood." },
            { "Troll", "An ugly monster with giant, hairy arms." },
            { "Beast", "A mighty beast with glowing red eyes." },
            { "Cyclop", "A large creature with one big piercing eye." },
            { "Spider", "An oddly big creepy crawler with eight legs." },
            { "Demon", "A devilish beast with sharp fangs and hot breath." }
        };
        private static List<string> monsters = new List<string>(monsterTypes.Keys);
        // Generate new monster with given reference level
        public static Monster GenMonster(int level, Area area) {
            string name = monsters[Globals.RANDOM.Next(monsters.Count)];
                                // name         desc        hp          experience  strength    coins
            List<Item> contents = GenItems(1, level);
            contents.Add(new Miscellaneous("Key", "A simple key for opening doors.", 0.7, 15, true));
            return new Monster(name, monsterTypes[name], level, level / 15, level / 5, level / 4, area, new Inventory(contents));
        }
    }
}