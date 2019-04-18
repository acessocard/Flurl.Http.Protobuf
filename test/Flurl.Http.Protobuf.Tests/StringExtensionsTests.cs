using Flurl.Http.Protobuf.Tests.Factories;
using Flurl.Http.Protobuf.Tests.Models;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public async Task GetProtobufAsync_SimpleObject()
        {
            var obj = new SimpleModel
            {
                Age = 26,
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Foo da Silva"
            };

            var factory = new SimpleModelHttpClientFactory(obj);
            using (var client = new FlurlClient()
            {
                Settings = new Configuration.ClientFlurlHttpSettings()
                {
                    HttpClientFactory = factory
                }
            })
            {
                var result = await "https://some.url".WithClient(client).GetProtobufAsync<SimpleModel>();

                Assert.AreEqual(obj.Id, result.Id);
                Assert.AreEqual(obj.Age, result.Age);
                Assert.AreEqual(obj.BirthDate, result.BirthDate);
                Assert.AreEqual(obj.Name, result.Name);
            }
        }

        [Test]
        public async Task PostProtobufAsync_SimpleObject()
        {
            var obj = new SimpleModel
            {
                Age = 26,
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Foo da Silva"
            };
            FlurlHttp.Configure(c => c.HttpClientFactory = new VerifyObjectHttpClientFactory(obj));

            var result = await "https://some.url".PostProtobufAsync(obj);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task PutProtobufAsync_SimpleObject()
        {
            var obj = new SimpleModel
            {
                Age = 26,
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Foo da Silva"
            };
            FlurlHttp.Configure(c => c.HttpClientFactory = new VerifyObjectHttpClientFactory(obj));

            var result = await "https://some.url".PutProtobufAsync(obj);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task SendProtobufAsync_Post_SimpleObject()
        {
            var obj = new SimpleModel
            {
                Age = 26,
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Foo da Silva"
            };
            FlurlHttp.Configure(c => c.HttpClientFactory = new VerifyObjectHttpClientFactory(obj));

            var result = await "https://some.url".SendProtobufAsync(HttpMethod.Post, obj);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task SendProtobufAsync_Put_SimpleObject()
        {
            var obj = new SimpleModel
            {
                Age = 26,
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Foo da Silva"
            };
            FlurlHttp.Configure(c => c.HttpClientFactory = new VerifyObjectHttpClientFactory(obj));

            var result = await "https://some.url".SendProtobufAsync(HttpMethod.Put, obj);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task SendProtobufAsync_Get_SimpleObject()
        {
            var obj = new SimpleModel
            {
                Age = 26,
                Id = Guid.NewGuid(),
                BirthDate = DateTime.Now,
                Name = "Foo da Silva"
            };
            FlurlHttp.Configure(c => c.HttpClientFactory = new SimpleModelHttpClientFactory(obj));

            var result = await "https://some.url".SendProtobufAsync(HttpMethod.Get, null);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void PostProtobufAsync_WithoutProtocontract()
        {
            var obj = new WithouProtoContract
            {
                Age = 26,
                Name = "Foo da Silva"
            };
            FlurlHttp.Configure(c => c.HttpClientFactory = new VerifyObjectHttpClientFactory(new SimpleModel()));

            Assert.ThrowsAsync<InvalidOperationException>(() => "https://some.url".PostProtobufAsync(obj));

        }

        [Test]
        public async Task GetProtobufAsync_ComplexObject()
        {
            var obj = new ComplexObject
            {
                Id = 12345,
                Name = "Fred",
                Address = new System.Collections.Generic.List<Address>(){ new Address
                {
                    Line1 = "Flat 1",
                    Line2 = "The Meadows"
                } }
            };

            FlurlHttp.Configure(c => c.HttpClientFactory = new ComplexObjectHttpClientFactory(obj));

            var result = await "https://some.url".GetProtobufAsync<ComplexObject>();

            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
            Assert.AreEqual(obj.Address.FirstOrDefault().Line1, result.Address.FirstOrDefault().Line1);
            Assert.AreEqual(obj.Address.FirstOrDefault().Line2, result.Address.FirstOrDefault().Line2);

        }
    }
}
