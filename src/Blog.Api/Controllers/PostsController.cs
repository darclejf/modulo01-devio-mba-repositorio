using AutoMapper;
using Blog.Api.Models;
using Blog.Application.Interfaces;
using Blog.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    [Route("api/posts")]
    [Authorize(Roles = "ADMINISTRADOR,AUTOR")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //private readonly IPostRepository _postRepository; //TODO para consultas que não tem regra de negócios, acessar direto o repository ou melhor passar pela camada de serviço postApplication?
        private readonly IPostApplicationServices _postApplication;
        private readonly IMapper _mapper; //TODO o automapper é ou pode ser lento como vi em algumas matérias na net?

        public PostsController(IPostApplicationServices postApplication, IMapper mapper)
        {
            //_postRepository = postRepository;
            _postApplication = postApplication;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var list = await _postApplication.GetPostsAsync(1, 10000);
            return Ok(list.Select(_mapper.Map<PostApiModel>));
        }

        [AllowAnonymous]
        [HttpGet("page/{page}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page)
        {
            var list = await _postApplication.GetPostsAsync(page);
            return Ok(list.Select(_mapper.Map<PostApiModel>));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var post = await _postApplication.GetPostAsync(id);
            if (post == null)
                return NotFound();
            return Ok(_mapper.Map<PostApiModel>(post));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PostCreateApiModel model)
        {
            var userLoggedId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var create = new CreatePostModel
            {
                Active = true,
                Description = model.Description,
                SubTitle = model.SubTitle,
                Title = model.Title,
            };

            var post = await _postApplication.AddAsync(create, userLoggedId);
            return Ok(_mapper.Map<PostApiModel>(post));
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] PostUpdateApiModel model)
        {
            var userLoggedId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var create = new EditPostModel
            {
                Active = true,
                Description = model.Description,
                SubTitle = model.SubTitle,
                Title = model.Title,
            };

            var post = await _postApplication.UpdateAsync(create, userLoggedId);
            return Ok(_mapper.Map<PostApiModel>(post));
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _postApplication.DeleteAsync(id, null);
            return Ok(result);
        }

        [HttpPut("{id:long}/comment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutComment(long id, [FromBody] CommentPostModel model)
        {
            var userLoggedId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _postApplication.AddCommentAsync(id, userLoggedId, model.Description);
            return Ok(_mapper.Map<PostApiModel>(post));
        }

        [HttpDelete("{id:long}/comment/{idComment:long}")]
        public async Task<IActionResult> DeleteComment(long id, long idComment)
        {
            var userLoggedId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _postApplication.DeleteCommentAsync(idComment, userLoggedId);
            return Ok(_mapper.Map<PostApiModel>(post));
        }
    }
}
