using Flurl.Http.Configuration;
using System;

namespace Flurl.Http.Protobuf
{
    public static class FlurlHttpSettingsExtensions
    {
        private static readonly Lazy<ProtobufSerializer> _protoSerializerInstance = new Lazy<ProtobufSerializer>(() => new ProtobufSerializer());

        public static ProtobufSerializer ProtobufSerializer(this FlurlHttpSettings settings)
        {
            return _protoSerializerInstance.Value;
        }
    }
}
