namespace Blog.Data.Interfaces.Application.Models
{
    public class JwtSettingsModel
    {
        public string Secret { get; set; }
        public int Expires { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
