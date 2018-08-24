using System;
using System.Collections.Generic;
using System.IO;
using Tournament;

namespace ConsoleTournament
{
    public class ConsoleTournament
    {
        private const int _maxNameLength = 8;

        public enum EliminationSystem
        {
            Double,
            Single
        }

        public enum BracketStyle
        {
            Horizontal,
            PlayOff
        }

        public delegate void PrintBracket(List<Participant> participant);

        public static void Main(string[] args)
        {
            bool isFromSavedFile = DataInput.IsFromSavedFile();
            EliminationSystem eliminationSystem = EliminationSystem.Single;

            if (isFromSavedFile)
            {
                if (File.Exists(BinarySaver.NameOfSinlgeEliminationFile))
                    eliminationSystem = EliminationSystem.Single;
                else if (File.Exists(BinarySaver.NameOfDoubleEliminationFile))
                    eliminationSystem = EliminationSystem.Double;
            }
            else
                eliminationSystem = DataInput.ChoseEliminationSystem();

            var bracketStyle = DataInput.ChoseBracket();
            PrintBracket print = null;

            switch (bracketStyle)
            {
                case BracketStyle.Horizontal:
                    print = BracketPrint.PrintHorizontalBracket;
                    break;
                case BracketStyle.PlayOff:
                    print = BracketPrint.PrintPlayOffBracket;
                    break;
            }

            if (eliminationSystem == EliminationSystem.Double)
                PlayDoubleElimination(isFromSavedFile, print);
            else
                PlaySingleElimination(isFromSavedFile, print);      
        }

        public static void PlaySingleElimination(bool isFromSavedFile, PrintBracket print)
        {
            SingleEliminationTournament tournament;

            if (isFromSavedFile)
                tournament = BinarySaver.LoadSingleFromBinnary();
            else
            {
                int amount = DataInput.InputAmount();
                var participants = DataInput.InputNames(amount, _maxNameLength);
                tournament = new SingleEliminationTournament(participants);
            }

            List<Participant> nextUpperBracketRound;

            while (!tournament.EndOfTheGame())
            {
                nextUpperBracketRound = tournament.GetBracket();
                Console.Clear();
                Console.WriteLine("----Upper Bracket----");
                print(nextUpperBracketRound);
                var meeting = tournament.GetPlayingParticipants();
                var side = DataInput.InputWinner(meeting);
                tournament.PlayGame(side);
            }

            nextUpperBracketRound = tournament.GetBracket();
            Console.Clear();
            Console.WriteLine("----Upper Bracket----");
            print(nextUpperBracketRound);
            Console.ReadLine();
        }

        public static void PlayDoubleElimination(bool isFromSavedFile, PrintBracket print)
        {
            DoubleEliminationTournament tournament;
            if (isFromSavedFile)
                tournament = BinarySaver.LoadDoubleFromBinnary();
            else
            {
                int amount = DataInput.InputAmount();
                var participants = DataInput.InputNames(amount, _maxNameLength);
                tournament = new DoubleEliminationTournament(participants);
            }

            List<Participant> nextUpperBracketRound;
            List<Participant> nextLowerBracketRound;

            while (!tournament.EndOfTheGame())
            {
                nextUpperBracketRound = tournament.GetBracket();
                Console.Clear();
                Console.WriteLine("----Upper Bracket----");
                print(nextUpperBracketRound);
                nextLowerBracketRound = tournament.GetLowerBracket();
                Console.WriteLine("----Lower Bracket----");
                print(nextLowerBracketRound);
                var meeting = tournament.GetPlayingParticipants();
                var side = DataInput.InputWinner(meeting);
                tournament.PlayGame(side);
            }

            nextUpperBracketRound = tournament.GetBracket();
            Console.Clear();
            Console.WriteLine("----Upper Bracket----");
            print(nextUpperBracketRound);
            Console.ReadLine();
        }

    }
}
