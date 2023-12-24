using System.Collections.Generic;
using System.Numerics;

namespace LogiSimLoader.LogiSim
{
    [LSContent("comp", false)]
    public class LSComponent
    {
        [LSContent("a", false)]
        public List<LSAttribute>? attr = null;
        [LSContent("lib")]
        public string? lib = null;
        [LSContent("loc")]
        public Vector2? loc = null;
        [LSContent("name")]
        public string? name = null;
    }
}