using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace HospitalLib.Utils
{
    public class JsonFormatter
    {
        public string ToJson<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(typeof (T));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, obj);

            stream.Position = 0;
            string jsonResult = new StreamReader(stream).ReadToEnd();

            return jsonResult;
        }

        public T FromJson<T>(string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            var serializer = new DataContractJsonSerializer(typeof (T));
            var stream = new MemoryStream(byteArray);
            var result = (T) serializer.ReadObject(stream);

            return result;
        }
    }
}