using System.Collections.Generic;

namespace LogiSimLoader.LogiSim
{
    [LSContent("mapping", false)]
    public class LSMapping
    {
        [LSContent("tool", false)]
        public List<LSTool> tool = new List<LSTool>();
    }
}