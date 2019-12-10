using Microsoft.AspNetCore.Mvc;

namespace Bivrost.Web.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FallbackController : ControllerBase
  {
     [HttpGet]
    public IActionResult Test()
    {
      return Content("Passed");
    }
  }
}
