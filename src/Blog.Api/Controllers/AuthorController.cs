using Blog.Api.Models;
using Blog.Application.Interfaces;
using Blog.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/authors")]
    [Authorize(Roles = "ADMINISTRADOR")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorRepository _authorRepository;
        private IAuthenticationApplicationServices _authenticationApplicationServices;

        public AuthorController(IAuthorRepository authorRepository, IAuthenticationApplicationServices authenticationApplicationServices)
        {
            _authorRepository = authorRepository;
            _authenticationApplicationServices = authenticationApplicationServices;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var list = await _authorRepository.GetAllAsNotTrackingAsync();
            return Ok(list);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var author = await _authorRepository.GetAsNotTrackingAsync(x => x.Id == id);
            if (author == null) 
                return NotFound();
            return Ok(author);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchApiModel model)
        {
            var list = await _authorRepository.SearchAsNotTrackingAsync(model.Search);
            return Ok(list);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var author = await _authorRepository.GetAsNotTrackingAsync(x => x.Id == id);
            if (author == null)
                return NotFound();

            if (author.User.UserName == "admin@admin.com")
                return Problem("Não é permitido excluir usuário admin@admin.com");

            _authorRepository.Delete(author);
            if (await _authenticationApplicationServices.RemoveAsync(author.UserId))
            {
                await _authorRepository.CommitAsync();
                return Ok(author);
            }
            return Problem("Não foi possível excluir usuário vinculado ao autor!");
        }
    }
}
