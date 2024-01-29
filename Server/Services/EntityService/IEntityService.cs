namespace GzReservation.Server.Services.EntityService
{
    public interface IEntityService
    {
        Task<ServiceResponse<List<PreReservation>>> GetEntityAsync(EntityLogin entityLogin);

    }
}
