using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lab4
{
    public static class Serializer
    {
        public static void SaveToXml<T>(List<T> list)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream("/Users/lizazalozna/Projects/Lab4/cares.xml", FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }

        public static List<T> LoadFromXml<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            using (FileStream stream = new FileStream("/Users/lizazalozna/Projects/Lab4/cares.xml", FileMode.Open))
            {
                return (List<T>)serializer.Deserialize(stream);
            }
        }

    }
}