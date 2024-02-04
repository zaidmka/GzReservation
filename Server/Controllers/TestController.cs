using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GzReservation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

        public class TestController : ControllerBase
        {
            private readonly List<PreReservation> _sampleData;
            private readonly ITestService _testService;

            public TestController(ITestService testService)
            {
                _testService = testService;

                _sampleData = new List<PreReservation>
        {
            new PreReservation { doc_no = 336414, full_name = "مرتضى مزهر زيدان محمد العتبي", mother_name = "نهاد عطيوي جواد", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 341904, full_name = "علاء لطيف عبد جبر العبودي", mother_name = "هدية  حمود حسن", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 345703, full_name = "ميثم صباح مؤنس جويعد الربيعاوي", mother_name = "قبيلة كاظم مهاوي", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 345711, full_name = "شاكر محمود دفار سد خان مسعدي", mother_name = "هدية خلف صالح", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 350043, full_name = "ميثم حسين امين سمين البياتي", mother_name = "بهيج علي رضا البياتي", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 351969, full_name = "اكرم نعيم عطوان عليخ الحميداوي", mother_name = "رويده حنطل سامان", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 354458, full_name = "حيدر حنون مطر حسين الساعدي", mother_name = "كاظمية شمال شراد الساعدي", EntityId = 98, Entity = null },
            new PreReservation { doc_no = 354459, full_name = "سيف كاظم محمد حسن العبيدي", mother_name = "امل حسين مصلح العبيدي", EntityId = 98, Entity = null }
        };
            }

            [HttpGet("{entityId}/{secret}")]
            public ActionResult<ServiceResponse<List<PreReservation>>> GetSampleData()
            {
                var response = new ServiceResponse<List<PreReservation>>
                {
                    Data = _sampleData,
                    Success = true,
                    Message = "Sample data retrieved successfully."
                };

                return Ok(response);
            }





            [HttpGet("message")]
            public ActionResult<string> GetMessage()
            {
                var result = _testService.GetMessage();
                return Ok(result);
            }
        }
    }

