using Microsoft.AspNetCore.Mvc;
using TestPlatform.Services.ExamServices;

namespace TestPlatform.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> Create([FromBody] long scienceId)
        {
            return Ok(scienceId);
        }
    }
}
