using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms.Shapes;

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
            try
            {
                if (!File.Exists("/Users/lizazalozna/Projects/Lab4/cares.xml"))
                    return new List<T>();

                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                using (FileStream stream = new FileStream("/Users/lizazalozna/Projects/Lab4/cares.xml", FileMode.Open))
                {
                    return (List<T>)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Помилка при завантаженні XML: {ex.Message}");
                return new List<T>();
            }
        }
    }
}