using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shyjus.BrowserDetection;

namespace Middleware.Middle
{
    public class CustomMiddleware
    {
        public readonly RequestDelegate next;

        public CustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IBrowserDetector browserDetector)
        {
            var defaultBrowser = browserDetector.Browser;

            if (defaultBrowser.Name == BrowserNames.InternetExplorer || defaultBrowser.Name == BrowserNames.EdgeChromium || defaultBrowser.Name == BrowserNames.Edge)
            {
                await httpContext.Response.WriteAsync("<html lang='pl'><head><meta charset='utf-8'></head><body><p>Przeglądarka nie jest obsługiwana</p></body></html>");
            }
            else
            {
                await this.next.Invoke(httpContext);
            }
        }
    }
}

