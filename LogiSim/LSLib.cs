using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiSimLoader.LogiSim
{
    [LSContent("lib", false)]
    public class LSLib
    {
        [LSContent("tool", false)]
        public LSTool tool;
        [LSContent("desc")]
        public string desc;
        [LSContent("name")]
        public string name;
    }
}
