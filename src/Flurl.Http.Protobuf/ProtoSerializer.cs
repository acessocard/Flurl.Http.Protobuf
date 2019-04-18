using System.IO;

namespace Flurl.Http.Protobuf
{
    public class ProtoSerializer
    {
        public static byte[] Serialize<T>(T obj)
        {
            if (obj == null)
                return new byte[] { };
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, obj);
                return stream.ToArray();
            }
        }
        public static T Deserialize<T>(Stream data)
        {
            if (data == null)
                return default(T);

            return ProtoBuf.Serializer.Deserialize<T>(data);
        }
    }
}
