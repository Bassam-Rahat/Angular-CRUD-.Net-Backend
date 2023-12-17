using Angular_and_Dotnet.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_and_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Age = e.Age,
                    DOJ = e.DOJ,
                    Gender = e.Gender,
                    IsActive = e.IsActive,
                    IsMarried = e.IsMarried,
                    DesignationTitle = _context.Designations
                                       .Where(d => d.Id == e.DesignationId)
                                       .Select(d => d.Title)
                                       .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(employees);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeCreateDto employeeCreateDto)
        {
            var designation = await _context.Designations.FirstOrDefaultAsync(d => d.Title == employeeCreateDto.DesignationTitle);

            if (designation == null)
            {
                return BadRequest("Invalid designation name.");
            }

            var employee = new Employee
            {
                FirstName = employeeCreateDto.FirstName,
                LastName = employeeCreateDto.LastName,
                Email = employeeCreateDto.Email,
                Age = employeeCreateDto.Age,
                DOJ = employeeCreateDto.DOJ,
                Gender = employeeCreateDto.Gender,
                IsActive = employeeCreateDto.IsActive,
                IsMarried = employeeCreateDto.IsMarried,
                Designation = designation,
                DesignationId = designation.Id
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeCreateDto employeedto)
        {
            var employee = _context.Employees.FirstOrDefault(d => d.Id == id);

            if (employee is null)
            {
                return BadRequest();
            }

            var designation = await _context.Designations.FirstOrDefaultAsync(d => d.Title == employeedto.DesignationTitle);

            if (designation == null)
            {
                return BadRequest("Invalid designation name.");
            }

            employee.FirstName = employeedto.FirstName;
            employee.LastName = employeedto.LastName;
            employee.Email = employeedto.Email;
            employee.Age = employeedto.Age;
            employee.DOJ = employeedto.DOJ;
            employee.Gender = employeedto.Gender;
            employee.IsActive = employeedto.IsActive;
            employee.IsMarried = employeedto.IsMarried;
            employee.Designation = designation;
            employee.DesignationId = designation.Id;

            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }

    public class EmployeeCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime DOJ { get; set; }
        public string Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsMarried { get; set; }
        public string DesignationTitle { get; set; }
    }
}
