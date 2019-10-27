using System.Net;
using Ionic.Zlib;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.Models;
using JustEmuTarkov.Utility;

namespace JustEmuTarkov.HttpServerComponents.HttpRequestHandlers
{
    public class LocaleRequestHandler : IHttpRequestHandler
    {
        private const string Path = "/client/menu/locale/en";
        public void Handle(RequestData data)
        {
            HttpListenerResponse serverResponse = data.Context.Response;
            Log.Info($"Locale requested from client at {data.Context.Request.RemoteEndPoint}");
            serverResponse.StatusCode = (int)HttpStatusCode.OK;
            serverResponse.ContentType = "text/plain";
            serverResponse.AddHeader("Content-Encoding", "deflate");
            if (data.Client != null)
                serverResponse.SetCookie(new Cookie("PHPSESSID", data.Client.UniqueSession)); ;
            byte[] messageBytes = ZlibStream.CompressString(Serializer.Read("./Templates/Localization/en/Menu.json"));
            serverResponse.OutputStream.Write(messageBytes, 0, messageBytes.Length);
            serverResponse.Close();
        }

        public string GetPath() => Path;
    }
}
