using System.Runtime.Serialization;

namespace soap_app.Models;
[DataContract]
public class AppUser
{
    [DataMember] public int Id { get; set; }

    [DataMember] public string Name { get; set; }

    [DataMember] public string Email { get; set; }
}