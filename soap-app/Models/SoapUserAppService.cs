using System.Xml.Linq;
namespace soap_app.Models;
public class SoapUserAppService: IUserAppService
{
    private readonly ILogger _logger;

    public SoapUserAppService(ILogger<SoapUserAppService> logger)
    {
        _logger = logger;
    }

    public string Test(string s)
    {
        _logger.LogInformation($"Message from test {s}");
        return $"test: {s}";
    }

    public void XmlMethod(XElement xml)
    {
        _logger.LogInformation($"XmlMethod {xml}");
    }

    public RegisteredUser Register1(string name, string email)
    {
        var model = new RegisteredUser() {Name = name, Email = email, Id = Random.Shared.Next(1,100)};
        _logger.LogInformation($"Register {model.Id} {model.Email} {model.Name}");
        return model;
    }

    public RegisteredUser Register2(AppUser user)
    {
        return new RegisteredUser()
        {
            Name = user.Name,
            Email = user.Email,
            Id= Random.Shared.Next(1, 100)
        };
    }
}