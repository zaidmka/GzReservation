namespace GzReservation.Server.Services.OracleService
{
    public interface IOracleService
    {
        Task<ServiceResponse<Form>> GetDataAsync(int docNo);
        string GetMessage();
        Task<ServiceResponse<List<PreReservation>>> GetRecoredsByEntity(int entityCode);

    }
}
