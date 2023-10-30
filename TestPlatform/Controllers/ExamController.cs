using Microsoft.AspNetCore.Mvc;
using TestPlatform.Services.DTOs.ExamDTOs;
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
        public async ValueTask<IActionResult> Create([FromBody]ExamForCreationDto examForCreationDto)
        {
            return Created("",await _examService.CreateAsync(examForCreationDto));
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetExams(long userId)
        {
            return Ok(await _examService.GetExamsOfUserAsync(userId));
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetHistoryExam(long examId)
        {
            return Ok(await _examService.GetSolvedQuizzesOfExamAsync(examId));
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetResultExam(long examId)
        {
            return Ok(await _examService.GetResultExamAsync(examId));
        }

        [HttpPost]
        public async ValueTask<IActionResult> QuizOrResult([FromBody] GetQuizInExamDto getQuizInExamDto)
        {
            try
            {
                return Ok(await _examService.FirstOrNextQuizAsync(getQuizInExamDto));
            }
            catch (NullReferenceException e)
            {
                return Ok(await _examService.GetResultExamAsync(getQuizInExamDto.examId));
            }
        }
    }
}
