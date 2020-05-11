using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace FluentFontAwesome.MvcCore
{
    [HtmlTargetElement("font-awesome-icon", Attributes = "asp-icon", TagStructure = TagStructure.WithoutEndTag)]
    public class FontAwesomeIconTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-icon")]
        public FontAwesomeIconsEnum FontAwesomeIconName { get; set; } = FontAwesomeIconsEnum.Info;
        [HtmlAttributeName("font-awesome-icon")]
        public FontAwesomeIcon? FontAwesomeIcon { get; set; }
        [HtmlAttributeName("font-awesome-rendering")]
        public Rendering? FontAwesomeRendering { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            FontAwesomeIcon icon;

            if (FontAwesomeIcon == null)
            {
                icon = new FontAwesomeIcon(FontAwesomeIconName, FontAwesomeRendering);
                if (icon.Size() == null)
                {
                    var curClasses = output.Attributes.FirstOrDefault(w => w.Name == "class")?.Value?.ToString();
                    if (curClasses?.Contains("btn-lg") == true)
                    {
                        icon = icon.Size(Size.Large);
                    }
                    else if (curClasses?.Contains("btn-sm") == true)
                    {
                        icon = icon.Size(Size.Small);
                    }
                }
                
            }
            else
            {
                icon = FontAwesomeIcon;
            }
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "i";
            output.Attributes.Add("class", icon.GetClass());
            icon.GetAttributes().ForEach(e => output.Attributes.Add(e.Key, e.Value));
        }
    }

}
