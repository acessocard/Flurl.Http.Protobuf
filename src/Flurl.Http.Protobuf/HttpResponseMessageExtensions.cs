using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf
{
    public static class HttpResponseMessageExtensions
    {
        private static HttpCall GetHttpCall(HttpRequestMessage request)
        {
            if (request?.Properties != null && request.Properties.TryGetValue("FlurlHttpCall", out var obj) && obj is HttpCall call)
            {
                return call;
            }
            return null;
        }
        private static string GetMediaType(HttpRequestMessage request)
        {
            if (request.Headers.Accept.Any())
            {
                // return media type of first accepted media type containing "xml", else of first accepted media type
                var acceptHeader = request.Headers.Accept.First(x => x.MediaType.IndexOf("protobuf", StringComparison.OrdinalIgnoreCase) >= 0)
                 ?? request.Headers.Accept.First();

                return acceptHeader.MediaType;
            }

            // no accepted media type present, return default
            return "application/x-protobuf";
        }
        private static async Task<T> ReceiveFromProtobufStream<T>(this Task<HttpResponseMessage> response, Func<HttpCall, Stream, T> streamHandler)
        {
            var resp = await ReceiveProtobufResponseMessage(response);
            var call = GetHttpCall(resp.RequestMessage);

            try
            {
                using (var stream = await resp.Content.ReadAsStreamAsync())
                {
                    return streamHandler(call, stream);
                }
            }
            catch (Exception ex)
            {
                var s = await resp.Content.ReadAsStringAsync();
                throw new FlurlHttpException(call, s, ex);
            }
        }
        private static async Task<HttpResponseMessage> ReceiveProtobufResponseMessage(this Task<HttpResponseMessage> responseMessage)
        {
            var response = await responseMessage.ConfigureAwait(false);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(GetMediaType(response.RequestMessage));

            return response;
        }
        public static async Task<T> ReceiveProtobuf<T>(this Task<HttpResponseMessage> response)
            => await ReceiveFromProtobufStream(response, (call, stm) =>
                 call.FlurlRequest.Settings.ProtobufSerializer().Deserialize<T>(stm));
    }
}
