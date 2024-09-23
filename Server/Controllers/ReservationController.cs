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
        [HttpPost("/nextweek")]
        public async Task<ActionResult<ServiceResponse<Reservation>>> CreateFormNextWeek(ReservationDto reservationDto)
        {
            var result = await _reservationService.AddNewReservationNextWeek(reservationDto);
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
        [HttpGet("nextweekspots/{entityId}")]
        public async Task<ActionResult<ServiceResponse<List<int>>>> GetFreeSpotsPerEntityNextWeek(int entityId)
        {
            var result = await _reservationService.GetFreeSpotsByEntityNextWeek(entityId);
            return Ok(result);
        }
        [HttpGet("reservations/{entityId}")]
        public async Task<ActionResult<ServiceResponse<List<Reservation>>>>GetReservationsByEntity(int entityId)
        {
            var result = await _reservationService.GetReservationByEntity(entityId);
            return Ok(result);
        }
        [HttpGet("reservationsnextweek/{entityId}")]
        public async Task<ActionResult<ServiceResponse<List<Reservation>>>> GetReservationsByEntityNextWeek(int entityId)
        {
            var result = await _reservationService.GetReservationByEntityNextWeek(entityId);
            return Ok(result);
        }

        [HttpGet("reservationhour/{reservationDate}")]
        public async Task<ActionResult<ServiceResponse<List<HourAvailability>>>> GetReservationByHours(DateOnly reservationDate)
        {
            var result = await _reservationService.GetReservationHourByDay(reservationDate);
            return Ok(result);
        }
        [HttpGet("dailylimit/{entityId}")]
        public async Task<ActionResult<ServiceResponse<int>>>GetDailyReservationLimitPerEntity(int entityId)
        {
            var result = await _reservationService.GetDaliyLimitPerEntity(entityId);
            return Ok(result);
        }

    }
}
