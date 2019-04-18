using Flurl.Http.Configuration;
using Flurl.Http.Protobuf.Tests.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;

namespace Flurl.Http.Protobuf.Tests.Factories
{
    public class SimpleModelHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly SimpleModel _response;
        public SimpleModelHttpClientFactory(SimpleModel response)
        {
            _response = response;
        }
        private HttpClient GetClient()
        {
            var builder = new WebHostBuilder().Configure(app =>
            {
                app.Use(async (context, next) =>
                {
                    context.Request.Headers.ValidateProtobuf();
                    await context.Response.Body.WriteAsync(ProtoSerializer.Serialize(_response));
                });
            });

            var server = new TestServer(builder);
            return server.CreateClient();
        }

        public override HttpClient CreateHttpClient(HttpMessageHandler handler) => GetClient();
    }

    public static class IHeaderDictionaryExtensions
    {
        public static void ValidateProtobuf(this IHeaderDictionary header)
        {
            Assert.AreEqual("application/x-protobuf", header["Accept"].ToString());
            Assert.AreEqual("application/x-protobuf", header["Content-Type"].ToString());
        }
    }
}
