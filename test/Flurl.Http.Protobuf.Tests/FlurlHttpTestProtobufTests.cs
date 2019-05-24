using Flurl.Http.Protobuf.Tests.Models;
using Flurl.Http.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf.Tests
{
    public class FlurlHttpTestProtobufTests
    {

        [Test]
        public async Task RespondWithProtobuf_GetObject()
        {
            HttpTest test = new HttpTest();

            ComplexObject objectToSerialize = new ComplexObject
            {
                Id = 1,
                Name = "Test",
                Address = new List<Address>
                {
                    new Address
                    {
                        Line1 = "Address line 1",
                        Line2 = "Address line 2"
                    }
                }
            };

            test.RespondWithProtobuf(objectToSerialize);

            var response = await FlurlRequest().PostProtobufAsync(new ComplexObject()).ReceiveProtobuf<ComplexObject>();

            Assert.AreEqual(objectToSerialize.Id, response.Id);
            Assert.AreEqual(objectToSerialize.Name, response.Name);
            Assert.AreEqual(objectToSerialize.Address.Count, response.Address.Count);
            Assert.AreEqual(objectToSerialize.Address[0].Line1, response.Address[0].Line1);
            Assert.AreEqual(objectToSerialize.Address[0].Line2, response.Address[0].Line2);

        }


        private IFlurlRequest FlurlRequest()
        {
            return $"http://FakeTest".AllowAnyHttpStatus();
        }
    }
}
