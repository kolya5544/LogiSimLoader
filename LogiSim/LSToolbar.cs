using System.Collections.Generic;

namespace LogiSimLoader.LogiSim
{
    [LSContent("toolbar", false)]
    public class LSToolbar
    {
        [LSContent("tool", false)]
        public List<LSTool> tool = new List<LSTool>();

        public int? sepIndex = 3;
    }
}