using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestPlatform.API.CustomAuthorize;
using TestPlatform.Services.DTOs.ScienceDTOs;
using TestPlatform.Services.ScienceServices;

namespace TestPlatform.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScienceController : ControllerBase
    {
        private readonly IScienceServices _scienceServices;

        public ScienceController(IScienceServices scienceServices)
        {
            _scienceServices = scienceServices;
        }
        [Authorize]
        [CustomAuthorize]
        [HttpPost]
        public async ValueTask<IActionResult> CreateScience(
            [FromBody] ScienceForCreationDto scienceForCreationDto)
        {
            return Ok(await _scienceServices.CreateScienceAsync(scienceForCreationDto));
        }
        [Authorize]
        [CustomAuthorize]
        [HttpPost]
        public async ValueTask<IActionResult> CreateScienceType(
            [FromBody] ScienceTypeForCreationDto scienceTypeForCreationDto)
        {
            return Ok(await _scienceServices.CreateScienceTypeAsync(scienceTypeForCreationDto));
        }
        [Authorize]
        [CustomAuthorize]
        [HttpDelete("{id:long}")]
        public async ValueTask<IActionResult> RemoveScience(long id)
        {
            return Ok(await _scienceServices.RemoveScienceAsync(id));
        }
        [Authorize]
        [CustomAuthorize]
        [HttpDelete("{id:long}")]
        public async ValueTask<IActionResult> RemoveScienceType(long id)
        {
            return Ok(await _scienceServices.RemoveScienceTypeAsync(id));
        }
        [Authorize]
        [CustomAuthorize]
        [HttpGet("{id:long}")]
        public async ValueTask<IActionResult> GetScience(long id)
        {
            return Ok(await _scienceServices.RetrieveScienceByIdAsync(id));
        }
        [Authorize]
        [CustomAuthorize]
        [HttpGet("{id:long}")]
        public async ValueTask<IActionResult> GetScienceType(long id)
        {
            return Ok(await _scienceServices.RetrieveScienceTypeByIdAsync(id));
        }
        
        [HttpGet]
        public IActionResult GetSciences()
        {
            return Ok(_scienceServices.RetrieveSciences());
        }
        
        [HttpGet]
        public IActionResult GetScienceTypes()
        {
            return Ok(_scienceServices.RetrieveScienceTypes());
        }
        
        [HttpGet("{name}")]
        public IActionResult GetScienceByNames(string name)
        {
            return Ok(_scienceServices.RetrieveByNameSciences(name));
        }
        
        [HttpGet("{name}")]
        public IActionResult GetScienceTypeByNames(string name)
        {
            return Ok(_scienceServices.RetrieveByNameScienceTypes(name));
        }
        
        [HttpGet("{count:int}")]
        public IActionResult GetScienceQuizCount(int count)
        {
            return Ok(_scienceServices.RetrieveByCountSciences(count));
        }
        [HttpPost]
        public async Task<IActionResult> AddPhotoScienceType([FromForm]AddPhotoDto addPhotoDto)
        {
            return Ok(await _scienceServices.AddPhotoScienceTypeAsync(addPhotoDto));
        }
        [HttpPost]
        public async Task<IActionResult> AddPhotoScience([FromForm]AddPhotoDto addPhotoDto)
        {
            return Ok(await _scienceServices.AddPhotoScienceAsync(addPhotoDto));
        }
    }
}
