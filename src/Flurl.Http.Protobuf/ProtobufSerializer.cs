using Flurl.Http.Configuration;
using System;
using System.IO;

namespace Flurl.Http.Protobuf
{
    public class ProtobufSerializer : ISerializer
    {
        public T Deserialize<T>(string s)
        {
            var bytes = Convert.FromBase64String(s);
            return ProtoSerializer.Deserialize<T>(bytes);
        }
        public T Deserialize<T>(Stream stream)
        {
            return ProtoSerializer.Deserialize<T>(stream);
        }
        public string Serialize(object obj)
        {
            return Convert.ToBase64String(ProtoSerializer.Serialize(obj));
        }
    }
}
