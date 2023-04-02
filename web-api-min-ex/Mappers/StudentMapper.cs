using core.Models;
using web_api_min_ex.Dto;

namespace web_api_min_ex.Mappers;

public class StudentMapper
{
    public static ResponseStudentDto of(Student s)
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