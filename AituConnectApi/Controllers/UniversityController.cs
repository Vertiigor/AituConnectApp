using AituConnectApi.Dto;
using AituConnectApi.Models;
using AituConnectApi.Services.Abstractions;
using AituConnectApi.Services.Implementations;
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

            return Ok(
                from university in universities
                select new { university.Id, university.Name }
                );
        }
    }
}
