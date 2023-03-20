using System.ComponentModel.DataAnnotations;

namespace core.Models;

public class NewStudentGroup
{
    [Required, StringLength(6)]
    public string Name { get; set; }

    public NewStudentGroup(string name)
    {
        Name = name;
    }
}