using System.Collections.Generic;

namespace LogiSimLoader.LogiSim
{
    [LSContent("appear", false)]
    public class LSAppearance
    {
        [LSContent("rect", false)]
        public LSRect rectangle = new LSRect();
        [LSContent("text", false)]
        public List<LSText>? text = null;
        [LSContent("circ-port", false)]
        public List<LSCircuitPort> ports = new List<LSCircuitPort>();
        [LSContent("circ-anchor", false)]
        public LSCircuitAnchor anchor = new LSCircuitAnchor();
    }
}