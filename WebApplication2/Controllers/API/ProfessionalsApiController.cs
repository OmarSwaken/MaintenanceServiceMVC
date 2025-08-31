using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.Repositories;
using Azure.Core;

namespace MaintenanceServiceMVC.Controllers.API
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfessionalsApiController : ControllerBase
    {
        private readonly IRepository<Professional> _professionalRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<Request> _requestRepository;

        public ProfessionalsApiController(
            IRepository<Professional> professionalRepository,
            IRepository<Service> serviceRepository,
            IRepository<Request> requestRepository)
        {
            _professionalRepository = professionalRepository;
            _serviceRepository = serviceRepository;
            _requestRepository = requestRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professional>>> GetProfessionals()
        {
            var professionals = await _professionalRepository.GetAllAsync();
            return Ok(professionals);
        }

        
       
    }
}
