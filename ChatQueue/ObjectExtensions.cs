using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatQueue
{
    public static class ObjectExtensions
    {
        public static string GetMessageType(this object obj)
        {
            return obj.GetType().AssemblyQualifiedName;
        }
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static Stream ToJsonStream(this object obj)
        {
            var json = obj.ToJsonString();
            return new MemoryStream(Encoding.Default.GetBytes(json));
        }



        public static string ReadToEnd(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        public static T ReadFromJson<T>(this Stream stream) 
        {
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static object ReadFromJson(this Stream stream,string messageType)
        {
            var type = Type.GetType(messageType);
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject(json, type);
        }
    }
}
