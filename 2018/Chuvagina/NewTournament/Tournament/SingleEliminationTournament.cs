using System;
using System.Collections.Generic;

namespace Tournament
{
    public class SingleEliminationTournament
    {
        protected List<Participant> UpperBracketParticipants;
        protected List<Participant> RoundBracket= new List<Participant>();
        protected int GameIndex;
        private const string _upperFileName = "upperBracket";
        protected const string LowerFileName = "lowerBracket";
        private const string _indexFileName = "gameIndex";
        private const string _roundFileName = "roundBracket";

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

            SaveData();
        }

        public SingleEliminationTournament()
        {
            UpperBracketParticipants = BinarySaver.LoadListFromBinnary<Participant>(_upperFileName);
            RoundBracket= BinarySaver.LoadListFromBinnary<Participant>(_roundFileName);
            GameIndex= BinarySaver.LoadIntFromBinnary(_indexFileName);
        }

        public void PlayGame(Func<string, string> inputWinner)
        {
            if (GameIndex >= RoundBracket.Count/2)
                OrganizeRound(ref UpperBracketParticipants);

            var leftParticipant = RoundBracket[GameIndex * 2];
            var rightParticipant = RoundBracket[GameIndex * 2 + 1];
            var game = new Game(leftParticipant, rightParticipant);
            game.PlayGame(inputWinner, out string winner, out string loser);
            UpperBracketParticipants[GameIndex].SetName(winner);

            GameIndex++;
            SaveData();
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

        public void SaveData()
        {
            BinarySaver.SaveListToBinnary(_upperFileName, UpperBracketParticipants);
            BinarySaver.SaveListToBinnary(_roundFileName, RoundBracket);
            BinarySaver.SaveListToBinnary(LowerFileName, new List<Participant>());
            BinarySaver.SaveIntToBinnary(_indexFileName, GameIndex);
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
