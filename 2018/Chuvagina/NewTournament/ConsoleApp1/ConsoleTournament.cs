using System;
using System.Collections.Generic;
using Tournament;

namespace ConsoleTournament
{
    public class ConsoleTournament
    {
      
        public enum EliminationSystem
        {
            Double,
            Single
        }

        public enum BracketStyle
        {
            Horizontal,
            Vertical,
            PlayOff
        }

        public delegate void PrintBracket(List<Participant> participant, int round);

        public static void PlayDoubleElimination(List<string> participantList,PrintBracket print)
        {
            var Tournament = new DoubleEliminationTournament(participantList);
            List<Participant> nextUpperBracketRound;
            List<Participant> nextLowerBracketRound;
            int round = 0;
            bool isLastRound;

            do
            {
                nextUpperBracketRound = Tournament.GetUpperBracket();
                Console.Clear();
                Console.WriteLine("----Upper Bracket----");
                print(nextUpperBracketRound, round);
                Tournament.PlayUpperBracket();
                nextLowerBracketRound = Tournament.GetLowerBracket();
                Console.Clear();
                Console.WriteLine("----Lower Bracket----");

                print(nextLowerBracketRound, round);

                isLastRound = Tournament.PlayLowerBracket();
                round++;

            } while (!isLastRound);

            var upperBracket = Tournament.GetUpperBracket();
            Console.Clear();
            Console.WriteLine("----Upper Bracket----");
            print(upperBracket, round);
            Tournament.PlayLastRound();

            round++;
            upperBracket = Tournament.GetUpperBracket();
            Console.Clear();
            Console.WriteLine("----Upper Bracket----");
            print(upperBracket, round);
            Console.ReadLine();
        }


        public static void PlaySingleElimination(List<string> participantList, PrintBracket print)
        {
            var Tournament = new SingleEliminationTournament(participantList);
            List<Participant> nextUpperBracketRound;
            int round = 0;

            do
            {
                nextUpperBracketRound = Tournament.GetBracket();
                Console.Clear();
                Console.WriteLine("----Upper Bracket----");
                print(nextUpperBracketRound, round);
                Tournament.PlayRound();
                round++;

            } while (nextUpperBracketRound.Count!=1);

            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            var eliminationSystem = DataInput.ChoseEliminationSystem();
            var bracketStyle = DataInput.ChoseBracket();
            int amount = DataInput.InputAmount();
            var participantList = DataInput.InputNames(amount);
            PrintBracket print=null;
            switch (bracketStyle)
            {
                case BracketStyle.Horizontal:
                    print = HorizontalBracket.Print;
                    break;
                case BracketStyle.Vertical:
                    print = VerticalBracket.Print;
                    break;
                case BracketStyle.PlayOff:
                    print = ReverseBracket.Print;
                    break;
            }

            if (eliminationSystem == EliminationSystem.Double)
                PlayDoubleElimination(participantList, print);
            else
                PlaySingleElimination(participantList, print);
         

        }


      

    }
}
