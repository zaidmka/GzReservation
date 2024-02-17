using GzReservation.Server.DTOs;

namespace GzReservation.Server.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ServiceResponse<List<Reservation>>> GetReservationsAsync();
        Task<ServiceResponse<Reservation>> AddNewReservation(ReservationDto reservationDto);
        Task<ServiceResponse<Reservation>> UpdateReservation(Reservation reservation);
        Task<ServiceResponse<Reservation>> DeleteReservation(int reservationId);
        Task<ServiceResponse<Reservation>> GetReservation(int reservationId);
        Task<ServiceResponse<List<Reservation>>> SearchReservation(string searchText);
        Task<ServiceResponse<List<int>>> GetFreeSpots();
        Task<ServiceResponse<List<int>>> GetFreeSpotsByEntity(int entityId);
        Task<ServiceResponse<List<Reservation>>> GetReservationByEntity(int entityId);
        Task<ServiceResponse<List<HourAvailability>>> GetReservationHourByDay(DateOnly reservationDate);


    }
}
