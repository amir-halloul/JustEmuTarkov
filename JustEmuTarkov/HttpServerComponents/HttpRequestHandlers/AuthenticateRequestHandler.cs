using System.Net;
using Ionic.Zlib;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.Models;
using JustEmuTarkov.Services;


namespace JustEmuTarkov.HttpServerComponents.HttpRequestHandlers
{
    public class AuthenticateRequestHandler : IHttpRequestHandler
    {
        private const string Path = "/client/game/login";
        public void Handle(RequestData data)
        {
            HttpListenerResponse serverResponse = data.Context.Response;
            Log.Info($"Authentication requested from client at {data.Context.Request.RemoteEndPoint}");
            serverResponse.StatusCode = (int) HttpStatusCode.OK;
            serverResponse.ContentType = "text/plain";
            serverResponse.AddHeader("Content-Encoding", "deflate");
            EftClient client = data.Client;
            byte[] messageBytes = ZlibStream.CompressString(AuthenticationService.Authenticate(ref client, data.Body));
            serverResponse.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            serverResponse.SetCookie(new Cookie("PHPSESSID", client.UniqueSession));;
            serverResponse.Close();
        }

        public string GetPath()
        {
            return Path;
        }
    }
}
