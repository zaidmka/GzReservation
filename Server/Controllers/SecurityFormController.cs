using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GzReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityFormController : ControllerBase
    {
        private readonly IFormService _formService;

        public SecurityFormController(IFormService formService)
        {
            _formService = formService;
        }
        [HttpGet, Authorize]
        public async Task<ActionResult<ServiceResponse<List<Form>>>> GetForms()
        {
            var result = await _formService.GetFormsAsync();
            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ServiceResponse<Form>>> CreateForm(Form form1)
        {
            var result = await _formService.AddNewForm(form1);
            return Ok(result);
        }
        [HttpPut, Authorize]
        public async Task<ActionResult<ServiceResponse<Form>>> UpdateForm(Form form)
        {
            var result = await _formService.UpdateForm(form);
            return Ok(result);
        }
        [HttpDelete, Authorize]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteFrom(int formId)
        {
            var result = await _formService.DeleteForm(formId);
            return Ok(result);
        }
        [HttpGet("search/{searchText}"),Authorize]
        public async Task<ActionResult<ServiceResponse<List<Form>>>> SearchForms(string searchText)
        {
            var result = await _formService.SearchForm(searchText);
            return Ok(result);
        }
        [HttpGet("form/{formId}"), Authorize]
        public async Task<ActionResult<ServiceResponse<Form>>> GetForm(int formId)
        {
            var result = await _formService.GetForm(formId);
            return Ok(result);
        }

    }
}
