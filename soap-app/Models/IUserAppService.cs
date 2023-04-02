using System.ServiceModel;

namespace soap_app.Models;
[ServiceContract(Namespace = "http://wsei.edu.pl/")]
public interface IUserAppService
{
        [OperationContract(Action = "register1")]
        RegisteredUser Register1(string name, string email);
        
        [OperationContract(Action = "register2")]
        RegisteredUser Register2(AppUser user);
}