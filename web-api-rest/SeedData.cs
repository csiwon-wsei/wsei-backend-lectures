using core.Domain;
using core.Interfaces;
using core.Models;

namespace core;

public static class SeedData
{
    public static void Seed(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var service = provider.GetService<ISchoolService>();
            List<Student> students = new List<Student>();
            students.Add(service.AddStudent(new NewStudent(firstName: "Adam", lastName: "Kowal", birth: DateOnly.Parse("2000-10-10"),
                phone: "23456789")));
            students.Add(service.AddStudent(new NewStudent(firstName: "Ewa", lastName: "Nowak", birth: DateOnly.Parse("2001-11-23"),
                phone: "11334455")));
            students.Add(service.AddStudent(new NewStudent(firstName: "Adam", lastName: "Baron", birth: DateOnly.Parse("2002-04-03"),
                phone: "12345678")));
            var group = service.AddGroup(new NewStudentGroup("PROGS-1-2-NS"));
            students.ForEach(st => service.AssignStudentToGroup(studentId: st.Id, groupId: group.Id));
        }
    }
}