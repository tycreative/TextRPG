using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class Room : Entity {
        private Dictionary<string, Door> exits;
        private Inventory inventory;
        private Character character;
        private bool visited;

        public Inventory Inventory {
            get { return inventory; }
        }

        public Character Character {
            get { return character; }
            set { character = value; }
        }

        public bool Visited {
            get { return visited; }
            set { visited = value; }
        }

        public Room(string name, string desc) : base(name, desc) {
            exits = new Dictionary<string, Door>();
            inventory = new Inventory();
            visited = false;
        }

        public void SetExits(Room above, Room below, Room right, Room left) {
            if (above != null) exits["north"] = new Door(this, above, "north");
            if (below != null) exits["south"] = new Door(this, below, "south");
            if (right != null) exits["east"] = new Door(this, right, "east");
            if (left != null) exits["west"] = new Door(this, left, "west");
        }

        public void SetExit(string dir, Door door) {
            exits[dir] = door;
        }

        public void LockDoor(string dir) {
            Door door = GetExit(dir);
            if (door != null) door.Locked = true;
        }

        public Door GetExit(string dir) {
            exits.TryGetValue(dir.ToLower().Trim(), out var door);
            return door;
        }

        public string[] FormatExits() {
            List<string> output = new List<string>();
            output.Add("Exits");
            foreach (string dir in Globals.DIRECTIONS.Keys) if (exits.ContainsKey(dir)) output.Add(dir.ToUpper() + " to " + exits[dir].FormatExit(this));
            return output.ToArray();
        }
    }
}
