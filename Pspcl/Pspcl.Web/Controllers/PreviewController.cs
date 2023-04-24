using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
	public class PreviewController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
