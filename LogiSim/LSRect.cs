namespace LogiSimLoader.LogiSim
{
    [LSContent("rect", false)]
    public class LSRect
    {
        [LSContent("fill")]
        public string? fill;
        [LSContent("height")]
        public int? height;
        [LSContent("stroke")]
        public string? stroke;
        [LSContent("stroke-width")]
        public int? stroke_width;
        [LSContent("width")]
        public int? width;
        [LSContent("x")]
        public int? x;
        [LSContent("y")]
        public int? y;
    }
}