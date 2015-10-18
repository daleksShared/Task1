using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Task1.Data
{
       public class Serializer
       {
           public T DeserializeFromPath<T>(string filepath)
           {
               T obj = default(T);

               XmlSerializer serializer = new XmlSerializer(typeof(T));

               StreamReader reader = new StreamReader(filepath);
               obj = (T)serializer.Deserialize(reader);
               reader.Close();
               return obj;
           }
        public void SerializeToPath<T>(T obj, string filepath)
        { 
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                ser.Serialize(fs, obj);
                
            }
        }

    }
    
}
