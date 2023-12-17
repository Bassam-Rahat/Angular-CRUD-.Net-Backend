using Angular_and_Dotnet.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_and_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DesignationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Designation>>> GetDesignations()
        {
            return await _context.Designations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Designation>> GetDesignation(Guid id)
        {
            var designation = await _context.Designations.FindAsync(id);

            if (designation == null)
            {
                return NotFound();
            }

            return designation;
        }

        [HttpPost]
        public async Task<ActionResult<Designation>> CreateDesignation(Designation designation)
        {
            _context.Designations.Add(designation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDesignation), new { id = designation.Id }, designation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDesignation(Guid id, Designation designation)
        {
            if (id != designation.Id)
            {
                return BadRequest();
            }

            _context.Entry(designation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignation(Guid id)
        {
            var designation = await _context.Designations.FindAsync(id);

            if (designation == null)
            {
                return NotFound();
            }

            _context.Designations.Remove(designation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DesignationExists(Guid id)
        {
            return _context.Designations.Any(d => d.Id == id);
        }
    }
}
