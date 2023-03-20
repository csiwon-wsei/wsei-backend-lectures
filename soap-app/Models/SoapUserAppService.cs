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

    public AppUser Register(string name, string email, int id)
    {
        var model = new AppUser() {Name = name, Email = email, Id = id};
        _logger.LogInformation($"Register {model.Id} {model.Email} {model.Name}");
        return model;
    }
}