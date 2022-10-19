using Newtonsoft.Json;

namespace GameServerCore
{
    public class ConfigProvider
    {
        public Configuration GetConfiguration()
        {
            Configuration? configuration;
            string path = "config.json";
            if (!File.Exists(path))
            {
                configuration = new Configuration();
                File.WriteAllText(path, JsonConvert.SerializeObject(configuration, Formatting.Indented));
                return configuration;
            }
            configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(path));
            if (configuration == null)
            {
                File.Delete(path);
                return GetConfiguration();
            }
            return configuration;
        }
    }
}