using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	[Authorize(Roles="ADMINISTRADOR")]
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
