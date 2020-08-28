namespace ApexLoader
{
    public class ApexConfigs
    {
        public Config Apex1 { get; set; } = new Config();
        public Config Apex2 { get; set; } = new Config();
    }

    public class Config
    {
        public bool Active { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
    }
}