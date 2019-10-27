using System.IO;
using Newtonsoft.Json;

namespace JustEmuTarkov.Utility
{
    public static class Serializer
    {
        public static string Serialize<T>(T obj) => JsonConvert.SerializeObject(obj);
        public static T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);
        public static string Read(string filepath)
        {
            using (StreamReader reader = new StreamReader(filepath))
                return reader.ReadToEnd();
        }
        public static void Write(string filepath, string text)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
                writer.Write(text);
        }
    }
}
