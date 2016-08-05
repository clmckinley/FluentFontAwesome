using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FluentFontAwesome.Mvc
{
    public static class FontAwesomeIconHtmlHelper
    {
        public static IHtmlString Icon(this HtmlHelper html, string name)
        {
            return html.Icon(new FontAwesomeIcon(name));
        }

        public static IHtmlString Icon(this HtmlHelper html, FontAwesomeIcon icon)
        {
            return new HtmlString(icon.GetTag());
        }
    }
}