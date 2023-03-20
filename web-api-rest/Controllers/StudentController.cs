using System.Dynamic;
using System.Text.Json;
using core.Interfaces;
using core.Mappers;
using core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace web_api_rest.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly ISchoolService _service;
    private static StudentPayments payments = new StudentPayments() { StudentId = 1, Payments = new List<decimal>() { 10, 20, 30 }, AccountNumber = "12345679000"};
    public StudentsController(ISchoolService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<DtoStudent>> GetAll()
    {
        return (await _service.FindAllAsync()).Select(s => StudentMapper.ToDtoStudent(s)).ToList();
        
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<DtoStudent> Get(int id)
    {
        return StudentMapper.ToDtoStudent(await _service.FindStudentByIdAsync(id));
    }
    
    
    
    
    [HttpGet("text")]
    public ContentResult GetContent()
    {
        var result = new ContentResult();
        result.Content = new {Id = 1, FirstName = "Adam", LastName = "Kowal"}.ToString();
        return result;
    }

    [HttpGet("json")]
    public JsonResult GetJson()
    {
        var student = new {Id = 1, FirstName = "Adam", LastName = "Kowal"};
        var result = new JsonResult(student);
        return result;
    }

    [HttpGet("json/formated")]
    public IActionResult GetJsonFormatted()
    {
        var student = new {Id = 1, FirstName = "Adam", LastName = "Kowal"};
        return new JsonResult(
            student,
            new JsonSerializerOptions {PropertyNamingPolicy = null}
        );
    }

    [HttpPost]
    public string Post(Student student)
    {
        return student.ToString();
    }

    [HttpPatch]
    [Route("payments/{studentId:int}"), Produces("application/json")]
    public IActionResult AddPayment(int studentId, [FromBody] JsonPatchDocument<StudentPayments> patchDoc)
    {
       
        if (patchDoc != null)
        {
            
            patchDoc.ApplyTo(payments, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new ObjectResult(payments);
        }
        return BadRequest(ModelState);        
    }
    
    [HttpPatch]
    [Route("payments/dynamic/{studentId:int}"), Produces("application/json")]
    public IActionResult AddPayment(int studentId, [FromBody] JsonPatchDocument patchDoc)
    {
        dynamic obj = new ExpandoObject();
        obj.payments = new List<decimal>() {33, 22};
        obj.account = "111222333999";
        if (patchDoc != null)
        {
            
            patchDoc.ApplyTo(obj);
            return new ObjectResult(obj);
        }
        return BadRequest(ModelState);        
    }


    [HttpGet]
    [Route("payments/{studentId:int}"), Produces("application/json")]
    public IActionResult GetPayments(int studentId)
    {
        return Ok(payments);
    }
}