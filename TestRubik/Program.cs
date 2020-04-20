using System;
using System.Drawing;
using System.Linq;
using Colorful;
using Console = Colorful.Console;
using Rubik = RubikCube.Cube<System.Drawing.Color>;

namespace TestRubik
{
    class Program
    {
        static void Main(string[] args)
        {
            var moves = new[] {"L", "R", "U", "D", "F", "B", "L'", "R'", "U'", "D'", "F'", "B'", "L2", "R2", "U2", "D2", "F2", "B2" };
            var cube = new Rubik(new[]
            {
                Color.Green, Color.Red, Color.Blue, 
                Color.Orange, Color.Cornsilk, Color.Yellow
            });
            DrawCube(cube);

            string sequence = "";

            while (true)
            {
                Console.WriteLine(sequence, Color.Cyan);
                Console.Write("Choose a move [  ]\b\b\b", Color.White);
                var str = Console.ReadLine().ToUpper().Trim();

                if(str == string.Empty) break;

                if (moves.Contains(str))
                {
                    Console.Clear();

                    cube = cube.Move(str);
                    DrawCube(cube);

                    sequence += $" - {str}";
                }
                else
                {
                    Console.Clear();

                    DrawCube(cube);
                    Console.WriteLine("Wrong Move!!! :[", Color.DarkRed);
                }
            }
        }

        static void DrawCube(Rubik cube)
        {
            Console.WriteAscii("Rubik's Cube");
            var sqr = "\u25A0";
            var structure = Color.Azure;

            Func<(int, int, int)[], Formatter[]> form = arr => arr.Select(t => new Formatter(sqr, cube[t.Item1, t.Item2, t.Item3])).ToArray();

            Console.WriteLineFormatted("         |-------|", structure);
            Console.WriteLineFormatted("         | {0} {1} {2} |", structure, form(new[] {(0, 0, 4), (0, 1, 4), (0, 2, 4)}));
            Console.WriteLineFormatted("         | {0} {1} {2} |", structure, form(new[] {(1, 0, 4), (1, 1, 4), (1, 2, 4)}));
            Console.WriteLineFormatted("         | {0} {1} {2} |", structure, form(new[] {(2, 0, 4), (2, 1, 4), (2, 2, 4)}));
            Console.WriteLineFormatted(" |-------|-------|-------|-------| ", structure);
            Console.WriteLineFormatted(" | {0} {1} {2} | {3} {4} {5} | {6} {7} {8} | {9} {10} {11} |", structure, form(new[] {(0, 0, 3), (0, 1, 3), (0, 2, 3), (0, 0, 0), (0, 1, 0), (0, 2, 0), (0, 0, 1), (0, 1, 1), (0, 2, 1), (2, 2, 2), (2, 1, 2), (2, 0, 2)}));
            Console.WriteLineFormatted(" | {0} {1} {2} | {3} {4} {5} | {6} {7} {8} | {9} {10} {11} |", structure, form(new[] {(1, 0, 3), (1, 1, 3), (1, 2, 3), (1, 0, 0), (1, 1, 0), (1, 2, 0), (1, 0, 1), (1, 1, 1), (1, 2, 1), (1, 2, 2), (1, 1, 2), (1, 0, 2)}));
            Console.WriteLineFormatted(" | {0} {1} {2} | {3} {4} {5} | {6} {7} {8} | {9} {10} {11} |", structure, form(new[] {(2, 0, 3), (2, 1, 3), (2, 2, 3), (2, 0, 0), (2, 1, 0), (2, 2, 0), (2, 0, 1), (2, 1, 1), (2, 2, 1), (0, 2, 2), (0, 1, 2), (0, 0, 2)}));
            Console.WriteLineFormatted(" |-------|-------|-------|-------| ", structure);
            Console.WriteLineFormatted("         | {0} {1} {2} |", structure, form(new[] {(0, 0, 5), (0, 1, 5), (0, 2, 5)}));
            Console.WriteLineFormatted("         | {0} {1} {2} |", structure, form(new[] {(1, 0, 5), (1, 1, 5), (1, 2, 5)}));
            Console.WriteLineFormatted("         | {0} {1} {2} |", structure, form(new[] {(2, 0, 5), (2, 1, 5), (2, 2, 5)}));
            Console.WriteLineFormatted("         |-------|\n", structure);

            Console.ResetColor();

        }
    }
}