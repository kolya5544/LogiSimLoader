namespace LogiSimLoader.LogiSim
{
    [LSContent("a", false)]
    public class LSAttribute
    {
        [LSContent("name")]
        public string name;
        [LSContent("val")]
        public string? value;
        [LSContent("_", false)]
        public string? textValue;

        public string Value
        {
            get
            {
                if (value == null) return textValue;
                return value;
            }
            set
            {
                if (this.value != null) this.value = value;
                this.textValue = value;
            }
        }
    }
}