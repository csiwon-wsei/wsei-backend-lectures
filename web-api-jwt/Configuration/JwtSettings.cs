namespace web_api_jwt.Data;

public class JwtSettings(IConfiguration configuration)
{
    private const string Section = "JwtSettings";
    public string? Issuer => configuration.GetSection(Section).GetSection("ValidIssuer").Value;
    
    public string? Audience => configuration.GetSection(Section).GetSection("ValidAudience").Value;
    // Uwaga!!!
    // Secret powino być zapisane w zmiennej środowiskowej!!!
    public string? Secret => configuration.GetSection(Section).GetSection("Secret").Value;
}