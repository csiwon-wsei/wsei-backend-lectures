using core.Domain;
using core.Interfaces;
using core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace web_api_rest.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class StudentGroupController : ControllerBase
{
    private readonly ISchoolService _service;

    public StudentGroupController(ISchoolService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("{groupId:int}/students")]
    public  async IAsyncEnumerable<Student> GetStudents(int groupId)
    {
        var  group = await _service.FindGroupByIdAsync(groupId);
        var students = group.Students.AsEnumerable();
        foreach (var s in students)
        {
            yield return s;
        }
    }
    
    [HttpPatch]
    [Route("{groupId:int}"), Produces("application/json")]
    public async Task<IActionResult> UpdateGroup(int groupId,[FromBody] JsonPatchDocument<DtoStudentGroup> patchGroup)
    {
        if (patchGroup != null)
        {
            var group = await _service.FindGroupByIdAsync(groupId);
            var gr = new DtoStudentGroup() { Id = group.Id, Name = group.Name, Students = group.Students.ToList() };
            patchGroup.ApplyTo(gr, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new ObjectResult(gr);
        }
        return BadRequest(ModelState);
    }
}