using System.Runtime.Serialization;

namespace soap_app.Models;
[DataContract(Namespace = "http://wsei.eu.pl")]
public class RegisteredUser
{
    [DataMember] public int Id { get; set; }
    
    [DataMember] public string Name { get; set; }

    [DataMember] public string Email { get; set; }
}