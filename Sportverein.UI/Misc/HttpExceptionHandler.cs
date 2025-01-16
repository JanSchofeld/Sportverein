using System;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sportverein.UI.Misc;

public class HttpExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //problem.Title = httpContext.Response.ToString();
        //var test = JObject.Parse(httpContext.Response.Body.ToString());
        //var response = test["title"].ToString();
        // await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problem), cancellationToken);

        //string body = httpContext.Response.StatusCode.ToString();




        httpContext.Response.Redirect("/error");
        return true;
    }
}
