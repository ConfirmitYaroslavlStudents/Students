using System;

namespace TournamentLibrary
{
    public class ConsoleWorker : IViewer
    {
        public void StartedNewTournament()
        {
            Console.WriteLine("New tournament started!\n");
        }

        public int EnterCountOfPlayers()
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

        public void EnterPlayerNames()
        {
            Console.WriteLine("\nEnter names of players:");
        }

        public string EnterPlayerName(int index)
        {
            Console.Write($"{index}. ");

            var name = Console.ReadLine();

            return name;
        }

        public void NameAlreadyExists()
        {
            Console.WriteLine("Player with this name already exists. Try again: ");
        }

        public int EnterPlayerScore(Player player)
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

        public void DrawIsNotPossible()
        {
            Console.WriteLine("There is can't be a draw. Try again:");
        }

        public void PrintGameResult(Game game)
        {
            Console.WriteLine($"\n{game.Result()}\n");
        }

        public void PrintGrandFinal(Game final)
        {
            Console.WriteLine("GRAND FINAL");
            PrintGameResult(final);
        }

        public void PrintChampion(Player champion)
        {
            Console.WriteLine($"Tournament is finished. {champion.Name} is a champion!");
        }
    }
}
