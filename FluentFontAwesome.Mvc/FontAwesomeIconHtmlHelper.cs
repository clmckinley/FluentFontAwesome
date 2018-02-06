using System;
using System.Web;
using System.Web.Mvc;

namespace FluentFontAwesome.Mvc
{
    public static class FontAwesomeIconHtmlHelper
    {   
        public static MvcHtmlString Icon(this HtmlHelper html, string name)
            => html.Icon(new FontAwesomeIconBuilder(name), FontAwesomeTagSettings.Default);

        public static MvcHtmlString Icon(this HtmlHelper html, string name, FontAwesomeTagSettings tagSettings)
            => html.Icon(new FontAwesomeIconBuilder(name), tagSettings);
        public static MvcHtmlString Icon(this HtmlHelper html, FontAwesomeIcon fontAwesomeIcon)
            => html.Icon(new FontAwesomeIconBuilder(fontAwesomeIcon), FontAwesomeTagSettings.Default);

        public static MvcHtmlString Icon(this HtmlHelper html, FontAwesomeIconBuilder fontAwesomeIconBuilder)
            => html.Icon(fontAwesomeIconBuilder, FontAwesomeTagSettings.Default);

        public static MvcHtmlString Icon(this HtmlHelper _, FontAwesomeIconBuilder fontAwesomeIconBuilder, FontAwesomeTagSettings tagSettings)
        {
            var icon = fontAwesomeIconBuilder.Icon;
            return new MvcHtmlString(icon.GetTag(tagSettings));
        }
    }
}