namespace GzReservation.Client.Services.MessageService
{
	public interface IMessageService
	{
		Task<ServiceResponse<List<Message>>> SendMessageReservationAsync(int doc_no, string reservationdate, string reservationhour);
	}
}
