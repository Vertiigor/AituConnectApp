using AituConnectApi.Dto.Responses;
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

            List<MajorResponseDto> dtoList = majors
                .Select(m => new MajorResponseDto { Id = m.Id, Name = m.Name })
                .ToList();

            return Ok(dtoList);
        }

    }
}
