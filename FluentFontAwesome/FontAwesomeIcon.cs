using System;
using System.Collections.Generic;

namespace FluentFontAwesome
{
    public struct FontAwesomeIcon
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public bool FixedWidth { get; set; }
        public bool Bordered { get; set; }
        public Pull Pull { get; set; }
        public Animation Animation { get; set; }
        public Rotation Rotation { get; set; }
        public Flip Flip { get; set; }
        
        public FontAwesomeIcon(string name)
        {
            Name = name;
            Size = Size.Normal;
            FixedWidth = false;
            Bordered = false;
            Pull = Pull.None;
            Animation = Animation.None;
            Rotation = Rotation.None;
            Flip = Flip.None;
        }

        public string GetTag(FontAwesomeTagSettings tagSettings = null)
        {
            tagSettings = tagSettings ?? FontAwesomeTagSettings.Default;
            var tagName = tagSettings.TagName;
            var quote = tagSettings.Quote;
            var ariaHidden = tagSettings.AriaHidden ? $" aria-hidden={quote}true{quote} " : "";

            return $"<{tagName} class={quote}{GetClass()}{quote}{ariaHidden}></{tagName}>";
        }

        public string GetClass() => string.Join(" ", GetClasses());

        public IEnumerable<string> GetClasses()
        {
            yield return "fa";
            yield return Name;

            switch (Size)
            {
                case Size.Normal:
                    break;
                case Size.Large:
                    yield return "fa-lg";
                    break;
                case Size.x2:
                    yield return "fa-2x";
                    break;
                case Size.x3:
                    yield return "fa-3x";
                    break;
                case Size.x4:
                    yield return "fa-4x";
                    break;
                case Size.x5:
                    yield return "fa-5x";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (FixedWidth)
                yield return "fa-fw";

            if (Bordered)
                yield return "fa-border";

            switch (Pull)
            {
                case Pull.None:
                    break;
                case Pull.Left:
                    yield return "fa-pull-left";
                    break;
                case Pull.Right:
                    yield return "fa-pull-right";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (Animation)
            {
                case Animation.None:
                    break;
                case Animation.Pulse:
                    yield return "fa-pulse";
                    break;
                case Animation.Spin:
                    yield return "fa-spin";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (Rotation)
            {
                case Rotation.None:
                    break;
                case Rotation.Rotate90:
                    yield return "fa-rotate-90";
                    break;
                case Rotation.Rotate180:
                    yield return "fa-rotate-180";
                    break;
                case Rotation.Rotate270:
                    yield return "fa-rotate-270";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (Flip)
            {
                case Flip.None:
                    break;
                case Flip.Horizontal:
                    yield return "fa-flip-horizontal";
                    break;
                case Flip.Vertical:
                    yield return "fa-flip-vertical";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}