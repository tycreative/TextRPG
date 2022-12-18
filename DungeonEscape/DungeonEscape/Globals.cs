using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEscape {
    public static class Globals {
        public static readonly int SCREEN_WIDTH = 120;
        public static readonly int SCREEN_HEIGHT = 30;
        public static readonly int VERTICAL_MARGINS = 0;
        public static readonly int HORIZONTAL_MARGINS = 1;
        public static readonly Random RANDOM = new Random();
        public static readonly List<string> COMPASS = new List<string> { "north", "south", "east", "west" };
        public static readonly Dictionary<string, string> DIRECTIONS = new Dictionary<string, string>{ 
            { "north", "south" },
            { "south", "north" },
            { "east", "west" },
            { "west", "east" }
        };
    }
}