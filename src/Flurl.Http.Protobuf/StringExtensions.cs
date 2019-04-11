using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf
{
    public static class StringExtensions
    {
        public static Task<T> GetProtobufAsync<T>(this string url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).GetProtobufAsync<T>(cancellationToken, completionOption);
        public static Task<HttpResponseMessage> PostProtobufAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).PostProtobufAsync(data, cancellationToken, completionOption);
        public static Task<HttpResponseMessage> PutProtobufAsync(this string url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).PutProtobufAsync(data, cancellationToken, completionOption);
        public static Task<HttpResponseMessage> SendProtobufAsync(this string url, HttpMethod method, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).SendProtobufAsync(method, data, cancellationToken, completionOption);
    }
}
