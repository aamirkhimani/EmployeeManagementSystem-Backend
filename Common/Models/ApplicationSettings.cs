using System;
namespace Common.Models
{
    public class ApplicationSettings
    {
        public JwtSettings JwtSettings { get; set; }
    }

    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}

