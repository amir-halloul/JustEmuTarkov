using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using JustEmuTarkov.Diagnostics;
using JustEmuTarkov.HttpServerComponents;
using JustEmuTarkov.HttpServerComponents.HttpRequestHandlers;
using JustEmuTarkov.HttpServerComponents.HttpRequestHandlers.Version;
using JustEmuTarkov.Models;

namespace JustEmuTarkov
{
    public class HttpServer
    {
        private readonly HttpListener _httpListener;
        private readonly ResourceLocator _resourceLocator ;
        private Thread _connectionThread;
        private bool _running;
        private bool _disposed;

        public HttpServer(string prefix)
        {
            this._httpListener = new HttpListener();
            this._resourceLocator = new ResourceLocator();
            this._resourceLocator.AddHttpRequestHandler(new AuthenticateRequestHandler());
            this._resourceLocator.AddHttpRequestHandler(new ValidationRequestHandler());
            this._resourceLocator.AddHttpRequestHandler(new LocaleRequestHandler());
            this._resourceLocator.AddHttpRequestHandler(new LanguagesRequestHandler());
            this._httpListener.Prefixes.Add(prefix);
        }

        public void AddHttpRequestHandler(IHttpRequestHandler requestHandler)
        {
            this._resourceLocator.AddHttpRequestHandler(requestHandler);
        }

        public void Start()
        {
            if (!this._httpListener.IsListening)
            {
                this._httpListener.Start();
                this._running = true;
                this._connectionThread = new Thread(this.ConnectionThreadStart);
                this._connectionThread.Start();
                Log.Info($"Server started listening on address {this._httpListener.Prefixes.First()}");
            }
        }

        public void Stop()
        {
            if (this._httpListener.IsListening)
            {
                this._running = false;
                this._httpListener.Stop();
            } 
        }

        private void ConnectionThreadStart()
        {
            try
            {
                while (this._running)
                {
                    HttpListenerContext context = _httpListener.GetContext();

                    string body = string.Empty;
                    if (context.Request.HasEntityBody)
                    {
                        MemoryStream ms = new MemoryStream();
                        context.Request.InputStream.CopyTo(ms);
                        byte[] data = ms.ToArray();

                        body = Ionic.Zlib.ZlibStream.UncompressString(data);
                    }

                    RequestData requestData = new RequestData()
                    {
                        Context = context,
                        Client = Server.GetClient(context.Request.Cookies["PHPSESSID"]?.Value.ToLower()),
                        Body = body
                    };
                    _resourceLocator.HandleContext(requestData);
                }
            }
            catch (HttpListenerException)
            {
                Console.WriteLine("HTTP server was shut down.");
            }
        }

        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (this._disposed)
                return;

            if (disposing)
            {
                if (this._running)
                    this.Stop();
                if (this._connectionThread != null)
                {
                    this._connectionThread.Abort();
                    this._connectionThread = null;
                }
            }
            this._disposed = true;
        }
    }
}
