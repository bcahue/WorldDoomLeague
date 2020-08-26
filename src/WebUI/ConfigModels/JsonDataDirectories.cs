using System.Configuration;

namespace WorldDoomLeague.WebUI.ConfigModels
{
    public sealed class JsonDataDirectories : ConfigurationSection
    {
        [ConfigurationProperty("jsonmatchdirectory",
             DefaultValue = "/odamex/wdl-json",
             IsRequired = true)]
        public string JsonMatchData { get; set; }

        [ConfigurationProperty("demorepository",
             DefaultValue = "/odamex/wdl-demos",
             IsRequired = true)]
        public string DemoRepository { get; set; }
    }
}