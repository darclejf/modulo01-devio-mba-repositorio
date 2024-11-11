using AutoMapper;
using Blog.Application.Exceptions;
using Blog.Application.Interfaces;
using Blog.Application.Models;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR,AUTOR")]
	public class PostsController : BaseController
	{
        private readonly IPostApplicationServices _postApplicationServices;
		private readonly IMapper _mapper; //TODO o automapper é ou pode ser lento como vi em algumas matérias na net?

		public PostsController(IPostApplicationServices postApplicationServices, IMapper mapper)
        {
            _postApplicationServices = postApplicationServices;
			_mapper = mapper;
		}


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var posts = await _postApplicationServices.GetPostsAsync(1);
            var model = new ListPostViewModel
            {
                Page = 1,
                Posts = posts.Select(x => _mapper.Map<ListItemPostViewModel>(x))
            };
            return View(model);
        }

        [AllowAnonymous]
        [Route("Posts/List/{page:int}")]
        public async Task<IActionResult> List(int page)
        {
            var posts = await _postApplicationServices.GetPostsAsync(page);
            var model = new ListPostViewModel
            {
                Page = page,
                Posts = posts.Select(x => _mapper.Map<ListItemPostViewModel>(x))
            };
            return PartialView("_ListPosts", model);
        }

        [AllowAnonymous]
		public async Task<IActionResult> View(long id)
        {
            var post = await _postApplicationServices.GetPostAsync(id);
            ViewBag.Edit = ValidEditPermission(post);
            ViewBag.UserId = UserLoggedId;
            ViewBag.IsAdm = UserIsAdm;
            return View(_mapper.Map<PostViewModel>(post));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,SubTitle,Description,Active")] CreatePostModel model)
        {
            if (ModelState.IsValid)
            {
                var post = await _postApplicationServices.AddAsync(model, UserLoggedId);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

		public async Task<IActionResult> Edit(long id)
        {
            var post = await _postApplicationServices.GetPostAsync(id);
            return View(_mapper.Map<EditPostModel>(post));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,SubTitle,Description,Active")] EditPostModel model)
        {
            try
            {
				if (ModelState.IsValid)
				{
                    if (id != model.Id)
						return NotFound();

					var post = await _postApplicationServices.UpdateAsync(model, UserLoggedId);
					return RedirectToAction(nameof(Index));
				}
				return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            var post = await _postApplicationServices.GetPostAsync(id);
            return View(_mapper.Map<EditPostModel>(post));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(long id)
        {
            try
            {
                var result = await _postApplicationServices.DeleteAsync(id, UserLoggedId);
                if (result)
                    return RedirectToAction(nameof(Index));
                return Errors(500, "Não foi possível excluir este registro.", "Excluir post.");
            }
            catch (InvalidUserException ex)
            {
                return Errors(ex.ErrorCode, ex.Message, "Excluir post.");
            }
            catch (Exception ex)
            {
                return Errors(500, ex.Message, "Excluir post.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveComment([Bind("Id,PostId,Description")] CommentPostModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _postApplicationServices.AddCommentAsync(model.PostId, userId, model.Description);
            }
            return RedirectToAction(nameof(View), new { id = model.PostId });
        }

        public async Task<IActionResult> DeleteComment(long id)
        {
            var comment = await _postApplicationServices.GetCommentAsync(id);
            return View(_mapper.Map<CommentPostModel>(comment));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentConfirm(long id)
        {
            try
            {
                var result = await _postApplicationServices.DeleteCommentAsync(id, UserLoggedId);
                if (result)
                    return RedirectToAction(nameof(Index));
                return Errors(500, "Não foi possível excluir este registro.", "Excluir comentário.");
            }
            catch (InvalidUserException ex)
            {
                return Errors(ex.ErrorCode, ex.Message, "Excluir comentário.");
            }
            catch (Exception ex)
            {
                return Errors(500, ex.Message, "Excluir comentário.");
            }
        }
    }
}
