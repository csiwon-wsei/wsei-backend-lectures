using HttpMethod = Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpMethod;

namespace web_api_rest_minimal.Models;

public class Link
{
    public string Rel { get; set; }
    public string Href { get; set; }
    public string Method { get; set; }
}