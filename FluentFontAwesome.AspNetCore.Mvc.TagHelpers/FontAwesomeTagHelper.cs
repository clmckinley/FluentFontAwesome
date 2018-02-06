using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FluentFontAwesome.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement("fa")]
    [HtmlTargetElement("font-awesome")]
    public class FontAwesomeTagHelper : TagHelper
    {
        public FontAwesomeIconBuilder Icon { get; set; } = new FontAwesomeIconBuilder();
        
        public string Name { get; set; }

        public Size? Size { get; set; }
        public bool? FixedWidth { get; set; }
        public bool? Bordered { get; set; }
        public Pull? Pull { get; set; }
        public Animation? Animation { get; set; }
        public Rotation? Rotation { get; set; }
        public Flip? Flip { get; set; }

        public FontAwesomeTagSettings TagSettings { get; set; } = FontAwesomeTagSettings.Default;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = TagSettings.TagName;
            output.TagMode = TagMode.StartTagAndEndTag;

            var classes = new HashSet<string>();
            if (output.Attributes.TryGetAttribute("class", out var @class))
                foreach (var c in ((string) @class.Value).Split(' '))
                    classes.Add(c);

            var iconBuilder = new FontAwesomeIconBuilder(Icon ?? FontAwesomeIcons.FontAwesome);

            if (!string.IsNullOrEmpty(Name))
                iconBuilder.Name(Name);

            if (Size.HasValue)
                iconBuilder.Size(Size.Value);

            if (FixedWidth.HasValue)
                iconBuilder.FixedWidth(FixedWidth.Value);

            if (Bordered.HasValue)
                iconBuilder.Bordered(Bordered.Value);

            if (Pull.HasValue)
                iconBuilder.Pull(Pull.Value);

            if (Animation.HasValue)
                iconBuilder.Animate(Animation.Value);

            if (Rotation.HasValue)
                iconBuilder.Rotate(Rotation.Value);

            if (Flip.HasValue)
                iconBuilder.Flip(Flip.Value);

            classes.Add(iconBuilder.Icon.GetClass());
            output.Attributes.Add("class", string.Join(" ", classes));

            if (TagSettings.AriaHidden)
                output.Attributes.Add("aria-hidden", "true");
        }
    }
}
