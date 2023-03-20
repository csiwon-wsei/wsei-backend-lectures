using System.ServiceModel;

namespace soap_app.Models;
[ServiceContract]
public interface IUserAppService
{
        [OperationContract]
        AppUser Register(string name, string email, int id);
}