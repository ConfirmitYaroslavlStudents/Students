using System;
using System.Collections.Generic;

namespace Championship
{
    [Serializable]
    public abstract class Tournament
    {
        protected int IndexOfRound;
        protected int IndexOfMatch;

        public abstract void CollectResults(int[] resultMatch);
        public abstract List<Round>[] GetTournamentToPrint();
        public abstract Meeting GetNextMeeting();

        protected virtual void PromotionWinnerToNextStage(Meeting meeting, string player)
        {
            if (meeting.NextStage == null)
            {
                return;
            }

            if (meeting.NextStage.FirstPlayer == null)
            {
                meeting.NextStage.FirstPlayer = player;
            }
            else
            {
                meeting.NextStage.SecondPlayer = player;
            }
        }
        protected virtual List<Round> CloneTournament(List<Round> rounds)
        {
            List<Round> newCloneTournament = new List<Round>();

            for (var i = 0; i < rounds.Count; i++)
            {
                newCloneTournament.Add(new Round());
                newCloneTournament[i].Stage = rounds[i].Stage;

                foreach (var meeting in rounds[i].Meetings)
                {
                    newCloneTournament[i].Meetings.Add(meeting.CloneMeeting());
                }
            }

            return newCloneTournament;
        }

    }

    public class FileManagerDecorator:Tournament
    {
        private Tournament _tournament;
        private IFileManager fileManager;

        public FileManagerDecorator(Tournament tournament, IFileManager manager)
        {
            _tournament = tournament;
            fileManager = manager;
        }

        public override void CollectResults(int[] resultMatch)
        {
            _tournament.CollectResults(resultMatch);
            fileManager.WriteToFile(_tournament);
        }

        public override List<Round>[] GetTournamentToPrint()
        {
            return _tournament.GetTournamentToPrint();
        }

        public override Meeting GetNextMeeting()
        {
            return _tournament.GetNextMeeting();
        }
    } 
}
