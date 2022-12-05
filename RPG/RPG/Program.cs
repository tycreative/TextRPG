using System;
using System.Collections.Generic;

namespace RPG {
    public class Program {
        

        static void Main(string[] args) {
            Console.SetWindowSize(Globals.SCREEN_WIDTH, Globals.SCREEN_HEIGHT);
            Console.ResetColor();

            Game game = new Game();
            game.Start();
            game.Play();
            game.End();
        }
    }
}