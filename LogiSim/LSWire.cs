using System.Numerics;

namespace LogiSimLoader.LogiSim
{
    [LSContent("wire", false)]
    public class LSWire
    {
        [LSContent("from")]
        public Vector2 from;
        [LSContent("to")]
        public Vector2 to;
    }
}