using AituConnectApi.Dto.Responses;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AituConnectApi.Controllers
{
    [ApiController]
    [Route("api/universities")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversityController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUniversities()
        {
            var universities = await _universityService.GetAllAsync();

            if (universities == null || !universities.Any())
            {
                return NotFound();
            }

            List<UniversityResponseDto> dtoList = universities
                .Select(u => new UniversityResponseDto { Id = u.Id, Name = u.Name })
                .ToList();

            return Ok(dtoList);
        }
    }
}
