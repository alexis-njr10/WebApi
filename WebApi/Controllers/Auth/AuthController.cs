using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var jwtExpires = _configuration["Jwt:Expires"];
                int expires = Convert.ToInt32(jwtExpires);
                var token = GetToken(authClaims, expires);
                var encryptedToken = new JwtSecurityTokenHandler().WriteToken(token);

                HttpContext.Response.Cookies.Append("cookie_token", encryptedToken, new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(expires),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None

                });

                return Ok(new
                {
                    token = encryptedToken,
                    expiration = token.ValidTo.ToString("dd-MM-yyyy HH:mm:ss")
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserModel Input)
        {
            var userExists = await _userManager.FindByNameAsync(Input.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            var user = new ApplicationUser{
                Name = Input.Name,
                Lastname = Input.Lastname,
                DocumentTypeId = Input.DocumentTypeId,
                DocumentNumber = Input.DocumentNumber,
                Address = Input.Address,
                Email = Input.Email,
                Gender = Input.Gender,
                LockoutEnabled = false,
                PhoneNumber = Input.PhoneNumber,
                 SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Input.DocumentNumber.ToString()
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("cookie_token");
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims, int expires)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? "";
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(expires),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                );

            return token;
        }

    }
}
