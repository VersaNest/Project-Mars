using System.Text.Json;

namespace Reqnroll_ProjectMars.Utilities
{
    public class TestSettings
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public static class ConfigReader
    {
        public static TestSettings LoadSettings()
        {
            var json = File.ReadAllText("appsettings.json");
            return JsonSerializer.Deserialize<TestSettings>(json);
        }
    }
}
