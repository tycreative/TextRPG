using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class Player : Character {
        private PlayerState state;
        private double capacity;

        public PlayerState State { get { return state; } set { state = value; } }
        public double Capacity { get { return capacity; } }
        public string FormatCapacity { get { return $"{Inventory.Weight, 3:##,##0.00}/{capacity, 3:##,##0.00}"; } }

        public Player(string name, Area area, Inventory inv, double capacity) : base(name, "The player.", 100, 0, 10, 100, area, inv) {
            this.area.Visited = true;
            this.capacity = capacity;
            state = new NormalState(this);
        }

        // Move in a specified direction (MoveCommand)
        public void Move(string dir) {
            Exit exit = area.GetExit(dir);
            if (exit != null) {
                if (!exit.Locked) {
                    Area move = exit.OtherSide(area);
                    if (move != null) {
                        NotificationCenter.Instance.PostNotification(new Notification("PlayerMoving")); // Tell World to move monsters
                        base.Move(move);
                        area.Enter(this, dir);
                        NotificationCenter.Instance.PostNotification(new Notification("PlayerMoved")); // Tell Game player moved (check for reaching exit)
                    } else Display.Warning("Exit does not lead anywhere.");
                } else Display.Warning("Exit in the specified direction is locked.");
            } else Display.Warning("Unable to find exit in the specified direction.");
        }

        // Unlock exit in a specified direction (UnlockCommand)
        public void Unlock(string dir) {
            Exit exit = area.GetExit(dir);
            if (exit != null) {
                if (exit.Locked) {
                    Item key = inv.Find("key");
                    if (key != null) {
                        if (inv.Remove(key)) {
                            exit.Locked = false;
                            Display.Success("Exit has been unlocked, key was consumed in use.");
                        } else Display.Warning("Unable to unlock exit.");
                    } else Display.Warning("You need a key to unlock.");
                } else Display.Warning("Exit in the specified direction is not locked.");
            } else Display.Warning("Unable to find exit in the specified direction.");
        }

        // Take an item from an area (TakeCommand)
        public void Take(string name) {
            Item item = area.Inventory.Find(name);
            if (item != null) {
                if (item.Takeable) {
                    if (item.Weight + inv.Weight <= capacity) {
                        if (area.Inventory.Remove(item) && inv.Add(item)) {
                            Display.Success($"Added '{item.Name}' to inventory.");
                        } else Display.Warning($"Unable to add '{item.Name}' to inventory.");
                    } else Display.Warning("Item is too heavy to take.");
                } else Display.Warning("This item cannot be taken.");
            } else Display.Warning("Unable to find the specified item.");
        }

        // Drop an item into an area (DropCommand)
        public void Drop(string name) {
            Item item = inv.Find(name);
            if (item != null) {
                if (inv.Remove(item) && area.Inventory.Add(item)) {
                    Display.Success($"Dropped '{item.Name}' from inventory.");
                } else Display.Warning($"Unable to drop '{item.Name}' from inventory.");
            } else Display.Warning("Unable to find the specified item.");
        }

        // Use an item (UseCommand)
        public bool Use(string name = "", Character foe = null) {
            Item item = inv.Find(name);
            if (name == "") item = new Wieldable("Fists", "Your bare fists.", 0, 0, 999); // Use player strength as damage
            if (item != null) item.Use(this, foe);
            return item != null;
        }

        // Upgrade a specific stat (UpgradeCommand)
        public void Upgrade(string stat) {
            if (exp > 0) {
                if (string.Equals(stat, "health", StringComparison.OrdinalIgnoreCase)) {
                    exp -= 1;
                    health += 10;
                    hp += 10;
                    Display.Success("Increased health by 10 points.");
                } else if (string.Equals(stat, "capacity", StringComparison.OrdinalIgnoreCase)) {
                    exp -= 1;
                    capacity += 5;
                    Display.Success("Increased carrying capacity by 10 points.");
                } else if (string.Equals(stat, "strength", StringComparison.OrdinalIgnoreCase)) {
                    exp -= 1;
                    str += 1;
                    Display.Success("Increased strength by 1 point.");
                } else Display.Warning("Unable to find specified stat, available stats are 'health', 'capacity', and 'strength'.");
            } else Display.Warning("Insufficient experience points available.");
        }

        public override string ToString() { 
            return $"{Name, -27}Experience: {exp, -12:##0}Strength: {str, -12:##0}Capacity: {FormatCapacity, -24}{$"Coins: {FormatCoins}", 10}";
        }
    }
}