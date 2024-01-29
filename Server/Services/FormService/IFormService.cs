namespace GzReservation.Server.Services.FormService
{
    public interface IFormService
    {
        Task<ServiceResponse<List<Form>>> GetFormsAsync();
        Task<ServiceResponse<Form>> AddNewForm(Form form);
        Task<ServiceResponse<Form>> UpdateForm(Form form);
        Task<ServiceResponse<Form>> DeleteForm(int formId);
        Task<ServiceResponse<Form>> GetForm(int formId);
        Task<ServiceResponse<List<Form>>> SearchForm(string searchText);



    }
}
