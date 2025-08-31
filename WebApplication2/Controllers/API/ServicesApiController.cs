using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesApiController : ControllerBase
    {
        private readonly IRepository<Service> _serviceRepository;

        public ServicesApiController(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            var services = await _serviceRepository.GetAllAsync();
            return Ok(services);
        }

        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
                return NotFound(new { Message = "Service not found" });

            return Ok(service);
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Service>> CreateService(Service service)
        {
            var existing = (await _serviceRepository.FindAsync(s => s.Name == service.Name)).FirstOrDefault();
            if (existing != null)
                return BadRequest(new { Message = "Service name must be unique" });

            await _serviceRepository.AddAsync(service);
            await _serviceRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetService), new { id = service.ServiceId }, service);
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Service>> UpdateService(int id, Service model)
        {
            if (id != model.ServiceId)
                return BadRequest(new { Message = "Service ID mismatch" });

            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
                return NotFound(new { Message = "Service not found" });

            service.Name = model.Name;
            service.Description = model.Description;

            _serviceRepository.Update(service);
            await _serviceRepository.SaveChangesAsync();

            return Ok(service);
        }

        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null)
                return NotFound(new { Message = "Service not found" });

            _serviceRepository.Delete(service);
            await _serviceRepository.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
