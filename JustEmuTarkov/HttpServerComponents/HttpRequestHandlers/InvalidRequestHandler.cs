using System.Net;
using System.Text;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.Models;

namespace JustEmuTarkov.HttpServerComponents.HttpRequestHandlers
{
    public class InvalidRequestHandler : IHttpRequestHandler
    {
        public const string Path = "/invalid";

        public void Handle(RequestData data)
        {
            HttpListenerResponse serverResponse = data.Context.Response;

            serverResponse.StatusCode = (int)HttpStatusCode.NotFound;
            serverResponse.ContentType = "text/html";
            serverResponse.SetCookie(new Cookie("PHPSESSID", "JustEmuTarkovNXT"));

            string message = "Could not find resource.";
            byte[] messageBytes = Encoding.Default.GetBytes(message);
            serverResponse.OutputStream.Write(messageBytes, 0, messageBytes.Length);

            serverResponse.Close();

            Log.Error($"Invalid request to {data.Context.Request.RawUrl}. Body: {data.Body}");
        }

        public string GetPath()
        {
            return Path;
        }
    }
}
