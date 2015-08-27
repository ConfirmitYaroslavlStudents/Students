using System;
using System.IO;

using Mp3TagLib.Plan;
using System.Runtime.Serialization.Formatters.Binary;

namespace mp3tager
{
    class PlanProvider : IPlanProvider
    {
        public SyncPlan Load(string path)
        {
           // path = @"C:\Users\Alexandr\Desktop\TEST\save";
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                
                BinaryFormatter formatter = new BinaryFormatter();
                return (SyncPlan)formatter.Deserialize(fs);
            }
        }

        public void Save(SyncPlan plan, string path)
        {
            //path = @"C:\Users\Alexandr\Desktop\TEST\save";
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, plan);
            }
        }
    }
}
