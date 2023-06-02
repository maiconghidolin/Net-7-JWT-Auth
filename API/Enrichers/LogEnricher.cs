using Microsoft.AspNetCore.Http.Features;
using Serilog;

namespace Enrichers;

public static class LogEnricher
{
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var request = httpContext.Request;

        diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].FirstOrDefault());
        diagnosticContext.Set("RequestHost", request.Host);
        diagnosticContext.Set("RequestProtocol", request.Protocol);
        diagnosticContext.Set("RequestPath", GetPath(httpContext));
    }

    static string GetPath(HttpContext httpContext)
    {
        return httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget ?? httpContext.Request.Path.ToString();
    }

}
