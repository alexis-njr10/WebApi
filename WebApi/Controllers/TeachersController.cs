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
    public class TeachersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TeachersController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET api/<TeachersController>/5
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter filter)
        {
            var data = await _context.Teachers
               .Skip((filter.PageNumber - 1) * filter.PageSize)
               .Take(filter.PageSize)
               .Where(x => x.DeletedAt.ToString() == "0001-01-01 00:00:00.0000000")
               .ToListAsync();
            return Ok(data);
        }

        // POST api/<TeachersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Teacher Input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.CreatedByUserId = user.Id ?? null;
            Input.CreatedAt = DateTime.Now;
            await _context.Teachers.AddAsync(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se agrego el profesor" });
            return Ok(new Response { Status = "Success", Message = "Profesor creado correctamente" });
        }

        // PUT api/<TeachersController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Teacher Input)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.UpdatedByUserId = user.Id ?? null;
            Input.UpdatedAt = DateTime.Now;
            _context.Teachers.Update(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se pudo editar el profesor" });
            return Ok(new Response { Status = "Success", Message = "Se edito el profesor" });
        }

        // DELETE api/<TeachersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Input = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name.ToString());
            Input.UpdatedByUserId = user.Id ?? null;
            Input.DeletedAt = DateTime.Now;
            _context.Teachers.Update(Input);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "No se pudo eliminar el profesor" });
            return Ok(new Response { Status = "Success", Message = "Profesor Eliminado Correctamente" });
        }
    }
}
