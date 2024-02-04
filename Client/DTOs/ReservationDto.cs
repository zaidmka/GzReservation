namespace GzReservation.Client.DTOs
{
    public record struct ReservationDto
(
    int id,
    int doc_no,
    string full_name,
    string mother_name,
    DateTime action_date,
    DateOnly reservation_date,
    int EntityId,
    bool state,
    string uuser
);
}
