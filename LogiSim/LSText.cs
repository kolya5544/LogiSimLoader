namespace LogiSimLoader.LogiSim
{
    [LSContent("text", false)]
    public class LSText
    {
        [LSContent("font-family")]
        public string? font_family;
        [LSContent("font-size")]
        public int? font_size;
        [LSContent("text-anchor")]
        public string? text_anchor;
        [LSContent("x")]
        public int? x;
        [LSContent("y")]
        public int? y;
        [LSContent("_", false)]
        public string? text;
    }
}