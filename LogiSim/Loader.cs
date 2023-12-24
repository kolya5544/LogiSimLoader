using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiSimLoader.LogiSim
{
    public class Loader
    {
        public static LSFile LoadFromFile(string filename) => Load(File.ReadAllText(filename));

        public static LSFile Load(string content)
        {
            LSFile nF = new LSFile();
            nF.Parse(content);

            return nF;
        }
    }
}
