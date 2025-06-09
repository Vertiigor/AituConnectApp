using AituConnectApi.Dto.Responses;
using AituConnectApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AituConnectApi.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllAsync();

            if (subjects == null || !subjects.Any())
            {
                return NotFound();
            }

            List<SubjectResponseDto> dtoList = subjects
                .Select(s => new SubjectResponseDto { Id = s.Id, Name = s.Name })
                .ToList();

            return Ok(dtoList);
        }
    }
}
