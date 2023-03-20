using core;
using core.Interfaces;
using core.Mappers;
using core.Models;
using core.Services;
using Microsoft.AspNetCore.Mvc;

public class WebApiRestMinimal
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddSingleton<ISchoolService, SchoolServiceInMemory>();
        
        var app = builder.Build();

        var handler = new HelloHandler();
        
        string LocalHandler() => "Hello World!";
        
        app.MapGet("/hello", () => "Hello World!")
            .WithName("hi");
        
        app.MapGet("/", (LinkGenerator linker) => 
            $"The link to the hello route is {linker.GetPathByName("hi", values: null)}");
        
        app.MapGet("/json", () => new {message = "Hello World", userId = 1});
        
        app.MapGet("/static", HelloHandler.StaticHandle);
        
        app.MapGet("/local", LocalHandler);
        
        app.MapGet("/instance", handler.InstanceHandler);

        var group = app.MapGroup("/api/students")
            .WithTags("Public");
        
        group.MapGet("/", async ([FromServices] ISchoolService service) => (await service.FindAllAsync()).Select(StudentMapper.ToDtoStudent));
       
        group.MapGet("/{id:int}", async ([FromServices] ISchoolService service, int id) 
            => StudentMapper.ToDtoStudent(await service.FindStudentByIdAsync(id)) is DtoStudent dto ? Results.Ok(dto) : Results.NotFound()) ;
        
        group.MapPost("/", ([FromServices] ISchoolService  service, NewStudent student) => service.AddStudent(student));

        app.Seed();
        
        app.Run();
    }
}

class HelloHandler
{
    public static string StaticHandle()
    {
        return "Hello World!";
    }
    public string InstanceHandler()
    {
        return "Hello World!";
    }
}
