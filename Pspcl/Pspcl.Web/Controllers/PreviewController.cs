using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pspcl.Web.Controllers
{
	[Authorize]
	public class PreviewController : Controller
	{
		public IActionResult Preview()
		{
			return View();
		}
	}
}
