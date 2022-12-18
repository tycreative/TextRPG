using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static System.Console;

namespace DungeonEscape {
    public static class Display {
        private static readonly int WIDTH = Globals.SCREEN_WIDTH - (Globals.HORIZONTAL_MARGINS * 2);
        private static readonly int HEIGHT = Globals.SCREEN_HEIGHT - (Globals.VERTICAL_MARGINS * 2);
        private static readonly string MARGIN = new string(' ', Globals.HORIZONTAL_MARGINS);

        private static string title = "";
        private static string subtitle = "";
        private static ConsoleColor titleColor = ConsoleColor.Gray;
        private static ConsoleColor subtitleColor = ConsoleColor.Gray;

        // Update title (top) message and color
        public static void Title(string str, ConsoleColor color) {
            title = str;
            titleColor = color;
        }

        // Update subtitle (bottom) message and color
        public static void Subtitle(string str, ConsoleColor color) {
            subtitle = str;
            subtitleColor = color;
        }

        // Display title and subtitle message(s)
        public static void Render() {
            ForegroundColor = titleColor;
            WriteLine(MARGIN + title);
            ForegroundColor = subtitleColor;
            WriteLine(MARGIN + subtitle);
            ResetColor();
        }

        // Display given string
        public static void Output(string str) {
            WriteLine(MARGIN + str);
        }

        // Update subtitle as an info message
        public static void Info(string str) { Subtitle(str, ConsoleColor.White); }

        // Update subtitle as a success message
        public static void Success(string str) { Subtitle(str, ConsoleColor.Green); }

        // Update subtitle as a warning message
        public static void Warning(string str) { Subtitle(str, ConsoleColor.Yellow); }

        // Update subtitle as as error message
        public static void Error(string str) { Subtitle(str, ConsoleColor.Red); }

        // Display a horizontal line of provided char, default of space
        public static void Span(char pattern = ' ') {
            WriteLine(MARGIN + new string(pattern, WIDTH));
        }

        // Display information about a Character (info on first line, healthbar on second line)
        public static void Character(Character character) {
            // Character info
            ForegroundColor = character.Color;
            WriteLine(MARGIN + character);
            // Character healthbar
            Write(MARGIN + "[");
            BackgroundColor = ConsoleColor.Green;
            if (character.HP / character.Health < 0.25) BackgroundColor = ConsoleColor.Red;
            else if (character.HP / character.Health < 0.5) BackgroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < character.HP; i += 10) Write(" ");
            BackgroundColor = ConsoleColor.Black;
            for (int i = (int)character.HP; i < character.Health; i += 10) Write(" ");
            Write($"] ({character.FormatHealth})\n");
            ResetColor();
        }

        // Display content as two columns, i.e. 'splitscreen'
        public static void SplitScreen(string[] left, string[] right, int bottom = 11) {
            for (int i = 0; i < Math.Max(HEIGHT - bottom, Math.Max(left.Length, right.Length)); i++) {
                Write(MARGIN);
                Write(String.Format($"{{0, {-WIDTH / 2}}}", left.ElementAtOrDefault(i) != null ? left[i] : ""));
                Write(String.Format($"{{0, {-WIDTH / 2}}}", right.ElementAtOrDefault(i) != null ? right[i] : ""));
                Write("\n");
            }
        }

        // Display content as one column
        public static void FullScreen(string[] content, int bottom = 11) {
            for (int i = 0; i < Math.Max(HEIGHT - bottom, content.Length); i++) {
                if (content.ElementAtOrDefault(i) != null) WriteLine(MARGIN + content[i]);
                else WriteLine();
            }
        }
    }
}