using System.IO;
using System.Xml.Serialization;

namespace OrderCakes
{
    public static class FileHelper
    {
        private static readonly XmlSerializer Xs = new XmlSerializer(typeof(CakeRequest));
        public static void WriteToFile(string fileName, CakeRequest data)
        {
            using (var fileStream = File.Create(fileName))
            {
                Xs.Serialize(fileStream, data);
            }
        }

        public static CakeRequest LoadFromFile(string fileName)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                return (CakeRequest)Xs.Deserialize(fileStream);
            }
        }

        public static CakeRequest LoadFromStream(Stream file)
        {
            return (CakeRequest)Xs.Deserialize(file);
        }
    }
}