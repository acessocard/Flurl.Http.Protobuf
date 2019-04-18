using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf
{
    public static class UrlExtensions
    {
        public static Task<T> GetProtobufAsync<T>(this Url url, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).GetProtobufAsync<T>(cancellationToken, completionOption);
        public static Task<HttpResponseMessage> PostProtobufAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).PostProtobufAsync(data, cancellationToken, completionOption);
        public static Task<HttpResponseMessage> PutProtobufAsync(this Url url, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).PutProtobufAsync(data, cancellationToken, completionOption);
        public static Task<HttpResponseMessage> SendProtobufAsync(this Url url, HttpMethod method, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead) =>
            new FlurlRequest(url).SendProtobufAsync(method, data, cancellationToken, completionOption);
    }
}
