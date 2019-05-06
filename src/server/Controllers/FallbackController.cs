using Microsoft.AspNetCore.Mvc;

namespace Bivrost.Web.Controllers
{
  public class FallbackController : Controller
  {
    public IActionResult Index()
    {
      return File("~/index.html", "text/html");
    }

    public IActionResult Test()
    {
      return Content("Passed");
    }
  }
}
