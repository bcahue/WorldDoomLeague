using System.Configuration;

namespace WorldDoomLeague.WebUI.ConfigModels
{
    public sealed class DataDirectories : ConfigurationSection
    {
        public const string Name = "DataDirectories";

        [ConfigurationProperty("JsonMatchDirectory",
             DefaultValue = "/odamex/wdl-json",
             IsRequired = true)]
        public string JsonMatchDirectory { get; set; }

        [ConfigurationProperty("DemoRepository",
             DefaultValue = "/odamex/wdl-demos",
             IsRequired = true)]
        public string DemoRepository { get; set; }
    }
}