using System.Collections.Generic;
using System.Net;
using System.Threading;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.HttpServerComponents.HttpRequestHandlers;
using JustEmuTarkov.Models;

namespace JustEmuTarkov.HttpServerComponents
{
    public class ResourceLocator
    {
        private Dictionary<string, IHttpRequestHandler> _httpRequestHandlers;

        public ResourceLocator()
        {
            this._httpRequestHandlers = new Dictionary<string, IHttpRequestHandler>();
            AddHttpRequestHandler(new InvalidRequestHandler());
        }

        public void AddHttpRequestHandler(IHttpRequestHandler httpRequestHandler)
        {
            if (!_httpRequestHandlers.ContainsKey(httpRequestHandler.GetPath()))
                this._httpRequestHandlers.Add(httpRequestHandler.GetPath(), httpRequestHandler);
            else
                this._httpRequestHandlers[httpRequestHandler.GetPath()] = httpRequestHandler;
        }

        public void HandleContext(RequestData data)
        {
            HttpListenerRequest request = data.Context.Request;
            Log.Debug($"Received request to {request.Url.AbsolutePath}");
            string requestHandlerName = request.Url.AbsolutePath;
            // Find corresponding handler
            IHttpRequestHandler handler = this._httpRequestHandlers.ContainsKey(requestHandlerName) ? this._httpRequestHandlers[requestHandlerName] : _httpRequestHandlers[InvalidRequestHandler.Path];

            // Invoke chosen handler
            this.InvokeHandler(handler, data);
        }

        private void InvokeHandler(IHttpRequestHandler handler, RequestData data)
        {
            Thread handleHttpRequestThread = new Thread(() => handler.Handle(data));
            handleHttpRequestThread.Start();
        }

    }
}
