using Flurl.Http.Configuration;
using Flurl.Http.Protobuf.Tests.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;


namespace Flurl.Http.Protobuf.Tests.Factories
{
    public class ComplexObjectHttpClientFactory : DefaultHttpClientFactory
    {
        private readonly ComplexObject _response;
        public ComplexObjectHttpClientFactory(ComplexObject response)
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
}