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
    public class CoursesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CoursesController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET api/<CoursesController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            var data = await _context.Courses
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .Where(x => x.DeletedAt.ToString() == "0001-01-01 00:00:00.0000000")
               .ToListAsync();
            return Ok(data);
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course Input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.CreatedByUserId = user.Id ?? null;
            Input.CreatedAt = DateTime.Now;
            await _context.Courses.AddAsync(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se agrego el curso" });
            return Ok(new Response { Status = "Success", Message = "Curso creado correctamente" });
        }

        // PUT api/<CoursesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Course Input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.UpdatedByUserId = user.Id ?? null;
            Input.UpdatedAt = DateTime.Now;
            _context.Courses.Update(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se pudo editar el curso" });
            return Ok(new Response { Status = "Success", Message = "Se edito el curso" });
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Input = await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.UpdatedByUserId = user.Id ?? null;
            Input.DeletedAt = DateTime.Now;
            _context.Courses.Update(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se pudo eliminar el curao" });
            return Ok(new Response { Status = "Success", Message = "Curso Eliminado Correctamente" });
        }
    }
}
