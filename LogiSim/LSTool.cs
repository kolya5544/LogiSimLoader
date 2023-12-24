using System.Collections.Generic;

namespace LogiSimLoader.LogiSim
{
    [LSContent("tool", false)]
    public class LSTool
    {
        [LSContent("a", false)]
        public List<LSAttribute>? attr = null;
        [LSContent("name")]
        public string name = "Default Tool Name";
        [LSContent("map")]
        public string? map = null;
        [LSContent("lib")]
        public string? lib = null;
    }
}