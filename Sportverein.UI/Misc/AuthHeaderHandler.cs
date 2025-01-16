using System;

namespace Sportverein.UI.Misc;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor httpcontext;
    public AuthHeaderHandler(IHttpContextAccessor httpContextAccessor)
    {
        this.httpcontext = httpContextAccessor;
    }


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var context = httpcontext.HttpContext;
        if (context.Request.Cookies.TryGetValue("Authorization", out var cookie))
        {
            if (!string.IsNullOrEmpty(cookie))
            {
                request.Headers.Add("Authorization", $"{cookie}");
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
