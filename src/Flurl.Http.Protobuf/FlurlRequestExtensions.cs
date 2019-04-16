﻿using Flurl.Http.Content;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flurl.Http.Protobuf
{
    public static class FlurlRequestExtensions
    {
        private static string GetMediaType(this IHttpSettingsContainer request)
        {
            var acceptHeaders = request.Headers
                .Where(x => x.Key == "Accept")
                .ToList();

            if (!acceptHeaders.Any() || acceptHeaders.All(x => x.Value == null))
            {
                return "application/x-protobuf";
            }

            var mediaTypes = acceptHeaders
                .Where(x => x.Value != null)
                .SelectMany(x => x.Value.ToString().Split(','))
                .Select(x => x.Trim())
                .ToList();

            return mediaTypes.First(x => x.IndexOf("protobuf", StringComparison.OrdinalIgnoreCase) >= 0)
                ?? mediaTypes.First();
        }
        public static async Task<HttpResponseMessage> SendProtobufAsync(this IFlurlRequest request, HttpMethod httpMethod, object data, CancellationToken cancellationToken = default(CancellationToken), HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
            var content = new CapturedStringContent(request.Settings.ProtobufSerializer().Serialize(data), Encoding.UTF8, request.GetMediaType());
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