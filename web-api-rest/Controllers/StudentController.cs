using System.Dynamic;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using core.Domain;
using core.Dto;
using core.Interfaces;
using core.Mappers;
using core.Models;
using core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

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
    
    /**
     * Metoda aynchroniczna zwracająca listę, serwis zwraca obiekty domenowe,
     * kontroler mapuje na obiekty DTO.
     * Uwaga!
     * Metoda może zwracać tylko JSON, serializer XML nie obsługuje słownika IDictionery!
     * Atrybut [Produces] ogranicza możliwe typy odpowiedzi
     */
     
    [HttpGet("dto")]
    [Produces("application/json")]
    public async Task<List<DtoStudent>> GetAll()
    {
        return (await _service.FindAllAStudentsAsync()).Select(s => StudentMapper.ToDtoStudent(s)).ToList();
    }
    
    [HttpGet("/text")]
    public ContentResult Text()
    {
        return new ContentResult() {Content = "Hello"};
        
    }
    /**
     * Metoda generuje błąd, obiekty kolekcji mają cykle - referencje do samych siebie
     */
    [HttpGet] 
    [Route("cycles")]
    [Produces("application/json")]
    public List<Student> GetByName()
    {
        return _service.FindAllAStudentsAsync().Result;
    }

    /**
     * Metoda zwraca obiekty pozbawione cykli
     */
    [AcceptVerbs("GET")]
    [Route("nocycles")]
    public async IAsyncEnumerable<Object> GetByNameNoCycles(string name)
    {
        foreach (var st in _service.FindAllAStudentsAsync().Result.Where(s => s.FirstName == name))
        {
            yield return new
            {
                Id = st.Id, 
                FirstName = st.FirstName,
                LastName = st.LastName, 
                Phone = st.Phone,
                Group = st.StudentGroup?.Name
            };
        }
    }

    /**
     * Metoda nie zwraca poprawnego statusu odpowiedzi
     */
    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    public void PostNewStudent(TimeProvider time, NewStudent student)
    {
        _service.AddStudent(student);
    }
    
    [HttpGet]
    public IResult Test()
    {
        return Results.Empty;
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

    [HttpPatch]
    [Route("payments/{studentId:int}"), Produces("application/json")]
    public IActionResult AddPayment(int studentId, [FromBody] JsonPatchDocument<StudentPayments>? patchDoc)
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