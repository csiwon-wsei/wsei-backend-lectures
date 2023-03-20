using core.Models;
using Newtonsoft.Json.Linq;

namespace core.Mappers;

public class StudentMapper
{
    public static DtoStudent ToDtoStudent(Student student)
    {
        return student is null ? null: new DtoStudent()
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Birth = student.Birth,
            StudentGroup = student.StudentGroup.Name,
            Phone = student.Phone,
            ExtraData = new Dictionary<string, JToken>()
            {
                { "matureCertificate", "świadectwo maturalne wydane w 2021 przez dyrektora szkoły nr I" },
                { "clubMemberships", "CERT, WSEI-Innovation" }
            }
        };
    }
}