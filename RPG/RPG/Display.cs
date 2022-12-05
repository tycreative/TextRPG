using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;

namespace RPG {
    public static class Display {
        private static int WIDTH = Globals.SCREEN_WIDTH - (Globals.HORIZONTAL_MARGINS * 2);
        private static int HEIGHT = Globals.SCREEN_HEIGHT - (Globals.VERTICAL_MARGINS * 2);
        private static string MARGIN = new string(' ', Globals.HORIZONTAL_MARGINS);

        private static DisplayState displayState = new ScanState();
        private static ConsoleColor color = ConsoleColor.Gray;
        private static string action = "";
        
        public static DisplayState DisplayState {
            get { return displayState; }
            set { displayState = value; }
        }

        public static void Output() {
            ForegroundColor = color;
            WriteLine(MARGIN + action);
            ResetColor();
        }

        public static void Info(string info) {
            action = info;
            color = ConsoleColor.Gray;
        }

        public static void Success(string success) {
            action = success;
            color = ConsoleColor.Green;
        }

        public static void Warning(string warning) {
            action = warning;
            color = ConsoleColor.Yellow;
        }

        public static void Error(string error) {
            action = error;
            color = ConsoleColor.DarkRed;
        }

        public static void InfoBar(string info, ConsoleColor color = ConsoleColor.Gray) {
            ForegroundColor = color;
            WriteLine(MARGIN + info);
            ResetColor();
        }

        public static void HealthBar(double health, double maxHealth) {
            Write(MARGIN + "[");
            BackgroundColor = ConsoleColor.Green;
            if (health / maxHealth < 0.25) BackgroundColor = ConsoleColor.Red;
            else if (health / maxHealth < 0.5) BackgroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < health; i += 10) Write(" ");
            BackgroundColor = ConsoleColor.Black;
            for (int i = (int)health; i < maxHealth; i += 10) Write(" ");
            Write($"] ({health, 0:##0.00}/{maxHealth, 0:##0.00})\n");
        }

        public static void HorizontalBar(char pattern) {
            WriteLine(MARGIN + new string(pattern, WIDTH));
        }

        public static void FullView(string[] content, int padding = 10) {
            for (int i = 0; i < Math.Max(HEIGHT - padding, content.Length); i++) {
                if (content.ElementAtOrDefault(i) != null) WriteLine(MARGIN + content[i]);
                else WriteLine();
            }
        }

        public static void SplitView(string[] left, string[] right) {
            int side = WIDTH / 2;
            for (int i = 0; i < Math.Max(HEIGHT - 10, Math.Max(left.Length, right.Length)); i++) {
                Write(MARGIN);
                Write(String.Format($"{{0, {-side}}}", left.ElementAtOrDefault(i) != null ? left[i] : ""));
                Write(String.Format($"{{0, {-side}}}", right.ElementAtOrDefault(i) != null ? right[i] : ""));
                Write("\n");
            }
        }
    }
}
