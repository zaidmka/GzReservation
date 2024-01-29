using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GzReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly IEntityService _entityService;

        public EntityController(IEntityService entityService)
        {
            _entityService = entityService;
        }
        [HttpGet("{entityId}/{secret}")] // Using '/' instead of ',' for standard RESTful URL format
        public async Task<ActionResult<ServiceResponse<List<PreReservation>>>> GetOracleRecordByEntity(int entityId, string secret)
        {
            // Create the EntityLogin object from the provided parameters
            EntityLogin entityLogin = new EntityLogin
            {
                entityId = entityId,
                entitySecret = secret
            };

            // Call the service method with the constructed EntityLogin object
            var response = await _entityService.GetEntityAsync(entityLogin);

            // Return the response
            return Ok(response);
        }

    }
}
