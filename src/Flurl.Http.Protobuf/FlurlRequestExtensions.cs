using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf
{
    public static class FlurlRequestExtensions
    {
        public static async Task<HttpResponseMessage> SendProtobufAsync(this IFlurlRequest request, HttpMethod httpMethod, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            var content = new ByteArrayContent(request.Settings.ProtobufSerializer().SerializeByte(data));
            return await request.SetHeaders().SendAsync(httpMethod, content, cancellationToken, completionOption);
        }
        public static Task<T> GetProtobufAsync<T>(this IFlurlRequest request, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            request.SendProtobufAsync(HttpMethod.Get, null, cancellationToken, completionOption).ReceiveProtobuf<T>();
        public static Task<HttpResponseMessage> PostProtobufAsync(this IFlurlRequest request, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            SendProtobufAsync(request, HttpMethod.Post, data, cancellationToken, completionOption);
        public static Task<HttpResponseMessage> PutProtobufAsync(this IFlurlRequest request, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            SendProtobufAsync(request, HttpMethod.Put, data, cancellationToken, completionOption);
        private static IFlurlRequest SetHeaders(this IFlurlRequest request) =>
            request.WithHeader("Accept", "application/x-protobuf").WithHeader("Content-Type", "application/x-protobuf");
    }
}
