using System;
using Microsoft.AspNetCore.Mvc;

namespace Sportverein.UI.Controllers;

public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index()
    {
        return Redirect("/courses");
    }
}
