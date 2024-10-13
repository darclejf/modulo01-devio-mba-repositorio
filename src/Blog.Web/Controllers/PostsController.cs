using Blog.Data.Interfaces.Application;
using Blog.Data.Models;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostApplicationServices _postApplicationServices;

        public PostsController(IPostApplicationServices postApplicationServices)
        {
            _postApplicationServices = postApplicationServices;
        }


        public async Task<IActionResult> Index()
        {
            var posts = await _postApplicationServices.GetPostsAsync();
            return View(posts.Select(x => new PostViewModel(x))); //TODO o automapper é ou pode ser lento como vi em algumas matérias na net?
        }

        public async Task<IActionResult> View(long id)
        {
            var post = await _postApplicationServices.GetPostAsync(id);
            return View(new PostViewModel(post));
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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var post = await _postApplicationServices.AddAsync(model, userId);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveComment([Bind("Id,PostId,Description")] CommentPostModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _postApplicationServices.AddCommentAsync(model.PostId, userId, model);
            }
            return RedirectToAction(nameof(View), new { id = model.PostId });
        }
    }
}
