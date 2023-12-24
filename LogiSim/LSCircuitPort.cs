namespace LogiSimLoader.LogiSim
{
    [LSContent("circ-port", false)]
    public class LSCircuitPort
    {
        [LSContent("height")]
        public int? height;
        [LSContent("pin")]
        public string? pin;
        [LSContent("width")]
        public int? width;
        [LSContent("x")]
        public int? x;
        [LSContent("y")]
        public int? y;
    }
}