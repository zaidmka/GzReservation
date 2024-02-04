namespace GzReservation.Client.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ServiceResponse<List<int>>> GetReservationSpotsAsync(int entityId);
    }
}
