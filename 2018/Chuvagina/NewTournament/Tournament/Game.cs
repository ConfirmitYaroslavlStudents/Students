using System;

namespace Tournament
{
    internal class Game
    {
        private enum Side
        {
            Left,
            Right
        }

        private readonly Participant _leftParticipant;
        private readonly Participant _rightParticipant;

        internal Game(Participant leftParticipant, Participant rightParticipant)
        {
            _leftParticipant = leftParticipant;
            _rightParticipant = rightParticipant;
        }

        internal void PlayGame(Func<string, string> inputWinner, out Participant winner, out Participant loser)
        {
            var side = DetectWinner(inputWinner,_leftParticipant, _rightParticipant);

            switch (side)
            {
                case Side.Left:
                    winner = new Participant(_leftParticipant.Name, _leftParticipant, _rightParticipant);
                    loser = new Participant(_rightParticipant.Name);
                    break;
                case Side.Right:
                    winner = new Participant(_rightParticipant.Name, _leftParticipant, _rightParticipant);
                    loser = new Participant(_leftParticipant.Name);
                    break;
                default:
                    winner = null;
                    loser = null;
                    break;
            }

            _leftParticipant.SetWinner(winner);
            _rightParticipant.SetWinner(winner);
        }

        private static Side DetectWinner(Func<string, string> inputWinner,Participant leftParticipant, Participant rightParticipant)
        {
            var name = "";

            do
            {
                name = inputWinner(
                    $"The winner between \"{leftParticipant.Name}\" and \"{rightParticipant.Name}\" is:  ");

            } while (name != null && (!name.Equals(leftParticipant.Name) && !name.Equals(rightParticipant.Name)));

            if (name.Equals(leftParticipant.Name))
                return Side.Left;

            return Side.Right;
        }
    }
}