using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTournament
{
    public static class ConsoleWorker
    {
        public static void StartedNewTournament()
        {
            Console.WriteLine("New tournament started!\n");
        }

        public static int EnterCountOfPlayers()
        { 
            Console.Write("Enter count of players: ");

            var count = -1;

            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Incorrect data! Try again: ");
                Console.Write("Enter count of players: ");
            }

            return count;
        }

        public static void EnterPlayerNames()
        {
            Console.WriteLine("\nEnter names of players:");
        }

        public static string EnterPlayerName(HashSet<string> existingNames, int index)
        {
            Console.Write($"{index}. ");

            var name = Console.ReadLine();

            while (existingNames.Contains(name) || name == " ")
            {
                Console.WriteLine("Player with this name already exists. Try again: ");
                Console.Write($"{index}. ");
                name = Console.ReadLine();
            }

            return name;
        }

        public static int EnterPlayerScore(Player player)
        {
            Console.Write($"{player.Name} scores: ");

            var score = -1;

            while (!int.TryParse(Console.ReadLine(), out score) || score < 0)
            {
                Console.WriteLine("Incorrect data! Try again: ");
                Console.Write($"{player.Name} scores: ");
            }

            return score;
        }

        public static void DrawIsNotPossible()
        {
            Console.WriteLine("There is can't be a draw. Try again:");
        }

        public static void PrintGameResult(Game game)
        {
            Console.WriteLine($"\n{game.Result()}\n");
        }

        public static void PrintGrandFinal(Game final)
        {
            Console.WriteLine("GRAND FINAL");
            PrintGameResult(final);
        }

        public static void PrintChampion(Player champion)
        {
            Console.WriteLine($"Tournament is finished. {champion.Name} is a champion!");
        }
    }
}
