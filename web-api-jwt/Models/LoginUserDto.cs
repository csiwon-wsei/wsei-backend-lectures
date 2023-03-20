using System.ComponentModel.DataAnnotations;

namespace web_api_jwt.Models;

public class LoginUserDto
{
    [Required]
    public String LoginName { get; set; }
    [Required]
    public String Password { get; set; }
}