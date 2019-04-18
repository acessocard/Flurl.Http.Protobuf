using ProtoBuf;
using System;

namespace Flurl.Http.Protobuf.Tests.Models
{
    [ProtoContract]
    public class SimpleModel
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }
        [ProtoMember(2)]
        public int Age { get; set; }
        [ProtoMember(3)]
        public DateTime BirthDate { get; set; }
        [ProtoMember(4)]
        public string Name { get; set; }
    }
}
