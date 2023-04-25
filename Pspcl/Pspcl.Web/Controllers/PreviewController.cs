using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
	public class PreviewController : Controller
	{
		public IActionResult Preview()
		{
			return View();
		}
	}
}
