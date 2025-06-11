using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab4
{
    public static class Serializer
    {
        public static void SaveToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using FileStream stream = new FileStream("/Users/lizazalozna/Projects/Lab4/care.xml", FileMode.Create);
            serializer.Serialize(stream, obj);
        }

        public static T LoadFromXml<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using FileStream stream = new FileStream("/Users/lizazalozna/Projects/CourseWork/care.xml", FileMode.Open);
            return (T)serializer.Deserialize(stream);
        }
    }
}

