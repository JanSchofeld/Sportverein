using System;
using Microsoft.AspNetCore.Mvc;

namespace Sportverein.UI.Controllers;

public class ErrorController : Controller
{

    [HttpGet("/error")]
    public IActionResult Error()
    {
        return View();
    }
}
