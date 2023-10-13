using Microsoft.AspNetCore.Mvc;
using TestPlatform.Services.DTOs.QuizDTOs;
using TestPlatform.Services.QuizServices;

namespace TestPlatform.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizServices _quizServices;

    public QuizController(IQuizServices quizServices)
    {
        _quizServices = quizServices;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QuizForCreationDto quizForCreationDto)
    {
        return Created("" , await _quizServices.CreateAsync(quizForCreationDto));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(long quizId)
    {
        return Ok(await _quizServices.RemoveAsyncAsync(quizId));
    }

    [HttpGet]
    public IActionResult Gets(long scienceId)
    {
        return Ok(_quizServices.GetByScienceId(scienceId));
    }
}