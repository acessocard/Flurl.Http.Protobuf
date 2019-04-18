using Flurl.Http.Configuration;
using Flurl.Http.Protobuf.Tests.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;

namespace Flurl.Http.Protobuf.Tests.Factories
{
    public class VerifyObjectHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly SimpleModel _request;
        public VerifyObjectHttpClientFactory(SimpleModel request)
        {
            _request = request;
        }
        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    context.Request.Headers.ValidateProtobuf();
                    var body = ProtoBuf.Serializer.Deserialize<SimpleModel>(context.Request.Body);
                    Assert.AreEqual(_request.Id, body.Id);
                    Assert.AreEqual(_request.Age, body.Age);
                    Assert.AreEqual(_request.BirthDate, body.BirthDate);
                    Assert.AreEqual(_request.Name, body.Name);

                    context.Response.StatusCode = 200;
                });
            });

            var server = new TestServer(builder);
            return server.CreateClient();
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }
}
