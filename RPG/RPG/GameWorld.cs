using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class GameWorld {
        private static GameWorld instance;
        public static GameWorld Instance {
            get {
                if (instance == null) instance = new GameWorld();
                return instance;
            }
        }

        private Room leftBottom = new Room("Left bottom", "This is the left bottom room.");
        private Room entrance = new Room("Entrance", "This is the entrance, the starting point.");
        private Room rightBottom = new Room("Right bottom", "This is the right bottom room.");
        private Room leftMiddle = new Room("Left middle", "This is the left middle room.");
        private Room tradingRoom = new Room("Trading room", "This is the trading hall/room.");
        private Room rightMiddle = new Room("Right middle", "This is the right middle room.");
        private Room endRoom = new Room("End room", "This is the ending room.");
        private Room rightTop = new Room("Right top", "This is the right top room.");
        public Room Entrance {
            get { return entrance; }
        }
        public Room Exit {
            get { return endRoom; }
        }

        private Item[] weapons = new Item[] {
            new Wieldable("Stone Sword", "A simple sword made of stone.", 15, 10, 15, 10),
            new Wieldable("Iron Sword", "A sturdy sword made of iron.", 20, 25, 30, 15),
            new Wieldable("Steel Sword", "A robust sword made of steel.", 24, 50, 50, 25),
            new Wieldable("Titanium Sword", "A strong sword made of titanium.", 20, 100, 70, 35)
        };

        private Item[] potions = new Item[] {
            new Consumable("Simple Potion", "Potion that restores 10 health.", 0.5, 10, 10),
            new Consumable("Normal Potion", "Potion that restores 25 health.", 1, 25, 25),
            new Consumable("Strong Potion", "Potion that restores 50 health.", 1.8, 50, 50),
            new Consumable("Ultra Potion", "Potion that restores 100 health.", 2.5, 100, 100)
        };

        private Item[] decor = new Item[] {
            new Miscellaneous("Dusty Book", "An old book covered in dust.", 1.4, 2, true),
            new Miscellaneous("Rusty Nail", "Just an iron nail rusted through.", 0.6, 1, true),
            new Miscellaneous("Goblet", "An old goblet used for drinking.", 3.4, 5, true),
            new Miscellaneous("Amulet", "A small piece of jewelry.", 1.4, 15, true),
            new Miscellaneous("Necklace", "A fancy necklace of jewels.", 6.8, 20, true),
            new Miscellaneous("Skull", "A skull of some kind, possibly human.", 14, 2, false),
            new Miscellaneous("Bone", "An old and fractured bone.", 3.4, 1, false),
            new Miscellaneous("Shard", "A discarded shard of something.", 4.5, 2, false)
        };

        private Enemy monster;
        private Character merchant;
        
        public GameWorld() {
            SetExits();
            FillRooms();
            merchant = new Character("Merchant", "Trader who buys and sells wares.", tradingRoom, new Inventory(), 1000);
            monster = new Enemy("Monster", "An ugly creature with glowing red eyes.", endRoom, 100, 3, 11, 25); // 100 3 11 25
            NotificationCenter.Instance.AddObserver("PlayerMoving", PlayerMoving);

            foreach (Item item in weapons) merchant.Inventory.Add(item);
            foreach (Item item in potions) {
                merchant.Inventory.Add(item);
                merchant.Inventory.Add(item);
            }
            merchant.Inventory.Add(new Miscellaneous("Key", "A simple key for unlocking doors.", 1.6, 12, true));
        }

        public void SetExits() {
            leftBottom.SetExits(leftMiddle, null, entrance, null);
            entrance.SetExits(tradingRoom, null, rightBottom, leftBottom);
            entrance.LockDoor("West");
            rightBottom.SetExits(rightMiddle, null, null, entrance);

            leftMiddle.SetExits(null, leftBottom, tradingRoom, null);
            tradingRoom.SetExits(null, entrance, rightMiddle, leftMiddle);
            rightMiddle.SetExits(rightTop, rightBottom, null, tradingRoom);

            endRoom.SetExits(null, null, rightTop, null);
            rightTop.SetExits(null, rightMiddle, null, endRoom);
        }

        public void FillRooms() {
            Random random = new Random();
            Room[] rooms = new Room[] { leftBottom, entrance, rightBottom, leftMiddle, tradingRoom, rightMiddle, endRoom, rightTop };

            foreach (Room room in rooms) {
                if (random.Next(5) > 3) room.Inventory.Add(potions[random.Next(potions.Length)]);
                room.Inventory.Add(decor[random.Next(decor.Length)]);
            }
        }

        public void PlayerMoving(Notification notification) {
            monster.Room.Character = null;

            if (monster.Health > 0) {
                foreach (string dir in Globals.DIRECTIONS.Values) {
                    Door door = monster.Room.GetExit(dir);
                    if (door != null) {
                        Room room = door.GetRoom(monster.Room);
                        room.Character = monster;
                        monster.Room = room;
                        break;
                    }
                }
            }
        }
    }
}
