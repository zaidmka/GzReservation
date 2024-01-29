
namespace GzReservation.Server.Services.EntityService
{
    public class EntityService : IEntityService
    {
        private readonly DataContext _dataContext;
        private readonly IOracleService _oracleService;

        public EntityService(DataContext dataContext, IOracleService oracleService)
        {
            _dataContext = dataContext;
            _oracleService = oracleService;
        }
        public async Task<ServiceResponse<List<PreReservation>>> GetEntityAsync(EntityLogin entityLogin)
        {
            var entity = await _dataContext.entities
                .Where(e => e.id == entityLogin.entityId && e.password == entityLogin.entitySecret)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                return new ServiceResponse<List<PreReservation>>
                {
                    Data = null,
                    Success = false,
                    Message = "Entity Not Found"
                };
            }
            else
            {
                var oracleResponse = await _oracleService.GetRecoredsByEntity(entity.id);
                if (!oracleResponse.Success || oracleResponse.Data == null)
                {
                    return new ServiceResponse<List<PreReservation>>
                    {
                        Data = null,
                        Success = false,
                        Message = oracleResponse.Message ?? "No Records Found"
                    };

                }
                else
                {
                    return new ServiceResponse<List<PreReservation>>
                    {
                        Data = oracleResponse.Data, // Use the Data from the oracle service response
                        Success = true,
                        Message = "Oracle Records"
                    };

                }
            }
        }

    }
}
