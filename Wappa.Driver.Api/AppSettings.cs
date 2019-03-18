
namespace Wappa.Driver.Api
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string MapKey { get; set; }
        public AppSettings(string connectionString, string mapKey)
        {
            ConnectionString = connectionString;
            MapKey = mapKey;
        }
    }
}
