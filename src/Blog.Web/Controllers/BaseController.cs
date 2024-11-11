using Blog.Data.Contexts;
using Blog.Data.Entities;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
	public abstract class BaseController : Controller
	{
		protected string UserLoggedId { get { return User.FindFirstValue(ClaimTypes.NameIdentifier); } }
		protected bool UserIsAdm { get { return User.IsInRole(BlogConstants.ADMINROLE); } }

        protected bool ValidEditPermission(Post post)
		{
			var userId = UserLoggedId;
			if (post.Author.UserId != userId)
				return User.IsInRole(BlogConstants.ADMINROLE);
			return true;
		}

		public IActionResult Errors(int code, string message, string title = "")
		{
			var model = new ErrorViewModel { Message = message, Title = title, ErrorCode = code };	
			return View("Error", model);
		}
	}
}
