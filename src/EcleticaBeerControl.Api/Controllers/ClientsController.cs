using EcleticaBeerControl.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcleticaBeerControl.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _repository;

        public ClientsController(IClientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Application.Domain.Entities.Client>>> AllDevices()
        {
            var response = await _repository.GetAll();
            return Ok(response);
        }
    }
}