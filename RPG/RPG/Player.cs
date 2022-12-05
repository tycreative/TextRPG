using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class Player : Character {
        private Stack<string> backTrack;
        private double capacity;

        public string FormatCapacity {
            get { return $"{inventory.Weight, 3:##,##0.00} / {capacity, 3:##,##0.00}"; }
        }

        public Player(string name, Room room, double capacity) : this(name, room, new Inventory(), capacity) { }

        public Player(string name, Room room, Inventory inventory, double capacity) : base(name, "The player.", null, inventory, 100, 0, 10, 100) {
            Room = room;
            backTrack = new Stack<string>();
            this.capacity = capacity;
        }

        // Used for refresh, checking state (RefreshCommand)
        public void Status() { // TODO: rework this
            if (Room.Character != null && Room.Character is Enemy && Room.Character.Health > 0) {
                Parser.FightingState();
                Display.DisplayState = new FightingState();
                Display.Error("An enemy engages you! Type 'help' for more options.");
            } else {
                Parser.NormalState();
                Display.DisplayState = new ScanState();
                Display.Info("What would you like to do? Type 'help' for more options.");
            }
        }

        // Used to move the player in a specified direction (MoveCommand)
        public void Move(string direction, bool backtracking = false) {
            Door door = Room.GetExit(direction);
            if (door != null) {
                if (!door.Locked) {
                    Room room = door.GetRoom(Room);
                    if (room != null) {
                        // send out notification that player is moving
                        // move enemies before moving player
                        NotificationCenter.Instance.PostNotification(new Notification("PlayerMoving"));
                        Room = room;
                        Room.Visited = true;
                        if (!backtracking) backTrack.Push(Globals.DIRECTIONS[direction.ToLower()]);
                        Status();
                        if (!(Room.Character != null && Room.Character is Enemy)) Display.Success($"Moved {direction.ToUpper()} to {Room.Name}.");
                        NotificationCenter.Instance.PostNotification(new Notification("PlayerMoved", this));
                    } else Display.Warning("There is no room on other side of door.");
                } else Display.Warning("Door in specified direction is locked.");
            } else Display.Warning("Unable to find door in specified direction.");
        }

        // Used for backtracking (BackCommand)
        public void Back() {
            if (backTrack.Count > 0) {
                Move(backTrack.Pop(), true);
            } else Display.Warning("Cannot backtrack any further.");
        }

        // Used to unlock an exit in a specified direction (UnlockCommand)
        public void Unlock(string direction) {
            Door door = Room.GetExit(direction);
            if (door != null) {
                if (door.Locked) {
                    Item item = Inventory.Find("key");
                    if (item != null) {
                        if (Inventory.Remove(item.Name)) {
                            door.Locked = false;
                            Display.Success("Door has been unlocked.");
                        } else Display.Warning("Unable to unlock door.");
                    } else Display.Warning("Unable to unlock door without a key.");
                } else Display.Warning("Door in specified direction is not locked.");
            } else Display.Warning("Unable to find door in specified direction.");
        }

        // Upgrade a specific stat (LevelCommand)
        public void Level(string stat) {
            if (Experience > 0) {
                if (string.Equals(stat, "health", StringComparison.OrdinalIgnoreCase)) {
                    Experience -= 1;
                    Health += 10;
                    MaxHealth += 10;
                    Display.Success("Increased max health by 10 points.");
                } else if (string.Equals(stat, "capacity", StringComparison.OrdinalIgnoreCase)) {
                    Experience -= 1;
                    capacity += 10;
                    Display.Success("Increased carrying capacity by 10 points.");
                } else if (string.Equals(stat, "strength", StringComparison.OrdinalIgnoreCase)) {
                    Experience -= 1;
                    Strength += 1;
                    Display.Success("Increased strength by 1 point.");
                } else Display.Warning("Unable to find specified stat, available stats are 'health', 'capacity', and 'strength'.");
            } else Display.Warning("No experience points available.");
        }

        // Used to pickup an item from an area (TakeCommand)
        public void Take(string name) {
            Item item = Room.Inventory.Find(name);
            if (item != null) {
                if (item.Interactable) {
                    if (item.Weight + inventory.Weight <= capacity) {
                        if (Inventory.Add(item) && Room.Inventory.Remove(item.Name)) {
                            Display.Success("Added '" + item.Name + "' to inventory.");
                        } else Display.Warning("Unable to add '" + item.Name + "' to inventory.");
                    } else Display.Warning("Item is too heavy to take.");
                } else Display.Warning("Item cannot be taken.");
            } else Display.Warning("Unable to find specified item.");
        }

        // Used to drop an item from inventory into an area (DropCommand)
        public void Drop(string name) {
            Item item = Inventory.Find(name);
            if (item != null) {
                if (Inventory.Remove(item.Name) && Room.Inventory.Add(item)) {
                    Display.Success("Dropped '" + item.Name + "' from inventory.");
                } else Display.Warning("Unable to drop '" + item.Name + "' from inventory.");
            } else Display.Warning("Unable to find specified item.");
        }

        // Use (if fighting and have no weapons)
        public void Use() { // TODO: rework this
            if (Room.Character != null && Room.Character is Enemy && Room.Character.Health > 0) {
                if (!Room.Character.Harm(Strength)) { // you killed enemy
                    Experience += Room.Character.Experience;
                    Coins += Room.Character.Coins;
                    Display.Success($"Damaged {Room.Character.Name} for {Strength} HP, killing it. You gained {Room.Character.Experience} experience and {Room.Character.Coins} coins.");               
                } else if (!Harm(Room.Character.Strength)) { // you died
                    Display.Error($"Damaged {Room.Character.Name} for {Strength} HP. {Room.Character.Name} damaged you for {Room.Character.Strength} HP, killing you.");
                    NotificationCenter.Instance.PostNotification(new Notification("PlayerDied"));
                } else Display.Info($"Damaged {Room.Character.Name} for {Strength} HP. {Room.Character.Name} damaged you for {Room.Character.Strength} HP.");
            } else Display.Info("You swing wildly at the air.");
        }

        // Use an item (UseCommand)
        public void Use(string name) {
            Item item = Inventory.Find(name);
            if (item != null) {
                item.Use(this);
            } else Display.Warning("Unable to find specified item.");
        }

        // Buy an item from a merchant (BuyCommand)
        public void Buy(string name) {
            if (Room.Character != null) {
                Item item = Room.Character.Inventory.Find(name);
                if (item != null) {
                    if (Coins >= item.Value) {
                        if (item.Weight + inventory.Weight <= capacity) {
                            if (Room.Character.Inventory.Remove(item.Name) && Inventory.Add(item)) {
                                Display.Success("Bought '" + item.Name + "' from merchant.");
                                Coins -= item.Value;
                            } else Display.Warning("Unable to buy '" + item.Name + "' from merchant.");
                        } else Display.Warning("Item is too heavy to take.");
                    } else Display.Warning("Insufficient funds.");
                } else Display.Warning("Unable to find specified item.");
            } else Display.Warning("There is no merchant in current room.");
        }

        // Sell an item to a merchant (SellCommand)
        public void Sell(string name) {
            if (Room.Character != null) {
                Item item = Inventory.Find(name);
                if (item != null) {
                    if (Room.Character.Coins >= item.Value) {
                        if (Inventory.Remove(item.Name) && Room.Character.Inventory.Add(item)) {
                            Display.Success("Sold '" + item.Name + "' to merchant.");
                            Room.Character.Coins -= item.Value / 2;
                            Coins += item.Value / 2;                            
                        } else Display.Warning("Unable to sell '" + item.Name + "' to merchant.");
                    } else Display.Warning("Merchant has insufficient funds."); 
                } else Display.Warning("Unable to find specified item.");
            } else Display.Warning("There is no merchant in current room.");
        }

        // Enter into trading with merchant (TradeCommand)
        public void Trade() {
            if (Room.Character != null && !(Room.Character is Enemy)) {
                Parser.TradingState();
                Display.DisplayState = new TradingState();
                Display.Info("Interacting with the Merchant, hit 'ENTER' to return to previous screen.");
            } else Display.Warning("There is no merchant in current room.");
        }

        // For outputting player information
        public override string ToString() {
            return $"{Name, -24}Experience: {Experience, -12:##0}Strength: {Strength, -11:##0}Capacity: {FormatCapacity, -20}Coins: {FormatCoins, -10}";
        }
    }
}