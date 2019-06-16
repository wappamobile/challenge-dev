using System;
using System.Collections.Generic;
using System.Text;

namespace MicroservicesPlatform.Configuration
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
    }

    public class AuthService
    {
        public string Address { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Audience { get; set; }
    }

    public class AuthenticationConfig
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}
