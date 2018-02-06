using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FluentFontAwesome.Mvc
{
    public static class FontAwesomeIconHtmlHelper
    {
        
        public static HtmlString Icon(this HtmlHelper html, string name)
            => html.Icon(b => b.Name(name));

        public static HtmlString Icon(this HtmlHelper html, FontAwesomeIconBuilder fontAwesomeIconBuilder)
            => html.Icon(fontAwesomeIconBuilder.Icon);

        public static HtmlString Icon(this HtmlHelper html, Action<FontAwesomeIconBuilder> fontAwesomeIconAdapter)
            => html.Icon(FontAwesomeIcons.FontAwesome, fontAwesomeIconAdapter);

        public static HtmlString Icon(this HtmlHelper html, FontAwesomeIcon fontAwesomeIcon)
            => html.Icon(fontAwesomeIcon, null);

        public static HtmlString Icon(this HtmlHelper html, FontAwesomeIcon fontAwesomeIcon, Action<FontAwesomeIconBuilder> fontAwesomeIconAdapter)
        {
            var icon = fontAwesomeIcon;

            if (fontAwesomeIconAdapter != null)
            {
                var fontAwesomeIconBuilder = new FontAwesomeIconBuilder(fontAwesomeIcon);
                fontAwesomeIconAdapter(fontAwesomeIconBuilder);
                icon = fontAwesomeIconBuilder.Icon;
            }
            
            return new HtmlString(icon.GetTag());
        }
    }
}