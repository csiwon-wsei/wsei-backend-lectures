using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace core.Dto;

public class NewStudent
{
    [Required, StringLength(maximumLength: 40)]
    public string FirstName { get; set; }
    
    [Required, StringLength(maximumLength: 40)]
    public string LastName { get; set; }
    
    [Phone]
    public string Phone { get; set; }
    
    [Required, DataType(DataType.Date)]
    public DateOnly Birth { get; set; }

    public NewStudent(string firstName, string lastName, string phone, DateOnly birth)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Birth = birth;
    }

    [JsonExtensionData(WriteData = true)]
    [JsonProperty(PropertyName = "details")]
    public IDictionary<string, JToken> ExtraData { set; get; } = new Dictionary<string, JToken>();
}