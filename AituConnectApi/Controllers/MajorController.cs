using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AituConnectApi.Controllers
{
    [ApiController]
    [Route("api/majors")]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _majorService;

        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllMajors()
        {
            var majors = await _majorService.GetAllAsync();

            if (majors == null || !majors.Any())
            {
                return NotFound();
            }

            return Ok(
                from major in majors
                select new { major.Id, major.Name }
                );
        }

    }
}
