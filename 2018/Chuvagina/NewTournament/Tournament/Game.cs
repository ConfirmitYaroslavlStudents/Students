using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament
{
    internal class Game
    {
        private enum _side
        {
            Left,
            Right
        }

        private Participant _leftParticipant;
        private Participant _rightParticipant;

        internal Game(Participant leftParticipant, Participant rightParticipant )
        {
            _leftParticipant = leftParticipant;
            _rightParticipant = rightParticipant;
        }

        internal void PlayGame(out Participant winner, out Participant loser)
        {
            
            var side = DetectWinner(_leftParticipant, _rightParticipant);
            switch (side)
            {
                case _side.Left:
                    winner = new Participant(_leftParticipant.Name,_leftParticipant,_rightParticipant);
                    loser = new Participant(_rightParticipant.Name);
                    break;
                case _side.Right:
                    winner = new Participant(_rightParticipant.Name, _leftParticipant, _rightParticipant);
                    loser = new Participant(_leftParticipant.Name); 
                    break;
                default:
                    winner = null;
                    loser = null;
                    break;
            }
            _leftParticipant.Winner = winner;
            _rightParticipant.Winner = winner;
        }

        private _side DetectWinner(Participant leftParticipant, Participant rightParticipant)
        {
            string name = "";
            do
            {
                Console.Write("The winner between \"{0}\" and \"{1}\" is:  ", leftParticipant.Name, rightParticipant.Name);
                name = Console.ReadLine();
            } while (!name.Equals(leftParticipant.Name) && !name.Equals(rightParticipant.Name));
            
            if (name.Equals(leftParticipant.Name))
                return _side.Left;
            else
                return _side.Right;
        }
    }

}
