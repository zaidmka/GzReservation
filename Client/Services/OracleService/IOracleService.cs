namespace GzReservation.Client.Services.OracleService
{
    public interface IOracleService
    {
        event Action OracleChange;
        Form OracleData { get; set; }
        Task GetDataAsync(int docNo);

        Task<ServiceResponse<List<PreReservation>>> GetPreValidRecords(int entityId,string secret);
    }
}
