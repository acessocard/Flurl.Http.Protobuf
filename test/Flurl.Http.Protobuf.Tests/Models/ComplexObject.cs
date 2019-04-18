using ProtoBuf;
using System.Collections.Generic;

namespace Flurl.Http.Protobuf.Tests.Models
{
    [ProtoContract]
    public class ComplexObject
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public List<Address> Address { get; set; }
    }
    [ProtoContract]
    public class Address
    {
        [ProtoMember(1)]
        public string Line1 { get; set; }
        [ProtoMember(2)]
        public string Line2 { get; set; }
    }
}
