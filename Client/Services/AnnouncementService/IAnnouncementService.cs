namespace GzReservation.Client.Services.AnnouncementService
{
    public interface IAnnouncementService
    {
        Task<ServiceResponse<List<Dbmessage>>> GetActiveDbMessage();
    }
}
