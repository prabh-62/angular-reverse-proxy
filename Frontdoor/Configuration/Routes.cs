namespace Frontdoor.Configuration
{
    public class HostedApp
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Destination { get; set; }
    }

    public class Routes
    {
        public HostedApp[] GetWebApplications()
        {
            var webApplications = new[] {
                new HostedApp {Name = "support", Prefix = "/support", Destination = "http://localhost:4201"},
                new HostedApp {Name = "admin", Prefix = "/admin", Destination = "http://localhost:4202"}
            };
            return webApplications;
        }
    }
}