using System;
using System.Collections.Generic;

namespace Tournament
{
    public class SingleEliminationTournament
    {
        protected List<Participant> UpperBracketParticipants;
        protected readonly Func<string, string> InputWinner;

        public SingleEliminationTournament(List<string> participants, Func<string,string> inputWinner)
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

            InputWinner = inputWinner;
            BinarySaver.SaveListToBinnary("upperBracket", UpperBracketParticipants);
            BinarySaver.SaveListToBinnary("lowerBracket", new List<Participant>());
        }

        public SingleEliminationTournament(Func<string, string> inputWinner)
        {
            UpperBracketParticipants = BinarySaver.LoadListFromBinnary<Participant>("upperBracket");
            InputWinner = inputWinner;
        }

        public void PlayRound()
        {
            UpperBracketParticipants = BinarySaver.LoadListFromBinnary<Participant>("upperBracket");

            Round round = new Round(UpperBracketParticipants);
            round.PlayUpperBracket(InputWinner);
            UpperBracketParticipants = round.UpperBracketParticipants;

            BinarySaver.SaveListToBinnary("upperBracket",UpperBracketParticipants);
            BinarySaver.SaveListToBinnary("lowerBracket", new List<Participant>());
        }

        public virtual bool EndOfTheGame()
        {
            if (UpperBracketParticipants.Count == 1)
                return true;
            return false;
        }

        public List<Participant> GetBracket()
        {
            return new List<Participant>(UpperBracketParticipants);
        }

    }
}
