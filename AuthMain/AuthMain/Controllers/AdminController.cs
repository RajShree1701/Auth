using AuthMain.Model;
using AuthMain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthMain.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)=>
            _adminService = adminService;

        [HttpGet]
        public async Task<List<Admin>> Get() =>
            await _adminService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Admin>> Get(string id)
        {
            var admin = await _adminService.GetAsync(id);
            if(admin is null)
            {
                return NotFound();
            }
            return admin;
        }

        [HttpPost]
       
        public async Task<IActionResult> Post(Admin admin)
        {
            var userId = User.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            admin.UserId = userId;
            await _adminService.CreateAsync(admin);

            return Ok(admin);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Admin updateAdmin)
        {
            var admin= await _adminService.GetAsync(id);

            if(admin is null)
            {
                return NotFound();
            }
            updateAdmin.Id= id;

            await _adminService.UpdateAsync(id, updateAdmin);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var admin=await _adminService.GetAsync(id);

            if(admin is null)
            {
                return NotFound();
            }
            await _adminService.DeleteAsync(id);
            return NoContent();
        }
    }
}
