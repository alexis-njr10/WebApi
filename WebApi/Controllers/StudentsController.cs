using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public StudentsController(
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

        // GET api/<StudentsController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            var data = await _context.Students
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .Where(x => x.DeletedAt.ToString() == "0001-01-01 00:00:00.0000000")
               .ToListAsync();
            return Ok(data);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student Input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.CreatedByUserId = user.Id ?? null;
            Input.CreatedAt = DateTime.Now;
            await _context.Students.AddAsync(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se agrego el estudiante" });
            return  Ok(new Response { Status = "Success", Message = "Estudiante creado correctamente" });
        }

        // PUT api/<StudentsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Student Input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.UpdatedByUserId = user.Id ?? null;
            Input.UpdatedAt = DateTime.Now;
            _context.Students.Update(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se pudo editar el estudiante" });
            return Ok(new Response { Status = "Success", Message = "Se edito el estudiante" });
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Input = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.UpdatedByUserId = user.Id ?? null;
            Input.DeletedAt = DateTime.Now;
            _context.Students.Update(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se pudo eliminar el estudiante" });
            return Ok(new Response { Status = "Success", Message = "Estudiante Eliminado Correctamente" });
        }
    }
}
