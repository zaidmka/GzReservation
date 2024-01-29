using GzReservation.Shared;

namespace GzReservation.Client.Services.SecurityFormService
{
    public interface ISecurityFormService
    {
        event Action FormChange;
        List<Form> Forms { get; set; }
        List<Form> AdminForms { get; set; }
        public string LastSearchText { get; set; }

        Task<ServiceResponse<Form>> CreateForm(Form form);
        Task<Form> UpdateForm(Form form);
        Task DeleteForm(int formId);
        Task<ServiceResponse<Form>> GetForm(int formId);
        Task GetForms();
        string message { get; set; }
        Task searchForms(string searchText);

    }
}
