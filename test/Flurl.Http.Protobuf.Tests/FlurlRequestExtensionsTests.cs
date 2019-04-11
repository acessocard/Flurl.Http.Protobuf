using Flurl.Http.Protobuf.Tests.Factories;
using Flurl.Http.Protobuf.Tests.Models;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace Flurl.Http.Protobuf.Tests
{
    public class FlurlRequestExtensionsTests
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
            FlurlHttp.Configure(c => c.HttpClientFactory = new SimpleModelHttpClientFactory(obj));

            var result = await new FlurlRequest("https://some.url").GetProtobufAsync<SimpleModel>();

            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Age, result.Age);
            Assert.AreEqual(obj.BirthDate, result.BirthDate);
            Assert.AreEqual(obj.Name, result.Name);
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

            var result = await new FlurlRequest("https://some.url").PostProtobufAsync(obj);

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

            var result = await new FlurlRequest("https://some.url").PutProtobufAsync(obj);

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

            var result = await new FlurlRequest("https://some.url").SendProtobufAsync(HttpMethod.Post, obj);

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

            var result = await new FlurlRequest("https://some.url").SendProtobufAsync(HttpMethod.Put, obj);

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

            var result = await new FlurlRequest("https://some.url").SendProtobufAsync(HttpMethod.Get, null);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
