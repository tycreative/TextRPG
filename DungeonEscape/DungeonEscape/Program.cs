using System;
using System.Collections.Generic;

namespace DungeonEscape {
    class Program {
        static void Main(string[] args) {
            Game game = new Game();
            game.Start();
            game.Run();
            game.End();
        }
    }
}
