using Blog.Data.Entities;
using Blog.Data.Interfaces.Application;
using Blog.Data.Interfaces.Application.Models;
using Blog.Data.Interfaces.Repositories;
using Blog.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Blog.Data.Application
{
    public class AuthenticationApplicationServices : IAuthenticationApplicationServices
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettingsModel _jwtSettings;
        private readonly IAuthorRepository _authorRepository;

        public AuthenticationApplicationServices(
            SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettingsModel> jwtSettings,
            IAuthorRepository authorRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authorRepository = authorRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> RegisterAsync(RegisterUserModel model)
        {
            var result = await RegisterAuthorAsync(model);
            if (result.Succeeded)
            {
                return await GerarJwt(model.Email);
            }
            return string.Empty;
        }

        public async Task<IdentityResult> RegisterAuthorAsync(RegisterUserModel model)
        {
            var user = Activator.CreateInstance<IdentityUser>();
            user.UserName = model.Email;
            user.Email = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                var author = Author.Create(model.Name, user);
                await _authorRepository.InsertAsync(author);
                await _authorRepository.CommitAsync();
            }
            return result;
        }

        //public async Task<IdentityUser> GetCurrentUser()
        //{
        //    return await _userManager.GetUserAsync(ClaimTypes.Name);
        //}

        private async Task<string> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName)
            //};

            //// Adicionar roles como claims
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }
    }
}
