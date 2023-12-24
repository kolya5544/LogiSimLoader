using System.Collections.Generic;

namespace LogiSimLoader.LogiSim
{
    [LSContent("circuit", false)]
    public class LSCircuit
    {
        [LSContent("a", false)]
        public List<LSAttribute>? attr = null;
        [LSContent("appear", false)]
        public LSAppearance? appear = null;
        [LSContent("wire", false)]
        public List<LSWire>? wires = null;
        [LSContent("comp", false)]
        public List<LSComponent>? components = null;
        [LSContent("name")]
        public string? name = null;
    }
}