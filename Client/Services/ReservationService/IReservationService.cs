using GzReservation.Client.DTOs;

namespace GzReservation.Client.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ServiceResponse<List<int>>> GetReservationSpotsAsync(int entityId);
        Task<ServiceResponse<Reservation>> AddReservation(ReservationDto reservationDto);
        Task<ServiceResponse<List<Reservation>>> GetReservationByEntity(int entityId);
        Task<ServiceResponse<List<Reservation>>> GetReservationByEntityNextWeek(int entityId);

        Task<ServiceResponse<List<HourAvailability>>> GetReservationByHour(DateOnly reservationDate);
        Task<ServiceResponse<List<int>>> GetReservationSpotsAsyncNextWeek(int entityId);
        Task<ServiceResponse<Reservation>> AddReservationNextWeek(ReservationDto reservationDto);
        Task<ServiceResponse<int>>GetDailyLimitbyEntity(int entityId);
    }
}
