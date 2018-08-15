using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tournament
{
    internal static class BinarySaver
    {
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
