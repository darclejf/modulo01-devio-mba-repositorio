using AutoMapper;
using Blog.Api.Models;
using Blog.Data.Interfaces.Application;
using Blog.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository; //TODO para consultas que não tem regra de negócios, acessar direto o repository ou melhor passar pela camada de serviço postApplication?
        private readonly IPostApplicationServices _postApplication;
        private readonly IMapper _mapper; //TODO o automapper é ou pode ser lento como vi em algumas matérias na net?

        public PostsController(IPostRepository postRepository, IPostApplicationServices postApplication, IMapper mapper)
        {
            _postRepository = postRepository;
            _postApplication = postApplication;
            _mapper = mapper;
        }

        // GET: api/<PostsController>
        [HttpGet("{page}/{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page, string search)
        {
            var list = await _postRepository.GetPagedAsNoTrackingAsync(page, 20);
            return Ok(list.Select(_mapper.Map<PostApiModel>));
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            var post = await _postRepository.GetAsNotTrackingAsync(x => x.Id == id);
            if (post == null)
                return NotFound();
            return Ok(_mapper.Map<PostApiModel>(post));
        }

        // POST api/<PostsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PostCreateApiModel model)
        {
            //TODO validacoes
            var post = Data.Entities.Post.Create(model.Title, model.Description, model.AuthorId);
            post = await _postApplication.AddAsync(post);
            return Ok(_mapper.Map<PostApiModel>(post));
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PostUpdateApiModel model)
        {
            var result = await _postApplication.UpdateAsync(id, model.Title, model.Description);
            return Ok(result);
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postApplication.DeleteAsync(id);
            return Ok(result);
        }
    }
}
