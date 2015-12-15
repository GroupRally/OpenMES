using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MES.Utility
{
    public class JsonUtility
    {
        public static byte[] JsonSerialize(object objectToSerialize, Type[] extraTypes, string rootName)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();

            settings.KnownTypes = extraTypes;
            settings.RootName = rootName;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType(), settings);

            byte[] buffer;

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, objectToSerialize);
                //stream.Flush();
                buffer = stream.GetBuffer();
            }

            return buffer;
        }

        public static object JsonDeserialize(byte[] jsonBytes, Type objectType, Type[] extraTypes, string rootName)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();

            settings.KnownTypes = extraTypes;
            settings.RootName = rootName;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectType, settings);

            object returnObject;

            using (MemoryStream stream = new MemoryStream(jsonBytes))
            {
                returnObject = serializer.ReadObject(stream);
            }

            return returnObject;
        }
    }
}
