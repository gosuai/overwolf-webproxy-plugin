using System;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.Utilities;
using Swan;

namespace ai.gosu
{
    public class WebProxy
    {
        private WebServer server;
        public void Start(string url, string origins, string headers, string methods)
        {
            if (this.server != null)
            {
                throw new Exception("Webserver is already started");
            }

            // Our web server is disposable.
            this.server = CreateWebServer(url, origins, headers, methods);
            this.server.RunAsync();
        }

        public void Stop()
        {
            if (this.server == null)
            {
                throw new Exception("Webserver is not started");
            }
            this.server.Dispose();
            this.server = null;
        }

        public event Action<object, object> OnRequest;

        // Create and configure our web server.
        private WebServer CreateWebServer(string url, string origins, string headers, string methods)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO)
            )
            .WithCors(origins, headers, methods)
            .OnAny(async ctx =>
            {
                var req = await ctx.GetRequestBodyAsStringAsync();
                OnRequest?.Invoke(req, ctx.Request.Headers.ToStringDictionary().ToJson());
                await ctx.SendDataAsync("OK");
            });

            return server;
        }
    }
}
