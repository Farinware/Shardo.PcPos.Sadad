using System.Net;
namespace Shardo.PcPos.Sadad.Exceptions
{
    public sealed class SadadPcPosException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? ResponseBody { get; }
        public SadadPcPosException(string message, HttpStatusCode statusCode, string? responseBody = null, Exception? inner = null) : base(message, inner)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }
}
