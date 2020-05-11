﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentFontAwesome.MvcCore
{
    [HtmlTargetElement("a", Attributes = "asp-icon")]
    [HtmlTargetElement("button", Attributes = "asp-icon")]
    [HtmlTargetElement("span", Attributes = "asp-icon")]
    public class FontAwesomeIconAttributeTagHelper : TagHelper
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

            if(FontAwesomeIcon == null)
            {
                icon = new FontAwesomeIcon(FontAwesomeIconName, FontAwesomeRendering);
                var curClasses = output.Attributes.FirstOrDefault(w => w.Name == "class")?.Value?.ToString();
                if(curClasses?.Contains("btn-lg") == true)
                {
                    icon = icon.Size(Size.Large);
                }
                else if(curClasses?.Contains("btn-sm") == true)
                {
                    icon = icon.Size(Size.Small);
                }
            }
            else
            {
                icon = FontAwesomeIcon;
            }
            var tag = icon.GetTag()+" ";
            output.PreContent.SetHtmlContent(tag);
        }
    }

}