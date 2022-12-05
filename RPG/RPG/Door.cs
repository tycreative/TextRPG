using System;
using System.Collections.Generic;
using System.Text;

namespace RPG {
    public class Door {
        private Room roomA;
        private Room roomB;
        private bool locked;

        public bool Locked {
            get { return locked; }
            set { locked = value; }
        }

        public Door(Room roomA, Room roomB, string dir, bool locked = false) {
            roomA.SetExit(dir.ToLower(), this);
            roomB.SetExit(Globals.DIRECTIONS[dir.ToLower()], this);
            this.roomA = roomA;
            this.roomB = roomB;
            this.locked = locked;
        }

        public Room GetRoom(Room room) {
            return room == roomA ? roomB : roomA;
        }

        public string FormatExit(Room room) {
            Room r = GetRoom(room);
            if (r.Visited) return r.Name + (locked ? " (Locked)" : "");
            return "???" + (locked ? " (Locked)" : "");
        }
    }
}
