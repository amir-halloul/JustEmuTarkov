using System.Net;
using Ionic.Zlib;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.Models;
using JustEmuTarkov.Utility;

namespace JustEmuTarkov.HttpServerComponents.HttpRequestHandlers.Version
{
    class ValidationRequestHandler : IHttpRequestHandler
    {
        private const string Path = "/client/game/version/validate";

        public void Handle(RequestData data)
        {
            HttpListenerResponse serverResponse = data.Context.Response;
            Log.Info($"Authentication requested from client at {data.Context.Request.RemoteEndPoint}");
            serverResponse.StatusCode = (int)HttpStatusCode.OK;
            serverResponse.ContentType = "text/plain";
            serverResponse.AddHeader("Content-Encoding", "deflate");
            if(data.Client != null)
                serverResponse.SetCookie(new Cookie("PHPSESSID", data.Client.UniqueSession)); ;
            byte[] messageBytes = ZlibStream.CompressString(Serializer.Read("./Templates/NullClass.json"));
            serverResponse.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            serverResponse.Close();
        }

        public string GetPath()
        {
            return Path;
        }
    }
}
