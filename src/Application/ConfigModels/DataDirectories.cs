using System.Configuration;
using WorldDoomLeague.Application.Common.Interfaces;

namespace WorldDoomLeague.Application.ConfigModels
{
    public sealed class DataDirectories
    {
        public const string Name = "DataDirectories";
        public string JsonMatchDirectory { get; set; }
        public string LogDirectory { get; set; }
        public string DemoRepository { get; set; }
        public string WadRepository { get; set; }
    }
}