using System.IO;
using System.Net;

namespace GccSharp
{
    public static class WebRequestExtensions
    {
        public static WebResponse GetResponse(this WebRequest request)
        {
            var asyncResult = request.BeginGetResponse(a => { }, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            var response = request.EndGetResponse(asyncResult);
            return response;
        }

        public static Stream GetRequestStream(this WebRequest request)
        {
            var asyncResult = request.BeginGetRequestStream(a => { }, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            var stream = request.EndGetRequestStream(asyncResult);
            return stream;
        }
    }
}
