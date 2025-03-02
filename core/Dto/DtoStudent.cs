using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace core.Dto
{
    public class DtoStudent
    {
        [JsonProperty(PropertyName = "studentId")]
        public int Id { get; init; }
        [JsonProperty(Required=Required.Always, PropertyName = "firstName")]
        public string FirstName { get; init; }
    
        [JsonProperty(Required=Required.Always, PropertyName = "lastName")]
        public string LastName { get; init; }
    
        [JsonProperty(Required = Required.Default, PropertyName = "phoneNumber")]
        public string Phone { get; init; }
    
        [JsonProperty(PropertyName = "group")]
        public string StudentGroup { get; init; }
    
        [JsonProperty(PropertyName = "birthDate")]
        public DateOnly Birth { get; init; }

        [JsonExtensionData(WriteData = true)]
        [JsonProperty(PropertyName = "details")]
        public IDictionary<string, JToken> ExtraData { set; get; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Phone)}: {Phone}, {nameof(Birth)}: {Birth}, {nameof(ExtraData)}: {ExtraData}";
        }
    
    
    }
}