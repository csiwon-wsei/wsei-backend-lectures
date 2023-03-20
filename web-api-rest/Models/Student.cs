

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace web_api_rest.Models;

[JsonObject(MemberSerialization.OptIn)]
public class Student
{
    [JsonProperty(PropertyName = "studentId")]
    public int Id { get; set; }
    [JsonProperty(Required=Required.Always)]
    public string FirstName { get; set; }
    
    [JsonIgnore]
    public string LastName { get; set; }
    
    [JsonProperty(Required = Required.Default)]
    public string Phone { get; set; }
    
    public DateOnly Birth { get; set; }
    [JsonExtensionData]
    public IDictionary<string, JToken> ExtraData { set; get; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Phone)}: {Phone}, {nameof(Birth)}: {Birth}, {nameof(ExtraData)}: {ExtraData}";
    }
}

