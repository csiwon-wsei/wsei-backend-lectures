using System.ComponentModel.DataAnnotations;
using core.Models;

namespace web_api_min_ex.Dto;

public class ResponseStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Group { get; set; }

    public ResponseStudentDto()
    {
    }

    public static ResponseStudentDto? of(Student s)
    {
        return s is null
            ? null
            : new ResponseStudentDto()
            {
                Id = s.Id,
                Name = $"{s.FirstName} {s.LastName}",
                Group = s.StudentGroup?.Name
            };
    }
}