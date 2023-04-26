using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;

namespace Pspcl.Web.Controllers
{
	public class AddStockController : Controller
	{
		private readonly ApplicationDbContext _dbcontext;
		public AddStockController(ApplicationDbContext dbContext)
		{
			_dbcontext = dbContext;
		}

		
		public IActionResult AddStock()
		{
			var materialGroup = _dbcontext.MaterialGroup.ToList();
			var materialType = _dbcontext.MaterialType.ToList();
			ViewBag.materialType = new SelectList(materialType, "Id", "Name");
			ViewBag.rating=new SelectList(materialType,"Id","Rating");
			ViewBag.materialGroup = new SelectList(materialGroup,"Id","Name");
			return View();
		}
	}
}
