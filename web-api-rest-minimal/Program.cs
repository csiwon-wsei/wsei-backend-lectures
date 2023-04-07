using System.Text.Json.Serialization;
using core;
using core.Interfaces;
using core.Mappers;
using core.Models;
using core.Services;
using Microsoft.AspNetCore.Mvc;
using web_api_rest_minimal.Models;


public class WebApiRestMinimal
{
    public static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<ISchoolService, SchoolServiceInMemory>();

        // Poniższa konfiguracja nie działa, domyślnie jest stosowany serializer System.Text.Json
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
        });

        // Konfiguracja cykli
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.WriteIndented = true;
            options.SerializerOptions.IncludeFields = true;
            //options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://www.record-it.pl",
                        "127.0.0.0");
                });
        });
        builder.Services.AddSingleton<ISchoolService, SchoolServiceInMemory>();

        var app = builder.Build();

        var handler = new HelloHandler();

        string LocalHandler() => "Hello World!";

        app.MapGet("/hello", () => "Hello World!")
            .WithName("hi");

        app.MapGet("/json", () => new {message = "Hello World", userId = 1});

        // Metoda statyczna obsługująca żądanie
        app.MapGet("/static", HelloHandler.StaticHandle);

        // Metody lokalna obsługująca żądanie
        app.MapGet("/local", LocalHandler);

        // Metoda instancyjne obsługująca żądanie 
        app.MapGet("/instance", handler.InstanceHandler);

        // 
        app.MapGet("/file", () => { return Results.Stream(File.OpenRead("c:\\data\\aa.pdf"), "application/pdf"); });

        app.MapGet("/old-path", () => Results.Redirect("/new-path"));

        app.MapGet("/download", () => Results.File("myfile.text"));

        app.MapGet("/cycles", (ISchoolService service) => { return service.FindGroupByIdAsync(1); });

        app.MapGet("/api/students/{id:int}",
                async (
                    [FromServices] ISchoolService schoolService,
                    int id,
                    LinkGenerator linker,
                    HttpContext context
                ) =>
                {
                    Student student = await schoolService.FindStudentByIdAsync(id);
                    return new
                    {
                        student.Id,
                        student.FirstName,
                        student.LastName,
                        StudentGroup = student.StudentGroup.Name,
                        Links = CreateLinks(linker, context, id, "")
                    };
                })
            .WithName("student");

        app.MapGet("/api/groups/{id:int}", async (HttpContext ctx, [FromServices] LinkGenerator generator,
            [FromServices] ISchoolService schoolService, int id) =>
        {
            var group = await schoolService.FindGroupByIdAsync(id);

            return new
            {
                Id = group.Id,
                Name = group.Name,
                Students = group.Students.Select(s => generator.GetPathByName("student", new {id = s.Id})),
                StudentsUrls = group.Students.Select(s => generator.GetUriByName(ctx, "student", new {id = s.Id}))
            };
        }).WithName("student_group");

        var group = app.MapGroup("/api/v1/students")
            .WithTags("Public");

        group.MapGet("/",
            async ([FromServices] ISchoolService service) =>
                (await service.FindAllAsync()).Select(StudentMapper.ToDtoStudent));

        group.MapGet("/{id:int}", async ([FromServices] ISchoolService service, int id)
            => StudentMapper.ToDtoStudent(await service.FindStudentByIdAsync(id))
                is DtoStudent dto
                ? Results.Ok(dto)
                : Results.NotFound()
        );

        group.MapPost("/",
                (
                        [FromServices] ISchoolService service,
                        NewStudent student
                    ) =>
                    service.AddStudent(student)
            ).WithName("create_student");
        
        
        app.Seed();
        app.Run();
    }

    public static List<Link> CreateLinks(LinkGenerator linker, HttpContext context, int id, string fields = "")
    {
        return new List<Link>
        {
            new()
            {
                Href = linker.GetUriByName(context, "student", values: new {id, fields}),
                Rel = "self",
                Method = "GET"
            },
            new()
            {
                Href = linker.GetUriByName(context, "create_student"),
                Rel = "create_student",
                Method = "POST"
            }
        };
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