using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class World {
        private static World instance;
        private Area[] areas;
        private List<Monster> monsters;
        private List<Merchant> merchants;
        private Player player;

        // Singleton Design Pattern
        public static World Instance { get {
                if (instance == null) instance = new World();
                return instance;
            } }

        public Area Start { get { return areas[10]; } }
        public Area End { get { return areas[0]; } }
        public Player Player { set { player = value; } }

        public World() {
            NotificationCenter.Instance.AddObserver("PlayerMoving", PlayerMoving);
            // Areas
            areas = Creator.Areas;
            PopulateAreas();
            // Monsters
            monsters = new List<Monster>();
            monsters.Add(Creator.GenMonster(75, areas[11]));
            monsters.Add(Creator.GenMonster(100, areas[3]));
            monsters.Add(Creator.GenMonster(125, areas[2]));
            // Merchants
            merchants = new List<Merchant>();
            List<Item> items = Creator.GenItems(7, 100);
            foreach (Item item in Creator.Items) if (!(item is Miscellaneous)) items.Add(item);
            merchants.Add(new Merchant("Merchant", 1000, areas[7], new Inventory(items)));
        }

        // Connect areas together and populate them with items
        private void PopulateAreas() {
            areas[0].SetExits(null, null, areas[1], null);
            areas[1].SetExits(null, areas[4], areas[2], areas[0]);
            areas[1].LockExit("west");
            areas[2].SetExits(null, areas[5], null, areas[1]);

            areas[3].SetExits(null, areas[6], areas[4], null);
            areas[4].SetExits(areas[1], areas[7], areas[5], areas[3]);
            areas[5].SetExits(areas[2], null, null, areas[4]);

            areas[6].SetExits(areas[3], null, areas[7], null);
            areas[7].SetExits(areas[4], areas[10], areas[8], areas[6]);
            areas[8].SetExits(null, areas[11], null, areas[7]);

            areas[9].SetExits(null, null, areas[10], null);
            areas[10].SetExits(areas[7], null, areas[11], areas[9]);
            areas[10].LockExit("west");
            areas[11].SetExits(areas[8], null, null, areas[10]);
            // Populate each area with random items
            foreach (Area area in areas) area.Inventory = new Inventory(Creator.GenItems(Globals.RANDOM.Next(0, 4), Globals.RANDOM.Next(35, 75), true));
        }

        // Upon player moving, move monsters
        public void PlayerMoving(Notification notif) {
            monsters.RemoveAll(monster => !monster.Alive); // Remove any dead monsters & generate new monster if needed
            while (monsters.Count < 3) monsters.Add(Creator.GenMonster((int)player.Health + player.Strength, player.Area));

            foreach (Monster monster in monsters.ToArray()) {
                for (int i = 0; i < 2; i++) {
                    Exit exit = monster.Area.GetExit(Globals.COMPASS[Globals.RANDOM.Next(Globals.COMPASS.Count)]);
                    if (exit != null && !exit.Locked && exit.OtherSide(monster.Area).Type == null) {
                        monster.Move(exit.OtherSide(monster.Area));
                        break;
                    }
                }
            }
        }
    }
}
