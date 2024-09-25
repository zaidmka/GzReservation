using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GzReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Dbmessage>>>> GetDbMessage()
        {
            var result = await _announcementService.GetDbMessagesListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Dbmessage>>>AddDbMessage(Dbmessage dbmessage)
        {
            var result = await _announcementService.AddDbMessagesAsync(dbmessage);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Dbmessage>>>UpdateDbMessage(Dbmessage dbmessage)
        {
            var result = await _announcementService.UpdateDbMessageAsync(dbmessage);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<Dbmessage>>>DeleteDbMessage(int dbmessageId)
        {
            var result = await _announcementService.DeleteDbMessageAsync(dbmessageId);
            return Ok(result);
        }
		[HttpGet("active")]
		public async Task<ActionResult<ServiceResponse<List<Dbmessage>>>> GetActiveDbMessage()
		{
			var result = await _announcementService.GetActiveDbMessagesListAsync();
			return Ok(result);
		}
	}
}
