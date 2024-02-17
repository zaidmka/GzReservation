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
       

    }
}
