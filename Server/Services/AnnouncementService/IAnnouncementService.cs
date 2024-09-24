namespace GzReservation.Server.Services.AnnouncementService
{
    public interface IAnnouncementService
    {
        Task<ServiceResponse<List<Dbmessage>>> GetDbMessagesListAsync();
        Task<ServiceResponse<Dbmessage>>AddDbMessagesAsync(Dbmessage dbmessage);
        Task<ServiceResponse<Dbmessage>> DeleteDbMessageAsync(int dbmessageId);
        Task<ServiceResponse<Dbmessage>>UpdateDbMessageAsync(Dbmessage dbmessage);
        Task<ServiceResponse<List<Dbmessage>>> GetActiveDbMessagesListAsync();


    }
}
