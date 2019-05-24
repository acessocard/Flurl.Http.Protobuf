using Flurl.Http.Testing;
using System.Net.Http;

namespace Flurl.Http.Protobuf
{

    /// <summary>
    /// A Extension for Flurl.Http.Testing.HttpTest to allows the protobuf fake test response.
    /// </summary>
    public static partial class FlurlHttpTestProtobufExtensions
    {

        /// <summary>
        /// Summary:
        ///     Adds an HttpResponseMessage to the response queue with the given data serialized
        ///     to Profobuf as the content body.
        ///     The Object body need to have a protobuf-net data notation do serialize the object.
        ///
        /// Parameters:
        ///   body:
        ///     The object to be Protobuf-serialized and used as the simulated response body.
        ///
        ///   status:
        ///     The simulated HTTP status. Default is 200.
        ///
        ///   headers:
        ///     The simulated response headers (optional).
        ///
        ///   cookies:
        ///     The simulated response cookies (optional).
        ///
        ///   replaceUnderscoreWithHyphen:
        ///     If true, underscores in property names of headers will be replaced by hyphens.
        ///     Default is true.
        ///
        /// Returns:
        ///     The current HttpTest object (so more responses can be chained).
        /// </summary>
        public static HttpTest RespondWithProtobuf<T>(this HttpTest test, T body, int status = 200, object headers = null, object cookies = null, bool replaceUnderscoreWithHyphen = true)
        {
            var protoBody = ProtoSerializer.Serialize(body);
            var content = new ByteArrayContent(protoBody);
            return test.RespondWith(content, status, headers, cookies, replaceUnderscoreWithHyphen);
        }
    }
}
