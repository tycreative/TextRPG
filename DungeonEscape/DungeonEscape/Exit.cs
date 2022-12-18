using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public class Exit {
        private Area entrance;
        private Area exit;
        private bool locked;

        public bool Locked { get { return locked; } set { locked = value; } }

        public Exit(Area entrance, Area exit, string dir, bool locked = false) {
            entrance.SetExit(dir.ToLower(), this);
            exit.SetExit(Globals.DIRECTIONS[dir.ToLower()], this);
            this.entrance = entrance;
            this.exit = exit;
            this.locked = locked;
        }

        // Return area on other side
        public Area OtherSide(Area area) {
            return area == entrance ? exit : entrance;
        }

        // Format exit for display
        public string FormatExit(Area area) {
            Area exit = OtherSide(area);
            if (exit.Visited) return exit.Name + (locked ? " (Locked" : "");
            return "???" + (locked ? " (Locked)" : "");
        }
    }
}
