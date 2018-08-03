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

        internal void PlayGame(Func<string, string> inputWinner, out string winner, out string loser)
        {
            var side = DetectWinner(inputWinner,_leftParticipant, _rightParticipant);

            switch (side)
            {
                case Side.Left:
                    winner = _leftParticipant.Name;
                    loser = _rightParticipant.Name;
                    break;
                case Side.Right:
                    winner = _rightParticipant.Name;
                    loser = _leftParticipant.Name;
                    break;
                default:
                    winner = null;
                    loser = null;
                    break;
            }
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