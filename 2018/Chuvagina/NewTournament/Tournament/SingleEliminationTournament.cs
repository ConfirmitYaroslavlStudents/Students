using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tournament
{
    public class SingleEliminationTournament
    {
        protected List<Participant> UpperBracketParticipants;

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

            SaveListToBinnary("upperBracket", UpperBracketParticipants);
            SaveListToBinnary("lowerBracket", new List<Participant>());
        }

        public SingleEliminationTournament()
        {
            UpperBracketParticipants = LoadListFromBinnary<Participant>("upperBracket");
        }

        public virtual void PlayRound()
        {
            UpperBracketParticipants = LoadListFromBinnary<Participant>("upperBracket");

            Round round = new Round(UpperBracketParticipants);
            round.PlayUpperBracket();
            UpperBracketParticipants = round.UpperBracketParticipants;

            SaveListToBinnary("upperBracket",UpperBracketParticipants);
            SaveListToBinnary("lowerBracket", new List<Participant>());
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

        public static void SaveListToBinnary<T>(String fileName, List<T> serializableObjects)
        {
            using (FileStream fs = File.Create(fileName))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, serializableObjects);
            }
        }

        public static List<T> LoadListFromBinnary<T>(String fileName)
        {
            using (FileStream fs = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<T>)formatter.Deserialize(fs);
            }
        }

    }
}
