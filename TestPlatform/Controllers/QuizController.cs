using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.API.CustomAuthorize;
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
    [Authorize]
    [CustomAuthorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] QuizForCreationDto quizForCreationDto)
    {
        return Created("" , await _quizServices.CreateAsync(quizForCreationDto));
    }
    [Authorize]
    [CustomAuthorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(long quizId)
    {
        return Ok(await _quizServices.RemoveAsyncAsync(quizId));
    }
    [Authorize]
    [CustomAuthorize]
    [HttpGet]
    public IActionResult Gets(long scienceId)
    {
        return Ok(_quizServices.GetByScienceId(scienceId));
    }
}