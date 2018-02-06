namespace FluentFontAwesome
{
    public class FontAwesomeTagSettings
    {
        public static FontAwesomeTagSettings Default = new FontAwesomeTagSettings();   

        public string TagName { get; set; } = "i";
        public char Quote { get; set; } = '"';
        public bool AriaHidden { get; set; } = true;
    }
}