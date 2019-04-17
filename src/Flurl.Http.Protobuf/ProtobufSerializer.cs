using System.IO;

namespace Flurl.Http.Protobuf
{
    public class ProtobufSerializer
    {
        public T Deserialize<T>(Stream stream)
        {
            return ProtoSerializer.Deserialize<T>(stream);
        }
        public byte[] SerializeByte(object obj)
        {
            return ProtoSerializer.Serialize(obj);
        }
    }
}
