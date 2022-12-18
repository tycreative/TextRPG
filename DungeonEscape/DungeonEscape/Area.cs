using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    // Areas act like rooms that the player can visit, exits connect to other areas, items can be taken/dropped
    public class Area : Entity {
        private Dictionary<string, Exit> exits;
        private Inventory inventory;
        private AreaDelegate type;
        private bool visited;

        public Inventory Inventory { get { return inventory; } set { inventory = value; } }
        public AreaDelegate Type { get { return type; } set { type = value; } }
        public bool Visited { get { return visited; } set { visited = value; } }

        public Area(string name, string desc, Inventory inventory = null) : base(name, desc) {
            exits = new Dictionary<string, Exit>();
            this.inventory = inventory != null ? inventory : new Inventory();
            type = null;
            visited = false;
        }

        // Change player state upon entry
        public void Enter(Player player, string dir) {
            visited = true;
            if (type != null) type.Enter(player);
            else player.State = new NormalState(player);
            Display.Success($"Moved {dir.ToUpper()} to {Name}." + (type != null ? $" {type.Desc}" : ""));
        }

        // Set possible exits at once
        public void SetExits(Area above, Area below, Area right, Area left) {
            if (above != null) exits["north"] = new Exit(this, above, "north");
            if (below != null) exits["south"] = new Exit(this, below, "south");
            if (right != null) exits["east"] = new Exit(this, right, "east");
            if (left != null) exits["west"] = new Exit(this, left, "west");
        }

        // Set specific exit in given direction
        public void SetExit(string dir, Exit exit) {
            exits[dir] = exit;
        }

        // Lock exit in given direction
        public void LockExit(string dir) {
            Exit exit = GetExit(dir);
            if (exit != null) exit.Locked = true;
        }

        // Return exit in given direction
        public Exit GetExit(string dir) {
            exits.TryGetValue(dir.ToLower().Trim(), out var exit);
            return exit;
        }

        // Returns string list of available exits
        public string[] GetExits() {
            List<string> output = new List<string>();
            output.Add("Exits");
            foreach (string dir in Globals.DIRECTIONS.Keys) if (exits.ContainsKey(dir)) output.Add(dir.ToUpper() + " to " + exits[dir].FormatExit(this));
            return output.ToArray();
        }
    }
}