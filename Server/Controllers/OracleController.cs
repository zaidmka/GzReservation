using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GzReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OracleController : ControllerBase
    {
        private readonly IOracleService _oracleService;

        public OracleController(IOracleService oracleService)
        {
            _oracleService = oracleService;
        }

        [HttpGet("{docNo}")]

        public async Task<ActionResult<ServiceResponse<Form>>> GetData(int docNo)
        {
            try
            {
                var response = await _oracleService.GetDataAsync(docNo);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response.Message);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_oracleService.GetMessage());
        }

        [HttpGet("entity/{entityId}")]

        public async Task<ActionResult<ServiceResponse<PreReservation>>> GetRecoredsByEntity(int entityId)
        {
            try
            {
                var response = await _oracleService.GetRecoredsByEntity(entityId);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound(response.Message);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, ex.Message);
            }
        }
    }
}
