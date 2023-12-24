using System.Collections.Generic;

namespace LogiSimLoader.LogiSim
{
    [LSContent("options", false)]
    public class LSOptions
    {
        [LSContent("a", false)]
        public List<LSAttribute> attributes;
    }
}