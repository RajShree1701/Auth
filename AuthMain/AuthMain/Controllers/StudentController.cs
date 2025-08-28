using AuthMain.Model;
using AuthMain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AuthMain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService) =>
            _studentService = studentService;

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public async Task<List<Student>> Get()
        {

            return await _studentService.GetAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Admin,Student")]
        public async Task<ActionResult<Student>> Get(string id)
        {
            var student = await _studentService.GetAsync(id);
            if (student is null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> Post(Student student)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId))
             {
                return Unauthorized("User ID not found in token.");
            }

            student.UserId = userId;
            await _studentService.CreateAsync(student);

            return Ok(student);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(string id, Student updateStudent)
        {
            var student = await _studentService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }
            updateStudent.Id = id;

            await _studentService.UpdateAsync(id, updateStudent);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetAsync(id);

            if (student is null)
            {
                return NotFound();
            }
            await _studentService.DeleteAsync(id);
            return Ok("Deleted");
        }
    }
}
