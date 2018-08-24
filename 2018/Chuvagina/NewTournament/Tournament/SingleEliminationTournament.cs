using System;
using System.Collections.Generic;

namespace Tournament
{
    [Serializable]
    public class SingleEliminationTournament
    {
        protected List<Participant> UpperBracketParticipants;
        protected List<Participant> RoundBracket= new List<Participant>();
        protected int GameIndex;
        protected Participant LeftParticipant;
        protected Participant RightParticipant;        

        public SingleEliminationTournament(List<string> participants)
        {
            UpperBracketParticipants = new List<Participant>();
            Random random = new Random();

            while (participants.Count > 0)
            {
                int index = random.Next(participants.Count);
                var newParticipant = new Participant(participants[index]);
                UpperBracketParticipants.Add(newParticipant);
                participants.RemoveAt(index);
            }

            BinarySaver.SaveSingleToBinnary(this);
        }

        public void GetPlayingParticipants(out Participant leftParticipant, out Participant rightParticipant)
        {
            if (GameIndex >= RoundBracket.Count/2)
                OrganizeRound(ref UpperBracketParticipants);

            LeftParticipant = leftParticipant =RoundBracket[GameIndex * 2];
            RightParticipant = rightParticipant = RoundBracket[GameIndex * 2 + 1];
        }

        public void PlayGame(Participant winner)
        {
            UpperBracketParticipants[GameIndex].SetName(winner.Name);
            GameIndex++;
            BinarySaver.SaveSingleToBinnary(this);
        }


        protected void OrganizeRound(ref List<Participant> bracket)
        {
            RoundBracket = new List<Participant>(bracket);
            bracket = new List<Participant>();

            for (int i = 0; i < RoundBracket.Count / 2; i++)
            {
                bracket.Add(new Participant("",RoundBracket[i * 2], RoundBracket[i * 2 + 1]));
                RoundBracket[i * 2].SetWinner(bracket[i]);
                RoundBracket[i * 2 + 1].SetWinner(bracket[i]);
            }

            if (RoundBracket.Count%2==1)
                bracket.Add(RoundBracket[RoundBracket.Count-1]);

            GameIndex = 0;
        }

        public virtual bool EndOfTheGame()
        {
            if (UpperBracketParticipants.Count < 2)
                return true;
            return false;
        }

        public List<Participant> GetBracket()
        {
            return new List<Participant>(UpperBracketParticipants);
        }

    }
}
