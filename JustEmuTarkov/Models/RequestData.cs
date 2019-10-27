using System.Net;

namespace JustEmuTarkov.Models
{
    public class RequestData
    {
        public HttpListenerContext Context { get; set; }
        public EftClient Client { get; set; }
        public string Body { get; set; }
    }
}
