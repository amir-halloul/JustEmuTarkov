using JustEmuTarkov.Models;


namespace JustEmuTarkov.HttpServerComponents
{
    public interface IHttpRequestHandler
    {
        void Handle(RequestData data);
        string GetPath();
    } 
}
