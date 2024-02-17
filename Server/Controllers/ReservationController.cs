using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GzReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Reservation>>> CreateForm(ReservationDto reservationDto)
        {
            var result = await _reservationService.AddNewReservation(reservationDto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<int>>>> GetFreeSpots()
        {
            var result = await _reservationService.GetFreeSpots();
            return Ok(result);
        }
        [HttpGet("{entityId}")]
        public async Task<ActionResult<ServiceResponse<List<int>>>> GetFreeSpotsPerEntity(int entityId)
        {
            var result = await _reservationService.GetFreeSpotsByEntity(entityId);
            return Ok(result);
        }
        [HttpGet("reservations/{entityId}")]
        public async Task<ActionResult<ServiceResponse<List<Reservation>>>>GetReservationsByEntity(int entityId)
        {
            var result = await _reservationService.GetReservationByEntity(entityId);
            return Ok(result);
        }

        [HttpGet("reservationhour/{reservationDate}")]
        public async Task<ActionResult<ServiceResponse<List<HourAvailability>>>> GetReservationByHours(DateOnly reservationDate)
        {
            var result = await _reservationService.GetReservationHourByDay(reservationDate);
            return Ok(result);
        }


    }
}
