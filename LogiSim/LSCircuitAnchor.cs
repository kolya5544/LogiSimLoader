namespace LogiSimLoader.LogiSim
{
    [LSContent("circ-anchor", false)]
    public class LSCircuitAnchor
    {
        [LSContent("facing")]
        public string? facing;
        [LSContent("height")]
        public int? height;
        [LSContent("width")]
        public int? width;
        [LSContent("x")]
        public int? x;
        [LSContent("y")]
        public int? y;
    }
}