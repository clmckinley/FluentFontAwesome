using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FluentFontAwesome
{
    public class FontAwesomeTagSettings
    {
        public string TagName { get; set; } = "i";
        public char Quote { get; set; } = '"';
        public bool AriaHidden { get; set; } = true;

        public static FontAwesomeTagSettings Default = new FontAwesomeTagSettings();
    }

    public class FontAwesomeIcon
    {
        private string _name;

        private Size _size = FluentFontAwesome.Size.Normal;
        private bool _fixedWidth = false;
        private bool _bordered = false;
        private Pull _pull = FluentFontAwesome.Pull.None;
        private Animation _animation = FluentFontAwesome.Animation.None;
        private Rotation _rotation = FluentFontAwesome.Rotation.None;
        private Flip _flip = FluentFontAwesome.Flip.None;

        public FontAwesomeIcon()
        {
        }

        public FontAwesomeIcon(string name)
        {
            Name(name);
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
            yield return "fa-" + _name;

            switch (_size)
            {
                case FluentFontAwesome.Size.Normal:
                    break;
                case FluentFontAwesome.Size.Large:
                    yield return "fa-lg";
                    break;
                case FluentFontAwesome.Size.x2:
                    yield return "fa-2x";
                    break;
                case FluentFontAwesome.Size.x3:
                    yield return "fa-3x";
                    break;
                case FluentFontAwesome.Size.x4:
                    yield return "fa-4x";
                    break;
                case FluentFontAwesome.Size.x5:
                    yield return "fa-5x";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_fixedWidth)
                yield return "fa-fw";

            if (_bordered)
                yield return "fa-border";

            switch (_pull)
            {
                case FluentFontAwesome.Pull.None:
                    break;
                case FluentFontAwesome.Pull.Left:
                    yield return "fa-pull-left";
                    break;
                case FluentFontAwesome.Pull.Right:
                    yield return "fa-pull-right";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (_animation)
            {
                case FluentFontAwesome.Animation.None:
                    break;
                case FluentFontAwesome.Animation.Pulse:
                    yield return "fa-pulse";
                    break;
                case FluentFontAwesome.Animation.Spin:
                    yield return "fa-spin";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (_rotation)
            {
                case FluentFontAwesome.Rotation.None:
                    break;
                case FluentFontAwesome.Rotation.Rotate90:
                    yield return "fa-rotate-90";
                    break;
                case FluentFontAwesome.Rotation.Rotate180:
                    yield return "fa-rotate-180";
                    break;
                case FluentFontAwesome.Rotation.Rotate270:
                    yield return "fa-rotate-270";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (_flip)
            {
                case FluentFontAwesome.Flip.None:
                    break;
                case FluentFontAwesome.Flip.Horizontal:
                    yield return "fa-flip-horizontal";
                    break;
                case FluentFontAwesome.Flip.Vertical:
                    yield return "fa-flip-vertical";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region Fluent Methods

        //[fluent regex]   : private (\w+) (_(\w+)).*;
        //[fluent replace] : public $1 $3() => $2;\npublic FontAwesomeIcon $3($1 $3)\n{\n\t$2 = $3;\n\treturn this;\n}\n

        public string Name() => _name;

        public FontAwesomeIcon Name(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (name.StartsWith("fa-"))
                name = name.Substring(3);

            _name = name;
            return this;
        }

        public Size Size() => _size;

        public FontAwesomeIcon Size(Size size)
        {
            _size = size;
            return this;
        }

        public bool FixedWidth() => _fixedWidth;

        public FontAwesomeIcon FixedWidth(bool fixedWidth)
        {
            _fixedWidth = fixedWidth;
            return this;
        }

        public bool Bordered() => _bordered;

        public FontAwesomeIcon Bordered(bool bordered)
        {
            _bordered = bordered;
            return this;
        }

        public Pull Pull() => _pull;

        public FontAwesomeIcon Pull(Pull pull)
        {
            _pull = pull;
            return this;
        }

        public Animation Animation() => _animation;

        public FontAwesomeIcon Animation(Animation animation)
        {
            _animation = animation;
            return this;
        }

        public Rotation Rotation() => _rotation;

        public FontAwesomeIcon Rotation(Rotation rotation)
        {
            _rotation = rotation;
            return this;
        }


        public FluentFontAwesome.Flip Flip() => _flip;

        public FontAwesomeIcon Flip(FluentFontAwesome.Flip flip)
        {
            _flip = flip;
            return this;
        }

        #endregion

        #region Static Icons

        // http://fontawesome.io/icons/
        // [jQuery] : copy(_.map(_.uniq(_.map($(".fontawesome-icon-list").find(".fa"), i => $(i).attr('class').substr(5))), c => { var n = c.replace(/-(.)/gi, (m, g) => g.toUpperCase()); return "public static FontAwesomeIcon " + n + ' => new FontAwesomeIcon("' + c.substr(1) + '");'}).join('\n'))
        // [version] : 4.6.3

        public static FontAwesomeIcon AmericanSignLanguageInterpreting
            => new FontAwesomeIcon("american-sign-language-interpreting");

        public static FontAwesomeIcon AslInterpreting => new FontAwesomeIcon("asl-interpreting");
        public static FontAwesomeIcon AssistiveListeningSystems => new FontAwesomeIcon("assistive-listening-systems");
        public static FontAwesomeIcon AudioDescription => new FontAwesomeIcon("audio-description");
        public static FontAwesomeIcon Blind => new FontAwesomeIcon("blind");
        public static FontAwesomeIcon Braille => new FontAwesomeIcon("braille");
        public static FontAwesomeIcon Deaf => new FontAwesomeIcon("deaf");
        public static FontAwesomeIcon Deafness => new FontAwesomeIcon("deafness");
        public static FontAwesomeIcon Envira => new FontAwesomeIcon("envira");
        public static FontAwesomeIcon Fa => new FontAwesomeIcon("fa");
        public static FontAwesomeIcon FirstOrder => new FontAwesomeIcon("first-order");
        public static FontAwesomeIcon FontAwesome => new FontAwesomeIcon("font-awesome");
        public static FontAwesomeIcon Gitlab => new FontAwesomeIcon("gitlab");
        public static FontAwesomeIcon Glide => new FontAwesomeIcon("glide");
        public static FontAwesomeIcon GlideG => new FontAwesomeIcon("glide-g");
        public static FontAwesomeIcon GooglePlusCircle => new FontAwesomeIcon("google-plus-circle");
        public static FontAwesomeIcon GooglePlusOfficial => new FontAwesomeIcon("google-plus-official");
        public static FontAwesomeIcon HardOfHearing => new FontAwesomeIcon("hard-of-hearing");
        public static FontAwesomeIcon Instagram => new FontAwesomeIcon("instagram");
        public static FontAwesomeIcon LowVision => new FontAwesomeIcon("low-vision");
        public static FontAwesomeIcon PiedPiper => new FontAwesomeIcon("pied-piper");
        public static FontAwesomeIcon QuestionCircleO => new FontAwesomeIcon("question-circle-o");
        public static FontAwesomeIcon SignLanguage => new FontAwesomeIcon("sign-language");
        public static FontAwesomeIcon Signing => new FontAwesomeIcon("signing");
        public static FontAwesomeIcon Snapchat => new FontAwesomeIcon("snapchat");
        public static FontAwesomeIcon SnapchatGhost => new FontAwesomeIcon("snapchat-ghost");
        public static FontAwesomeIcon SnapchatSquare => new FontAwesomeIcon("snapchat-square");
        public static FontAwesomeIcon Themeisle => new FontAwesomeIcon("themeisle");
        public static FontAwesomeIcon UniversalAccess => new FontAwesomeIcon("universal-access");
        public static FontAwesomeIcon Viadeo => new FontAwesomeIcon("viadeo");
        public static FontAwesomeIcon ViadeoSquare => new FontAwesomeIcon("viadeo-square");
        public static FontAwesomeIcon VolumeControlPhone => new FontAwesomeIcon("volume-control-phone");
        public static FontAwesomeIcon WheelchairAlt => new FontAwesomeIcon("wheelchair-alt");
        public static FontAwesomeIcon Wpbeginner => new FontAwesomeIcon("wpbeginner");
        public static FontAwesomeIcon Wpforms => new FontAwesomeIcon("wpforms");
        public static FontAwesomeIcon Yoast => new FontAwesomeIcon("yoast");
        public static FontAwesomeIcon Adjust => new FontAwesomeIcon("adjust");
        public static FontAwesomeIcon Anchor => new FontAwesomeIcon("anchor");
        public static FontAwesomeIcon Archive => new FontAwesomeIcon("archive");
        public static FontAwesomeIcon AreaChart => new FontAwesomeIcon("area-chart");
        public static FontAwesomeIcon Arrows => new FontAwesomeIcon("arrows");
        public static FontAwesomeIcon ArrowsH => new FontAwesomeIcon("arrows-h");
        public static FontAwesomeIcon ArrowsV => new FontAwesomeIcon("arrows-v");
        public static FontAwesomeIcon Asterisk => new FontAwesomeIcon("asterisk");
        public static FontAwesomeIcon At => new FontAwesomeIcon("at");
        public static FontAwesomeIcon Automobile => new FontAwesomeIcon("automobile");
        public static FontAwesomeIcon BalanceScale => new FontAwesomeIcon("balance-scale");
        public static FontAwesomeIcon Ban => new FontAwesomeIcon("ban");
        public static FontAwesomeIcon Bank => new FontAwesomeIcon("bank");
        public static FontAwesomeIcon BarChart => new FontAwesomeIcon("bar-chart");
        public static FontAwesomeIcon BarChartO => new FontAwesomeIcon("bar-chart-o");
        public static FontAwesomeIcon Barcode => new FontAwesomeIcon("barcode");
        public static FontAwesomeIcon Bars => new FontAwesomeIcon("bars");
        public static FontAwesomeIcon Battery0 => new FontAwesomeIcon("battery-0");
        public static FontAwesomeIcon Battery1 => new FontAwesomeIcon("battery-1");
        public static FontAwesomeIcon Battery2 => new FontAwesomeIcon("battery-2");
        public static FontAwesomeIcon Battery3 => new FontAwesomeIcon("battery-3");
        public static FontAwesomeIcon Battery4 => new FontAwesomeIcon("battery-4");
        public static FontAwesomeIcon BatteryEmpty => new FontAwesomeIcon("battery-empty");
        public static FontAwesomeIcon BatteryFull => new FontAwesomeIcon("battery-full");
        public static FontAwesomeIcon BatteryHalf => new FontAwesomeIcon("battery-half");
        public static FontAwesomeIcon BatteryQuarter => new FontAwesomeIcon("battery-quarter");
        public static FontAwesomeIcon BatteryThreeQuarters => new FontAwesomeIcon("battery-three-quarters");
        public static FontAwesomeIcon Bed => new FontAwesomeIcon("bed");
        public static FontAwesomeIcon Beer => new FontAwesomeIcon("beer");
        public static FontAwesomeIcon Bell => new FontAwesomeIcon("bell");
        public static FontAwesomeIcon BellO => new FontAwesomeIcon("bell-o");
        public static FontAwesomeIcon BellSlash => new FontAwesomeIcon("bell-slash");
        public static FontAwesomeIcon BellSlashO => new FontAwesomeIcon("bell-slash-o");
        public static FontAwesomeIcon Bicycle => new FontAwesomeIcon("bicycle");
        public static FontAwesomeIcon Binoculars => new FontAwesomeIcon("binoculars");
        public static FontAwesomeIcon BirthdayCake => new FontAwesomeIcon("birthday-cake");
        public static FontAwesomeIcon Bluetooth => new FontAwesomeIcon("bluetooth");
        public static FontAwesomeIcon BluetoothB => new FontAwesomeIcon("bluetooth-b");
        public static FontAwesomeIcon Bolt => new FontAwesomeIcon("bolt");
        public static FontAwesomeIcon Bomb => new FontAwesomeIcon("bomb");
        public static FontAwesomeIcon Book => new FontAwesomeIcon("book");
        public static FontAwesomeIcon Bookmark => new FontAwesomeIcon("bookmark");
        public static FontAwesomeIcon BookmarkO => new FontAwesomeIcon("bookmark-o");
        public static FontAwesomeIcon Briefcase => new FontAwesomeIcon("briefcase");
        public static FontAwesomeIcon Bug => new FontAwesomeIcon("bug");
        public static FontAwesomeIcon Building => new FontAwesomeIcon("building");
        public static FontAwesomeIcon BuildingO => new FontAwesomeIcon("building-o");
        public static FontAwesomeIcon Bullhorn => new FontAwesomeIcon("bullhorn");
        public static FontAwesomeIcon Bullseye => new FontAwesomeIcon("bullseye");
        public static FontAwesomeIcon Bus => new FontAwesomeIcon("bus");
        public static FontAwesomeIcon Cab => new FontAwesomeIcon("cab");
        public static FontAwesomeIcon Calculator => new FontAwesomeIcon("calculator");
        public static FontAwesomeIcon Calendar => new FontAwesomeIcon("calendar");
        public static FontAwesomeIcon CalendarCheckO => new FontAwesomeIcon("calendar-check-o");
        public static FontAwesomeIcon CalendarMinusO => new FontAwesomeIcon("calendar-minus-o");
        public static FontAwesomeIcon CalendarO => new FontAwesomeIcon("calendar-o");
        public static FontAwesomeIcon CalendarPlusO => new FontAwesomeIcon("calendar-plus-o");
        public static FontAwesomeIcon CalendarTimesO => new FontAwesomeIcon("calendar-times-o");
        public static FontAwesomeIcon Camera => new FontAwesomeIcon("camera");
        public static FontAwesomeIcon CameraRetro => new FontAwesomeIcon("camera-retro");
        public static FontAwesomeIcon Car => new FontAwesomeIcon("car");
        public static FontAwesomeIcon CaretSquareODown => new FontAwesomeIcon("caret-square-o-down");
        public static FontAwesomeIcon CaretSquareOLeft => new FontAwesomeIcon("caret-square-o-left");
        public static FontAwesomeIcon CaretSquareORight => new FontAwesomeIcon("caret-square-o-right");
        public static FontAwesomeIcon CaretSquareOUp => new FontAwesomeIcon("caret-square-o-up");
        public static FontAwesomeIcon CartArrowDown => new FontAwesomeIcon("cart-arrow-down");
        public static FontAwesomeIcon CartPlus => new FontAwesomeIcon("cart-plus");
        public static FontAwesomeIcon Cc => new FontAwesomeIcon("cc");
        public static FontAwesomeIcon Certificate => new FontAwesomeIcon("certificate");
        public static FontAwesomeIcon Check => new FontAwesomeIcon("check");
        public static FontAwesomeIcon CheckCircle => new FontAwesomeIcon("check-circle");
        public static FontAwesomeIcon CheckCircleO => new FontAwesomeIcon("check-circle-o");
        public static FontAwesomeIcon CheckSquare => new FontAwesomeIcon("check-square");
        public static FontAwesomeIcon CheckSquareO => new FontAwesomeIcon("check-square-o");
        public static FontAwesomeIcon Child => new FontAwesomeIcon("child");
        public static FontAwesomeIcon Circle => new FontAwesomeIcon("circle");
        public static FontAwesomeIcon CircleO => new FontAwesomeIcon("circle-o");
        public static FontAwesomeIcon CircleONotch => new FontAwesomeIcon("circle-o-notch");
        public static FontAwesomeIcon CircleThin => new FontAwesomeIcon("circle-thin");
        public static FontAwesomeIcon ClockO => new FontAwesomeIcon("clock-o");
        public static FontAwesomeIcon Clone => new FontAwesomeIcon("clone");
        public static FontAwesomeIcon Close => new FontAwesomeIcon("close");
        public static FontAwesomeIcon Cloud => new FontAwesomeIcon("cloud");
        public static FontAwesomeIcon CloudDownload => new FontAwesomeIcon("cloud-download");
        public static FontAwesomeIcon CloudUpload => new FontAwesomeIcon("cloud-upload");
        public static FontAwesomeIcon Code => new FontAwesomeIcon("code");
        public static FontAwesomeIcon CodeFork => new FontAwesomeIcon("code-fork");
        public static FontAwesomeIcon Coffee => new FontAwesomeIcon("coffee");
        public static FontAwesomeIcon Cog => new FontAwesomeIcon("cog");
        public static FontAwesomeIcon Cogs => new FontAwesomeIcon("cogs");
        public static FontAwesomeIcon Comment => new FontAwesomeIcon("comment");
        public static FontAwesomeIcon CommentO => new FontAwesomeIcon("comment-o");
        public static FontAwesomeIcon Commenting => new FontAwesomeIcon("commenting");
        public static FontAwesomeIcon CommentingO => new FontAwesomeIcon("commenting-o");
        public static FontAwesomeIcon Comments => new FontAwesomeIcon("comments");
        public static FontAwesomeIcon CommentsO => new FontAwesomeIcon("comments-o");
        public static FontAwesomeIcon Compass => new FontAwesomeIcon("compass");
        public static FontAwesomeIcon Copyright => new FontAwesomeIcon("copyright");
        public static FontAwesomeIcon CreativeCommons => new FontAwesomeIcon("creative-commons");
        public static FontAwesomeIcon CreditCard => new FontAwesomeIcon("credit-card");
        public static FontAwesomeIcon CreditCardAlt => new FontAwesomeIcon("credit-card-alt");
        public static FontAwesomeIcon Crop => new FontAwesomeIcon("crop");
        public static FontAwesomeIcon Crosshairs => new FontAwesomeIcon("crosshairs");
        public static FontAwesomeIcon Cube => new FontAwesomeIcon("cube");
        public static FontAwesomeIcon Cubes => new FontAwesomeIcon("cubes");
        public static FontAwesomeIcon Cutlery => new FontAwesomeIcon("cutlery");
        public static FontAwesomeIcon Dashboard => new FontAwesomeIcon("dashboard");
        public static FontAwesomeIcon Database => new FontAwesomeIcon("database");
        public static FontAwesomeIcon Desktop => new FontAwesomeIcon("desktop");
        public static FontAwesomeIcon Diamond => new FontAwesomeIcon("diamond");
        public static FontAwesomeIcon DotCircleO => new FontAwesomeIcon("dot-circle-o");
        public static FontAwesomeIcon Download => new FontAwesomeIcon("download");
        public static FontAwesomeIcon Edit => new FontAwesomeIcon("edit");
        public static FontAwesomeIcon EllipsisH => new FontAwesomeIcon("ellipsis-h");
        public static FontAwesomeIcon EllipsisV => new FontAwesomeIcon("ellipsis-v");
        public static FontAwesomeIcon Envelope => new FontAwesomeIcon("envelope");
        public static FontAwesomeIcon EnvelopeO => new FontAwesomeIcon("envelope-o");
        public static FontAwesomeIcon EnvelopeSquare => new FontAwesomeIcon("envelope-square");
        public static FontAwesomeIcon Eraser => new FontAwesomeIcon("eraser");
        public static FontAwesomeIcon Exchange => new FontAwesomeIcon("exchange");
        public static FontAwesomeIcon Exclamation => new FontAwesomeIcon("exclamation");
        public static FontAwesomeIcon ExclamationCircle => new FontAwesomeIcon("exclamation-circle");
        public static FontAwesomeIcon ExclamationTriangle => new FontAwesomeIcon("exclamation-triangle");
        public static FontAwesomeIcon ExternalLink => new FontAwesomeIcon("external-link");
        public static FontAwesomeIcon ExternalLinkSquare => new FontAwesomeIcon("external-link-square");
        public static FontAwesomeIcon Eye => new FontAwesomeIcon("eye");
        public static FontAwesomeIcon EyeSlash => new FontAwesomeIcon("eye-slash");
        public static FontAwesomeIcon Eyedropper => new FontAwesomeIcon("eyedropper");
        public static FontAwesomeIcon Fax => new FontAwesomeIcon("fax");
        public static FontAwesomeIcon Feed => new FontAwesomeIcon("feed");
        public static FontAwesomeIcon Female => new FontAwesomeIcon("female");
        public static FontAwesomeIcon FighterJet => new FontAwesomeIcon("fighter-jet");
        public static FontAwesomeIcon FileArchiveO => new FontAwesomeIcon("file-archive-o");
        public static FontAwesomeIcon FileAudioO => new FontAwesomeIcon("file-audio-o");
        public static FontAwesomeIcon FileCodeO => new FontAwesomeIcon("file-code-o");
        public static FontAwesomeIcon FileExcelO => new FontAwesomeIcon("file-excel-o");
        public static FontAwesomeIcon FileImageO => new FontAwesomeIcon("file-image-o");
        public static FontAwesomeIcon FileMovieO => new FontAwesomeIcon("file-movie-o");
        public static FontAwesomeIcon FilePdfO => new FontAwesomeIcon("file-pdf-o");
        public static FontAwesomeIcon FilePhotoO => new FontAwesomeIcon("file-photo-o");
        public static FontAwesomeIcon FilePictureO => new FontAwesomeIcon("file-picture-o");
        public static FontAwesomeIcon FilePowerpointO => new FontAwesomeIcon("file-powerpoint-o");
        public static FontAwesomeIcon FileSoundO => new FontAwesomeIcon("file-sound-o");
        public static FontAwesomeIcon FileVideoO => new FontAwesomeIcon("file-video-o");
        public static FontAwesomeIcon FileWordO => new FontAwesomeIcon("file-word-o");
        public static FontAwesomeIcon FileZipO => new FontAwesomeIcon("file-zip-o");
        public static FontAwesomeIcon Film => new FontAwesomeIcon("film");
        public static FontAwesomeIcon Filter => new FontAwesomeIcon("filter");
        public static FontAwesomeIcon Fire => new FontAwesomeIcon("fire");
        public static FontAwesomeIcon FireExtinguisher => new FontAwesomeIcon("fire-extinguisher");
        public static FontAwesomeIcon Flag => new FontAwesomeIcon("flag");
        public static FontAwesomeIcon FlagCheckered => new FontAwesomeIcon("flag-checkered");
        public static FontAwesomeIcon FlagO => new FontAwesomeIcon("flag-o");
        public static FontAwesomeIcon Flash => new FontAwesomeIcon("flash");
        public static FontAwesomeIcon Flask => new FontAwesomeIcon("flask");
        public static FontAwesomeIcon Folder => new FontAwesomeIcon("folder");
        public static FontAwesomeIcon FolderO => new FontAwesomeIcon("folder-o");
        public static FontAwesomeIcon FolderOpen => new FontAwesomeIcon("folder-open");
        public static FontAwesomeIcon FolderOpenO => new FontAwesomeIcon("folder-open-o");
        public static FontAwesomeIcon FrownO => new FontAwesomeIcon("frown-o");
        public static FontAwesomeIcon FutbolO => new FontAwesomeIcon("futbol-o");
        public static FontAwesomeIcon Gamepad => new FontAwesomeIcon("gamepad");
        public static FontAwesomeIcon Gavel => new FontAwesomeIcon("gavel");
        public static FontAwesomeIcon Gear => new FontAwesomeIcon("gear");
        public static FontAwesomeIcon Gears => new FontAwesomeIcon("gears");
        public static FontAwesomeIcon Gift => new FontAwesomeIcon("gift");
        public static FontAwesomeIcon Glass => new FontAwesomeIcon("glass");
        public static FontAwesomeIcon Globe => new FontAwesomeIcon("globe");
        public static FontAwesomeIcon GraduationCap => new FontAwesomeIcon("graduation-cap");
        public static FontAwesomeIcon Group => new FontAwesomeIcon("group");
        public static FontAwesomeIcon HandGrabO => new FontAwesomeIcon("hand-grab-o");
        public static FontAwesomeIcon HandLizardO => new FontAwesomeIcon("hand-lizard-o");
        public static FontAwesomeIcon HandPaperO => new FontAwesomeIcon("hand-paper-o");
        public static FontAwesomeIcon HandPeaceO => new FontAwesomeIcon("hand-peace-o");
        public static FontAwesomeIcon HandPointerO => new FontAwesomeIcon("hand-pointer-o");
        public static FontAwesomeIcon HandRockO => new FontAwesomeIcon("hand-rock-o");
        public static FontAwesomeIcon HandScissorsO => new FontAwesomeIcon("hand-scissors-o");
        public static FontAwesomeIcon HandSpockO => new FontAwesomeIcon("hand-spock-o");
        public static FontAwesomeIcon HandStopO => new FontAwesomeIcon("hand-stop-o");
        public static FontAwesomeIcon Hashtag => new FontAwesomeIcon("hashtag");
        public static FontAwesomeIcon HddO => new FontAwesomeIcon("hdd-o");
        public static FontAwesomeIcon Headphones => new FontAwesomeIcon("headphones");
        public static FontAwesomeIcon Heart => new FontAwesomeIcon("heart");
        public static FontAwesomeIcon HeartO => new FontAwesomeIcon("heart-o");
        public static FontAwesomeIcon Heartbeat => new FontAwesomeIcon("heartbeat");
        public static FontAwesomeIcon History => new FontAwesomeIcon("history");
        public static FontAwesomeIcon Home => new FontAwesomeIcon("home");
        public static FontAwesomeIcon Hotel => new FontAwesomeIcon("hotel");
        public static FontAwesomeIcon Hourglass => new FontAwesomeIcon("hourglass");
        public static FontAwesomeIcon Hourglass1 => new FontAwesomeIcon("hourglass-1");
        public static FontAwesomeIcon Hourglass2 => new FontAwesomeIcon("hourglass-2");
        public static FontAwesomeIcon Hourglass3 => new FontAwesomeIcon("hourglass-3");
        public static FontAwesomeIcon HourglassEnd => new FontAwesomeIcon("hourglass-end");
        public static FontAwesomeIcon HourglassHalf => new FontAwesomeIcon("hourglass-half");
        public static FontAwesomeIcon HourglassO => new FontAwesomeIcon("hourglass-o");
        public static FontAwesomeIcon HourglassStart => new FontAwesomeIcon("hourglass-start");
        public static FontAwesomeIcon ICursor => new FontAwesomeIcon("i-cursor");
        public static FontAwesomeIcon Image => new FontAwesomeIcon("image");
        public static FontAwesomeIcon Inbox => new FontAwesomeIcon("inbox");
        public static FontAwesomeIcon Industry => new FontAwesomeIcon("industry");
        public static FontAwesomeIcon Info => new FontAwesomeIcon("info");
        public static FontAwesomeIcon InfoCircle => new FontAwesomeIcon("info-circle");
        public static FontAwesomeIcon Institution => new FontAwesomeIcon("institution");
        public static FontAwesomeIcon Key => new FontAwesomeIcon("key");
        public static FontAwesomeIcon KeyboardO => new FontAwesomeIcon("keyboard-o");
        public static FontAwesomeIcon Language => new FontAwesomeIcon("language");
        public static FontAwesomeIcon Laptop => new FontAwesomeIcon("laptop");
        public static FontAwesomeIcon Leaf => new FontAwesomeIcon("leaf");
        public static FontAwesomeIcon Legal => new FontAwesomeIcon("legal");
        public static FontAwesomeIcon LemonO => new FontAwesomeIcon("lemon-o");
        public static FontAwesomeIcon LevelDown => new FontAwesomeIcon("level-down");
        public static FontAwesomeIcon LevelUp => new FontAwesomeIcon("level-up");
        public static FontAwesomeIcon LifeBouy => new FontAwesomeIcon("life-bouy");
        public static FontAwesomeIcon LifeBuoy => new FontAwesomeIcon("life-buoy");
        public static FontAwesomeIcon LifeRing => new FontAwesomeIcon("life-ring");
        public static FontAwesomeIcon LifeSaver => new FontAwesomeIcon("life-saver");
        public static FontAwesomeIcon LightbulbO => new FontAwesomeIcon("lightbulb-o");
        public static FontAwesomeIcon LineChart => new FontAwesomeIcon("line-chart");
        public static FontAwesomeIcon LocationArrow => new FontAwesomeIcon("location-arrow");
        public static FontAwesomeIcon Lock => new FontAwesomeIcon("lock");
        public static FontAwesomeIcon Magic => new FontAwesomeIcon("magic");
        public static FontAwesomeIcon Magnet => new FontAwesomeIcon("magnet");
        public static FontAwesomeIcon MailForward => new FontAwesomeIcon("mail-forward");
        public static FontAwesomeIcon MailReply => new FontAwesomeIcon("mail-reply");
        public static FontAwesomeIcon MailReplyAll => new FontAwesomeIcon("mail-reply-all");
        public static FontAwesomeIcon Male => new FontAwesomeIcon("male");
        public static FontAwesomeIcon Map => new FontAwesomeIcon("map");
        public static FontAwesomeIcon MapMarker => new FontAwesomeIcon("map-marker");
        public static FontAwesomeIcon MapO => new FontAwesomeIcon("map-o");
        public static FontAwesomeIcon MapPin => new FontAwesomeIcon("map-pin");
        public static FontAwesomeIcon MapSigns => new FontAwesomeIcon("map-signs");
        public static FontAwesomeIcon MehO => new FontAwesomeIcon("meh-o");
        public static FontAwesomeIcon Microphone => new FontAwesomeIcon("microphone");
        public static FontAwesomeIcon MicrophoneSlash => new FontAwesomeIcon("microphone-slash");
        public static FontAwesomeIcon Minus => new FontAwesomeIcon("minus");
        public static FontAwesomeIcon MinusCircle => new FontAwesomeIcon("minus-circle");
        public static FontAwesomeIcon MinusSquare => new FontAwesomeIcon("minus-square");
        public static FontAwesomeIcon MinusSquareO => new FontAwesomeIcon("minus-square-o");
        public static FontAwesomeIcon Mobile => new FontAwesomeIcon("mobile");
        public static FontAwesomeIcon MobilePhone => new FontAwesomeIcon("mobile-phone");
        public static FontAwesomeIcon Money => new FontAwesomeIcon("money");
        public static FontAwesomeIcon MoonO => new FontAwesomeIcon("moon-o");
        public static FontAwesomeIcon MortarBoard => new FontAwesomeIcon("mortar-board");
        public static FontAwesomeIcon Motorcycle => new FontAwesomeIcon("motorcycle");
        public static FontAwesomeIcon MousePointer => new FontAwesomeIcon("mouse-pointer");
        public static FontAwesomeIcon Music => new FontAwesomeIcon("music");
        public static FontAwesomeIcon Navicon => new FontAwesomeIcon("navicon");
        public static FontAwesomeIcon NewspaperO => new FontAwesomeIcon("newspaper-o");
        public static FontAwesomeIcon ObjectGroup => new FontAwesomeIcon("object-group");
        public static FontAwesomeIcon ObjectUngroup => new FontAwesomeIcon("object-ungroup");
        public static FontAwesomeIcon PaintBrush => new FontAwesomeIcon("paint-brush");
        public static FontAwesomeIcon PaperPlane => new FontAwesomeIcon("paper-plane");
        public static FontAwesomeIcon PaperPlaneO => new FontAwesomeIcon("paper-plane-o");
        public static FontAwesomeIcon Paw => new FontAwesomeIcon("paw");
        public static FontAwesomeIcon Pencil => new FontAwesomeIcon("pencil");
        public static FontAwesomeIcon PencilSquare => new FontAwesomeIcon("pencil-square");
        public static FontAwesomeIcon PencilSquareO => new FontAwesomeIcon("pencil-square-o");
        public static FontAwesomeIcon Percent => new FontAwesomeIcon("percent");
        public static FontAwesomeIcon Phone => new FontAwesomeIcon("phone");
        public static FontAwesomeIcon PhoneSquare => new FontAwesomeIcon("phone-square");
        public static FontAwesomeIcon Photo => new FontAwesomeIcon("photo");
        public static FontAwesomeIcon PictureO => new FontAwesomeIcon("picture-o");
        public static FontAwesomeIcon PieChart => new FontAwesomeIcon("pie-chart");
        public static FontAwesomeIcon Plane => new FontAwesomeIcon("plane");
        public static FontAwesomeIcon Plug => new FontAwesomeIcon("plug");
        public static FontAwesomeIcon Plus => new FontAwesomeIcon("plus");
        public static FontAwesomeIcon PlusCircle => new FontAwesomeIcon("plus-circle");
        public static FontAwesomeIcon PlusSquare => new FontAwesomeIcon("plus-square");
        public static FontAwesomeIcon PlusSquareO => new FontAwesomeIcon("plus-square-o");
        public static FontAwesomeIcon PowerOff => new FontAwesomeIcon("power-off");
        public static FontAwesomeIcon Print => new FontAwesomeIcon("print");
        public static FontAwesomeIcon PuzzlePiece => new FontAwesomeIcon("puzzle-piece");
        public static FontAwesomeIcon Qrcode => new FontAwesomeIcon("qrcode");
        public static FontAwesomeIcon Question => new FontAwesomeIcon("question");
        public static FontAwesomeIcon QuestionCircle => new FontAwesomeIcon("question-circle");
        public static FontAwesomeIcon QuoteLeft => new FontAwesomeIcon("quote-left");
        public static FontAwesomeIcon QuoteRight => new FontAwesomeIcon("quote-right");
        public static FontAwesomeIcon Random => new FontAwesomeIcon("random");
        public static FontAwesomeIcon Recycle => new FontAwesomeIcon("recycle");
        public static FontAwesomeIcon Refresh => new FontAwesomeIcon("refresh");
        public static FontAwesomeIcon Registered => new FontAwesomeIcon("registered");
        public static FontAwesomeIcon Remove => new FontAwesomeIcon("remove");
        public static FontAwesomeIcon Reorder => new FontAwesomeIcon("reorder");
        public static FontAwesomeIcon Reply => new FontAwesomeIcon("reply");
        public static FontAwesomeIcon ReplyAll => new FontAwesomeIcon("reply-all");
        public static FontAwesomeIcon Retweet => new FontAwesomeIcon("retweet");
        public static FontAwesomeIcon Road => new FontAwesomeIcon("road");
        public static FontAwesomeIcon Rocket => new FontAwesomeIcon("rocket");
        public static FontAwesomeIcon Rss => new FontAwesomeIcon("rss");
        public static FontAwesomeIcon RssSquare => new FontAwesomeIcon("rss-square");
        public static FontAwesomeIcon Search => new FontAwesomeIcon("search");
        public static FontAwesomeIcon SearchMinus => new FontAwesomeIcon("search-minus");
        public static FontAwesomeIcon SearchPlus => new FontAwesomeIcon("search-plus");
        public static FontAwesomeIcon Send => new FontAwesomeIcon("send");
        public static FontAwesomeIcon SendO => new FontAwesomeIcon("send-o");
        public static FontAwesomeIcon Server => new FontAwesomeIcon("server");
        public static FontAwesomeIcon Share => new FontAwesomeIcon("share");
        public static FontAwesomeIcon ShareAlt => new FontAwesomeIcon("share-alt");
        public static FontAwesomeIcon ShareAltSquare => new FontAwesomeIcon("share-alt-square");
        public static FontAwesomeIcon ShareSquare => new FontAwesomeIcon("share-square");
        public static FontAwesomeIcon ShareSquareO => new FontAwesomeIcon("share-square-o");
        public static FontAwesomeIcon Shield => new FontAwesomeIcon("shield");
        public static FontAwesomeIcon Ship => new FontAwesomeIcon("ship");
        public static FontAwesomeIcon ShoppingBag => new FontAwesomeIcon("shopping-bag");
        public static FontAwesomeIcon ShoppingBasket => new FontAwesomeIcon("shopping-basket");
        public static FontAwesomeIcon ShoppingCart => new FontAwesomeIcon("shopping-cart");
        public static FontAwesomeIcon SignIn => new FontAwesomeIcon("sign-in");
        public static FontAwesomeIcon SignOut => new FontAwesomeIcon("sign-out");
        public static FontAwesomeIcon Signal => new FontAwesomeIcon("signal");
        public static FontAwesomeIcon Sitemap => new FontAwesomeIcon("sitemap");
        public static FontAwesomeIcon Sliders => new FontAwesomeIcon("sliders");
        public static FontAwesomeIcon SmileO => new FontAwesomeIcon("smile-o");
        public static FontAwesomeIcon SoccerBallO => new FontAwesomeIcon("soccer-ball-o");
        public static FontAwesomeIcon Sort => new FontAwesomeIcon("sort");
        public static FontAwesomeIcon SortAlphaAsc => new FontAwesomeIcon("sort-alpha-asc");
        public static FontAwesomeIcon SortAlphaDesc => new FontAwesomeIcon("sort-alpha-desc");
        public static FontAwesomeIcon SortAmountAsc => new FontAwesomeIcon("sort-amount-asc");
        public static FontAwesomeIcon SortAmountDesc => new FontAwesomeIcon("sort-amount-desc");
        public static FontAwesomeIcon SortAsc => new FontAwesomeIcon("sort-asc");
        public static FontAwesomeIcon SortDesc => new FontAwesomeIcon("sort-desc");
        public static FontAwesomeIcon SortDown => new FontAwesomeIcon("sort-down");
        public static FontAwesomeIcon SortNumericAsc => new FontAwesomeIcon("sort-numeric-asc");
        public static FontAwesomeIcon SortNumericDesc => new FontAwesomeIcon("sort-numeric-desc");
        public static FontAwesomeIcon SortUp => new FontAwesomeIcon("sort-up");
        public static FontAwesomeIcon SpaceShuttle => new FontAwesomeIcon("space-shuttle");
        public static FontAwesomeIcon Spinner => new FontAwesomeIcon("spinner");
        public static FontAwesomeIcon Spoon => new FontAwesomeIcon("spoon");
        public static FontAwesomeIcon Square => new FontAwesomeIcon("square");
        public static FontAwesomeIcon SquareO => new FontAwesomeIcon("square-o");
        public static FontAwesomeIcon Star => new FontAwesomeIcon("star");
        public static FontAwesomeIcon StarHalf => new FontAwesomeIcon("star-half");
        public static FontAwesomeIcon StarHalfEmpty => new FontAwesomeIcon("star-half-empty");
        public static FontAwesomeIcon StarHalfFull => new FontAwesomeIcon("star-half-full");
        public static FontAwesomeIcon StarHalfO => new FontAwesomeIcon("star-half-o");
        public static FontAwesomeIcon StarO => new FontAwesomeIcon("star-o");
        public static FontAwesomeIcon StickyNote => new FontAwesomeIcon("sticky-note");
        public static FontAwesomeIcon StickyNoteO => new FontAwesomeIcon("sticky-note-o");
        public static FontAwesomeIcon StreetView => new FontAwesomeIcon("street-view");
        public static FontAwesomeIcon Suitcase => new FontAwesomeIcon("suitcase");
        public static FontAwesomeIcon SunO => new FontAwesomeIcon("sun-o");
        public static FontAwesomeIcon Support => new FontAwesomeIcon("support");
        public static FontAwesomeIcon Tablet => new FontAwesomeIcon("tablet");
        public static FontAwesomeIcon Tachometer => new FontAwesomeIcon("tachometer");
        public static FontAwesomeIcon Tag => new FontAwesomeIcon("tag");
        public static FontAwesomeIcon Tags => new FontAwesomeIcon("tags");
        public static FontAwesomeIcon Tasks => new FontAwesomeIcon("tasks");
        public static FontAwesomeIcon Taxi => new FontAwesomeIcon("taxi");
        public static FontAwesomeIcon Television => new FontAwesomeIcon("television");
        public static FontAwesomeIcon Terminal => new FontAwesomeIcon("terminal");
        public static FontAwesomeIcon ThumbTack => new FontAwesomeIcon("thumb-tack");
        public static FontAwesomeIcon ThumbsDown => new FontAwesomeIcon("thumbs-down");
        public static FontAwesomeIcon ThumbsODown => new FontAwesomeIcon("thumbs-o-down");
        public static FontAwesomeIcon ThumbsOUp => new FontAwesomeIcon("thumbs-o-up");
        public static FontAwesomeIcon ThumbsUp => new FontAwesomeIcon("thumbs-up");
        public static FontAwesomeIcon Ticket => new FontAwesomeIcon("ticket");
        public static FontAwesomeIcon Times => new FontAwesomeIcon("times");
        public static FontAwesomeIcon TimesCircle => new FontAwesomeIcon("times-circle");
        public static FontAwesomeIcon TimesCircleO => new FontAwesomeIcon("times-circle-o");
        public static FontAwesomeIcon Tint => new FontAwesomeIcon("tint");
        public static FontAwesomeIcon ToggleDown => new FontAwesomeIcon("toggle-down");
        public static FontAwesomeIcon ToggleLeft => new FontAwesomeIcon("toggle-left");
        public static FontAwesomeIcon ToggleOff => new FontAwesomeIcon("toggle-off");
        public static FontAwesomeIcon ToggleOn => new FontAwesomeIcon("toggle-on");
        public static FontAwesomeIcon ToggleRight => new FontAwesomeIcon("toggle-right");
        public static FontAwesomeIcon ToggleUp => new FontAwesomeIcon("toggle-up");
        public static FontAwesomeIcon Trademark => new FontAwesomeIcon("trademark");
        public static FontAwesomeIcon Trash => new FontAwesomeIcon("trash");
        public static FontAwesomeIcon TrashO => new FontAwesomeIcon("trash-o");
        public static FontAwesomeIcon Tree => new FontAwesomeIcon("tree");
        public static FontAwesomeIcon Trophy => new FontAwesomeIcon("trophy");
        public static FontAwesomeIcon Truck => new FontAwesomeIcon("truck");
        public static FontAwesomeIcon Tty => new FontAwesomeIcon("tty");
        public static FontAwesomeIcon Tv => new FontAwesomeIcon("tv");
        public static FontAwesomeIcon Umbrella => new FontAwesomeIcon("umbrella");
        public static FontAwesomeIcon University => new FontAwesomeIcon("university");
        public static FontAwesomeIcon Unlock => new FontAwesomeIcon("unlock");
        public static FontAwesomeIcon UnlockAlt => new FontAwesomeIcon("unlock-alt");
        public static FontAwesomeIcon Unsorted => new FontAwesomeIcon("unsorted");
        public static FontAwesomeIcon Upload => new FontAwesomeIcon("upload");
        public static FontAwesomeIcon User => new FontAwesomeIcon("user");
        public static FontAwesomeIcon UserPlus => new FontAwesomeIcon("user-plus");
        public static FontAwesomeIcon UserSecret => new FontAwesomeIcon("user-secret");
        public static FontAwesomeIcon UserTimes => new FontAwesomeIcon("user-times");
        public static FontAwesomeIcon Users => new FontAwesomeIcon("users");
        public static FontAwesomeIcon VideoCamera => new FontAwesomeIcon("video-camera");
        public static FontAwesomeIcon VolumeDown => new FontAwesomeIcon("volume-down");
        public static FontAwesomeIcon VolumeOff => new FontAwesomeIcon("volume-off");
        public static FontAwesomeIcon VolumeUp => new FontAwesomeIcon("volume-up");
        public static FontAwesomeIcon Warning => new FontAwesomeIcon("warning");
        public static FontAwesomeIcon Wheelchair => new FontAwesomeIcon("wheelchair");
        public static FontAwesomeIcon Wifi => new FontAwesomeIcon("wifi");
        public static FontAwesomeIcon Wrench => new FontAwesomeIcon("wrench");
        public static FontAwesomeIcon HandODown => new FontAwesomeIcon("hand-o-down");
        public static FontAwesomeIcon HandOLeft => new FontAwesomeIcon("hand-o-left");
        public static FontAwesomeIcon HandORight => new FontAwesomeIcon("hand-o-right");
        public static FontAwesomeIcon HandOUp => new FontAwesomeIcon("hand-o-up");
        public static FontAwesomeIcon Ambulance => new FontAwesomeIcon("ambulance");
        public static FontAwesomeIcon Subway => new FontAwesomeIcon("subway");
        public static FontAwesomeIcon Train => new FontAwesomeIcon("train");
        public static FontAwesomeIcon Genderless => new FontAwesomeIcon("genderless");
        public static FontAwesomeIcon Intersex => new FontAwesomeIcon("intersex");
        public static FontAwesomeIcon Mars => new FontAwesomeIcon("mars");
        public static FontAwesomeIcon MarsDouble => new FontAwesomeIcon("mars-double");
        public static FontAwesomeIcon MarsStroke => new FontAwesomeIcon("mars-stroke");
        public static FontAwesomeIcon MarsStrokeH => new FontAwesomeIcon("mars-stroke-h");
        public static FontAwesomeIcon MarsStrokeV => new FontAwesomeIcon("mars-stroke-v");
        public static FontAwesomeIcon Mercury => new FontAwesomeIcon("mercury");
        public static FontAwesomeIcon Neuter => new FontAwesomeIcon("neuter");
        public static FontAwesomeIcon Transgender => new FontAwesomeIcon("transgender");
        public static FontAwesomeIcon TransgenderAlt => new FontAwesomeIcon("transgender-alt");
        public static FontAwesomeIcon Venus => new FontAwesomeIcon("venus");
        public static FontAwesomeIcon VenusDouble => new FontAwesomeIcon("venus-double");
        public static FontAwesomeIcon VenusMars => new FontAwesomeIcon("venus-mars");
        public static FontAwesomeIcon File => new FontAwesomeIcon("file");
        public static FontAwesomeIcon FileO => new FontAwesomeIcon("file-o");
        public static FontAwesomeIcon FileText => new FontAwesomeIcon("file-text");
        public static FontAwesomeIcon FileTextO => new FontAwesomeIcon("file-text-o");
        public static FontAwesomeIcon CcAmex => new FontAwesomeIcon("cc-amex");
        public static FontAwesomeIcon CcDinersClub => new FontAwesomeIcon("cc-diners-club");
        public static FontAwesomeIcon CcDiscover => new FontAwesomeIcon("cc-discover");
        public static FontAwesomeIcon CcJcb => new FontAwesomeIcon("cc-jcb");
        public static FontAwesomeIcon CcMastercard => new FontAwesomeIcon("cc-mastercard");
        public static FontAwesomeIcon CcPaypal => new FontAwesomeIcon("cc-paypal");
        public static FontAwesomeIcon CcStripe => new FontAwesomeIcon("cc-stripe");
        public static FontAwesomeIcon CcVisa => new FontAwesomeIcon("cc-visa");
        public static FontAwesomeIcon GoogleWallet => new FontAwesomeIcon("google-wallet");
        public static FontAwesomeIcon Paypal => new FontAwesomeIcon("paypal");
        public static FontAwesomeIcon Bitcoin => new FontAwesomeIcon("bitcoin");
        public static FontAwesomeIcon Btc => new FontAwesomeIcon("btc");
        public static FontAwesomeIcon Cny => new FontAwesomeIcon("cny");
        public static FontAwesomeIcon Dollar => new FontAwesomeIcon("dollar");
        public static FontAwesomeIcon Eur => new FontAwesomeIcon("eur");
        public static FontAwesomeIcon Euro => new FontAwesomeIcon("euro");
        public static FontAwesomeIcon Gbp => new FontAwesomeIcon("gbp");
        public static FontAwesomeIcon Gg => new FontAwesomeIcon("gg");
        public static FontAwesomeIcon GgCircle => new FontAwesomeIcon("gg-circle");
        public static FontAwesomeIcon Ils => new FontAwesomeIcon("ils");
        public static FontAwesomeIcon Inr => new FontAwesomeIcon("inr");
        public static FontAwesomeIcon Jpy => new FontAwesomeIcon("jpy");
        public static FontAwesomeIcon Krw => new FontAwesomeIcon("krw");
        public static FontAwesomeIcon Rmb => new FontAwesomeIcon("rmb");
        public static FontAwesomeIcon Rouble => new FontAwesomeIcon("rouble");
        public static FontAwesomeIcon Rub => new FontAwesomeIcon("rub");
        public static FontAwesomeIcon Ruble => new FontAwesomeIcon("ruble");
        public static FontAwesomeIcon Rupee => new FontAwesomeIcon("rupee");
        public static FontAwesomeIcon Shekel => new FontAwesomeIcon("shekel");
        public static FontAwesomeIcon Sheqel => new FontAwesomeIcon("sheqel");
        public static FontAwesomeIcon Try => new FontAwesomeIcon("try");
        public static FontAwesomeIcon TurkishLira => new FontAwesomeIcon("turkish-lira");
        public static FontAwesomeIcon Usd => new FontAwesomeIcon("usd");
        public static FontAwesomeIcon Won => new FontAwesomeIcon("won");
        public static FontAwesomeIcon Yen => new FontAwesomeIcon("yen");
        public static FontAwesomeIcon AlignCenter => new FontAwesomeIcon("align-center");
        public static FontAwesomeIcon AlignJustify => new FontAwesomeIcon("align-justify");
        public static FontAwesomeIcon AlignLeft => new FontAwesomeIcon("align-left");
        public static FontAwesomeIcon AlignRight => new FontAwesomeIcon("align-right");
        public static FontAwesomeIcon Bold => new FontAwesomeIcon("bold");
        public static FontAwesomeIcon Chain => new FontAwesomeIcon("chain");
        public static FontAwesomeIcon ChainBroken => new FontAwesomeIcon("chain-broken");
        public static FontAwesomeIcon Clipboard => new FontAwesomeIcon("clipboard");
        public static FontAwesomeIcon Columns => new FontAwesomeIcon("columns");
        public static FontAwesomeIcon Copy => new FontAwesomeIcon("copy");
        public static FontAwesomeIcon Cut => new FontAwesomeIcon("cut");
        public static FontAwesomeIcon Dedent => new FontAwesomeIcon("dedent");
        public static FontAwesomeIcon FilesO => new FontAwesomeIcon("files-o");
        public static FontAwesomeIcon FloppyO => new FontAwesomeIcon("floppy-o");
        public static FontAwesomeIcon Font => new FontAwesomeIcon("font");
        public static FontAwesomeIcon Header => new FontAwesomeIcon("header");
        public static FontAwesomeIcon Indent => new FontAwesomeIcon("indent");
        public static FontAwesomeIcon Italic => new FontAwesomeIcon("italic");
        public static FontAwesomeIcon Link => new FontAwesomeIcon("link");
        public static FontAwesomeIcon List => new FontAwesomeIcon("list");
        public static FontAwesomeIcon ListAlt => new FontAwesomeIcon("list-alt");
        public static FontAwesomeIcon ListOl => new FontAwesomeIcon("list-ol");
        public static FontAwesomeIcon ListUl => new FontAwesomeIcon("list-ul");
        public static FontAwesomeIcon Outdent => new FontAwesomeIcon("outdent");
        public static FontAwesomeIcon Paperclip => new FontAwesomeIcon("paperclip");
        public static FontAwesomeIcon Paragraph => new FontAwesomeIcon("paragraph");
        public static FontAwesomeIcon Paste => new FontAwesomeIcon("paste");
        public static FontAwesomeIcon Repeat => new FontAwesomeIcon("repeat");
        public static FontAwesomeIcon RotateLeft => new FontAwesomeIcon("rotate-left");
        public static FontAwesomeIcon RotateRight => new FontAwesomeIcon("rotate-right");
        public static FontAwesomeIcon Save => new FontAwesomeIcon("save");
        public static FontAwesomeIcon Scissors => new FontAwesomeIcon("scissors");
        public static FontAwesomeIcon Strikethrough => new FontAwesomeIcon("strikethrough");
        public static FontAwesomeIcon Subscript => new FontAwesomeIcon("subscript");
        public static FontAwesomeIcon Superscript => new FontAwesomeIcon("superscript");
        public static FontAwesomeIcon Table => new FontAwesomeIcon("table");
        public static FontAwesomeIcon TextHeight => new FontAwesomeIcon("text-height");
        public static FontAwesomeIcon TextWidth => new FontAwesomeIcon("text-width");
        public static FontAwesomeIcon Th => new FontAwesomeIcon("th");
        public static FontAwesomeIcon ThLarge => new FontAwesomeIcon("th-large");
        public static FontAwesomeIcon ThList => new FontAwesomeIcon("th-list");
        public static FontAwesomeIcon Underline => new FontAwesomeIcon("underline");
        public static FontAwesomeIcon Undo => new FontAwesomeIcon("undo");
        public static FontAwesomeIcon Unlink => new FontAwesomeIcon("unlink");
        public static FontAwesomeIcon AngleDoubleDown => new FontAwesomeIcon("angle-double-down");
        public static FontAwesomeIcon AngleDoubleLeft => new FontAwesomeIcon("angle-double-left");
        public static FontAwesomeIcon AngleDoubleRight => new FontAwesomeIcon("angle-double-right");
        public static FontAwesomeIcon AngleDoubleUp => new FontAwesomeIcon("angle-double-up");
        public static FontAwesomeIcon AngleDown => new FontAwesomeIcon("angle-down");
        public static FontAwesomeIcon AngleLeft => new FontAwesomeIcon("angle-left");
        public static FontAwesomeIcon AngleRight => new FontAwesomeIcon("angle-right");
        public static FontAwesomeIcon AngleUp => new FontAwesomeIcon("angle-up");
        public static FontAwesomeIcon ArrowCircleDown => new FontAwesomeIcon("arrow-circle-down");
        public static FontAwesomeIcon ArrowCircleLeft => new FontAwesomeIcon("arrow-circle-left");
        public static FontAwesomeIcon ArrowCircleODown => new FontAwesomeIcon("arrow-circle-o-down");
        public static FontAwesomeIcon ArrowCircleOLeft => new FontAwesomeIcon("arrow-circle-o-left");
        public static FontAwesomeIcon ArrowCircleORight => new FontAwesomeIcon("arrow-circle-o-right");
        public static FontAwesomeIcon ArrowCircleOUp => new FontAwesomeIcon("arrow-circle-o-up");
        public static FontAwesomeIcon ArrowCircleRight => new FontAwesomeIcon("arrow-circle-right");
        public static FontAwesomeIcon ArrowCircleUp => new FontAwesomeIcon("arrow-circle-up");
        public static FontAwesomeIcon ArrowDown => new FontAwesomeIcon("arrow-down");
        public static FontAwesomeIcon ArrowLeft => new FontAwesomeIcon("arrow-left");
        public static FontAwesomeIcon ArrowRight => new FontAwesomeIcon("arrow-right");
        public static FontAwesomeIcon ArrowUp => new FontAwesomeIcon("arrow-up");
        public static FontAwesomeIcon ArrowsAlt => new FontAwesomeIcon("arrows-alt");
        public static FontAwesomeIcon CaretDown => new FontAwesomeIcon("caret-down");
        public static FontAwesomeIcon CaretLeft => new FontAwesomeIcon("caret-left");
        public static FontAwesomeIcon CaretRight => new FontAwesomeIcon("caret-right");
        public static FontAwesomeIcon CaretUp => new FontAwesomeIcon("caret-up");
        public static FontAwesomeIcon ChevronCircleDown => new FontAwesomeIcon("chevron-circle-down");
        public static FontAwesomeIcon ChevronCircleLeft => new FontAwesomeIcon("chevron-circle-left");
        public static FontAwesomeIcon ChevronCircleRight => new FontAwesomeIcon("chevron-circle-right");
        public static FontAwesomeIcon ChevronCircleUp => new FontAwesomeIcon("chevron-circle-up");
        public static FontAwesomeIcon ChevronDown => new FontAwesomeIcon("chevron-down");
        public static FontAwesomeIcon ChevronLeft => new FontAwesomeIcon("chevron-left");
        public static FontAwesomeIcon ChevronRight => new FontAwesomeIcon("chevron-right");
        public static FontAwesomeIcon ChevronUp => new FontAwesomeIcon("chevron-up");
        public static FontAwesomeIcon LongArrowDown => new FontAwesomeIcon("long-arrow-down");
        public static FontAwesomeIcon LongArrowLeft => new FontAwesomeIcon("long-arrow-left");
        public static FontAwesomeIcon LongArrowRight => new FontAwesomeIcon("long-arrow-right");
        public static FontAwesomeIcon LongArrowUp => new FontAwesomeIcon("long-arrow-up");
        public static FontAwesomeIcon Backward => new FontAwesomeIcon("backward");
        public static FontAwesomeIcon Compress => new FontAwesomeIcon("compress");
        public static FontAwesomeIcon Eject => new FontAwesomeIcon("eject");
        public static FontAwesomeIcon Expand => new FontAwesomeIcon("expand");
        public static FontAwesomeIcon FastBackward => new FontAwesomeIcon("fast-backward");
        public static FontAwesomeIcon FastForward => new FontAwesomeIcon("fast-forward");
        public static FontAwesomeIcon Forward => new FontAwesomeIcon("forward");
        public static FontAwesomeIcon Pause => new FontAwesomeIcon("pause");
        public static FontAwesomeIcon PauseCircle => new FontAwesomeIcon("pause-circle");
        public static FontAwesomeIcon PauseCircleO => new FontAwesomeIcon("pause-circle-o");
        public static FontAwesomeIcon Play => new FontAwesomeIcon("play");
        public static FontAwesomeIcon PlayCircle => new FontAwesomeIcon("play-circle");
        public static FontAwesomeIcon PlayCircleO => new FontAwesomeIcon("play-circle-o");
        public static FontAwesomeIcon StepBackward => new FontAwesomeIcon("step-backward");
        public static FontAwesomeIcon StepForward => new FontAwesomeIcon("step-forward");
        public static FontAwesomeIcon Stop => new FontAwesomeIcon("stop");
        public static FontAwesomeIcon StopCircle => new FontAwesomeIcon("stop-circle");
        public static FontAwesomeIcon StopCircleO => new FontAwesomeIcon("stop-circle-o");
        public static FontAwesomeIcon YoutubePlay => new FontAwesomeIcon("youtube-play");
        public static FontAwesomeIcon _500px => new FontAwesomeIcon("500px");
        public static FontAwesomeIcon Adn => new FontAwesomeIcon("adn");
        public static FontAwesomeIcon Amazon => new FontAwesomeIcon("amazon");
        public static FontAwesomeIcon Android => new FontAwesomeIcon("android");
        public static FontAwesomeIcon Angellist => new FontAwesomeIcon("angellist");
        public static FontAwesomeIcon Apple => new FontAwesomeIcon("apple");
        public static FontAwesomeIcon Behance => new FontAwesomeIcon("behance");
        public static FontAwesomeIcon BehanceSquare => new FontAwesomeIcon("behance-square");
        public static FontAwesomeIcon Bitbucket => new FontAwesomeIcon("bitbucket");
        public static FontAwesomeIcon BitbucketSquare => new FontAwesomeIcon("bitbucket-square");
        public static FontAwesomeIcon BlackTie => new FontAwesomeIcon("black-tie");
        public static FontAwesomeIcon Buysellads => new FontAwesomeIcon("buysellads");
        public static FontAwesomeIcon Chrome => new FontAwesomeIcon("chrome");
        public static FontAwesomeIcon Codepen => new FontAwesomeIcon("codepen");
        public static FontAwesomeIcon Codiepie => new FontAwesomeIcon("codiepie");
        public static FontAwesomeIcon Connectdevelop => new FontAwesomeIcon("connectdevelop");
        public static FontAwesomeIcon Contao => new FontAwesomeIcon("contao");
        public static FontAwesomeIcon Css3 => new FontAwesomeIcon("css3");
        public static FontAwesomeIcon Dashcube => new FontAwesomeIcon("dashcube");
        public static FontAwesomeIcon Delicious => new FontAwesomeIcon("delicious");
        public static FontAwesomeIcon Deviantart => new FontAwesomeIcon("deviantart");
        public static FontAwesomeIcon Digg => new FontAwesomeIcon("digg");
        public static FontAwesomeIcon Dribbble => new FontAwesomeIcon("dribbble");
        public static FontAwesomeIcon Dropbox => new FontAwesomeIcon("dropbox");
        public static FontAwesomeIcon Drupal => new FontAwesomeIcon("drupal");
        public static FontAwesomeIcon Edge => new FontAwesomeIcon("edge");
        public static FontAwesomeIcon Empire => new FontAwesomeIcon("empire");
        public static FontAwesomeIcon Expeditedssl => new FontAwesomeIcon("expeditedssl");
        public static FontAwesomeIcon Facebook => new FontAwesomeIcon("facebook");
        public static FontAwesomeIcon FacebookF => new FontAwesomeIcon("facebook-f");
        public static FontAwesomeIcon FacebookOfficial => new FontAwesomeIcon("facebook-official");
        public static FontAwesomeIcon FacebookSquare => new FontAwesomeIcon("facebook-square");
        public static FontAwesomeIcon Firefox => new FontAwesomeIcon("firefox");
        public static FontAwesomeIcon Flickr => new FontAwesomeIcon("flickr");
        public static FontAwesomeIcon Fonticons => new FontAwesomeIcon("fonticons");
        public static FontAwesomeIcon FortAwesome => new FontAwesomeIcon("fort-awesome");
        public static FontAwesomeIcon Forumbee => new FontAwesomeIcon("forumbee");
        public static FontAwesomeIcon Foursquare => new FontAwesomeIcon("foursquare");
        public static FontAwesomeIcon Ge => new FontAwesomeIcon("ge");
        public static FontAwesomeIcon GetPocket => new FontAwesomeIcon("get-pocket");
        public static FontAwesomeIcon Git => new FontAwesomeIcon("git");
        public static FontAwesomeIcon GitSquare => new FontAwesomeIcon("git-square");
        public static FontAwesomeIcon Github => new FontAwesomeIcon("github");
        public static FontAwesomeIcon GithubAlt => new FontAwesomeIcon("github-alt");
        public static FontAwesomeIcon GithubSquare => new FontAwesomeIcon("github-square");
        public static FontAwesomeIcon Gittip => new FontAwesomeIcon("gittip");
        public static FontAwesomeIcon Google => new FontAwesomeIcon("google");
        public static FontAwesomeIcon GooglePlus => new FontAwesomeIcon("google-plus");
        public static FontAwesomeIcon GooglePlusSquare => new FontAwesomeIcon("google-plus-square");
        public static FontAwesomeIcon Gratipay => new FontAwesomeIcon("gratipay");
        public static FontAwesomeIcon HackerNews => new FontAwesomeIcon("hacker-news");
        public static FontAwesomeIcon Houzz => new FontAwesomeIcon("houzz");
        public static FontAwesomeIcon Html5 => new FontAwesomeIcon("html5");
        public static FontAwesomeIcon InternetExplorer => new FontAwesomeIcon("internet-explorer");
        public static FontAwesomeIcon Ioxhost => new FontAwesomeIcon("ioxhost");
        public static FontAwesomeIcon Joomla => new FontAwesomeIcon("joomla");
        public static FontAwesomeIcon Jsfiddle => new FontAwesomeIcon("jsfiddle");
        public static FontAwesomeIcon Lastfm => new FontAwesomeIcon("lastfm");
        public static FontAwesomeIcon LastfmSquare => new FontAwesomeIcon("lastfm-square");
        public static FontAwesomeIcon Leanpub => new FontAwesomeIcon("leanpub");
        public static FontAwesomeIcon Linkedin => new FontAwesomeIcon("linkedin");
        public static FontAwesomeIcon LinkedinSquare => new FontAwesomeIcon("linkedin-square");
        public static FontAwesomeIcon Linux => new FontAwesomeIcon("linux");
        public static FontAwesomeIcon Maxcdn => new FontAwesomeIcon("maxcdn");
        public static FontAwesomeIcon Meanpath => new FontAwesomeIcon("meanpath");
        public static FontAwesomeIcon Medium => new FontAwesomeIcon("medium");
        public static FontAwesomeIcon Mixcloud => new FontAwesomeIcon("mixcloud");
        public static FontAwesomeIcon Modx => new FontAwesomeIcon("modx");
        public static FontAwesomeIcon Odnoklassniki => new FontAwesomeIcon("odnoklassniki");
        public static FontAwesomeIcon OdnoklassnikiSquare => new FontAwesomeIcon("odnoklassniki-square");
        public static FontAwesomeIcon Opencart => new FontAwesomeIcon("opencart");
        public static FontAwesomeIcon Openid => new FontAwesomeIcon("openid");
        public static FontAwesomeIcon Opera => new FontAwesomeIcon("opera");
        public static FontAwesomeIcon OptinMonster => new FontAwesomeIcon("optin-monster");
        public static FontAwesomeIcon Pagelines => new FontAwesomeIcon("pagelines");
        public static FontAwesomeIcon PiedPiperAlt => new FontAwesomeIcon("pied-piper-alt");
        public static FontAwesomeIcon PiedPiperPp => new FontAwesomeIcon("pied-piper-pp");
        public static FontAwesomeIcon Pinterest => new FontAwesomeIcon("pinterest");
        public static FontAwesomeIcon PinterestP => new FontAwesomeIcon("pinterest-p");
        public static FontAwesomeIcon PinterestSquare => new FontAwesomeIcon("pinterest-square");
        public static FontAwesomeIcon ProductHunt => new FontAwesomeIcon("product-hunt");
        public static FontAwesomeIcon Qq => new FontAwesomeIcon("qq");
        public static FontAwesomeIcon Ra => new FontAwesomeIcon("ra");
        public static FontAwesomeIcon Rebel => new FontAwesomeIcon("rebel");
        public static FontAwesomeIcon Reddit => new FontAwesomeIcon("reddit");
        public static FontAwesomeIcon RedditAlien => new FontAwesomeIcon("reddit-alien");
        public static FontAwesomeIcon RedditSquare => new FontAwesomeIcon("reddit-square");
        public static FontAwesomeIcon Renren => new FontAwesomeIcon("renren");
        public static FontAwesomeIcon Resistance => new FontAwesomeIcon("resistance");
        public static FontAwesomeIcon Safari => new FontAwesomeIcon("safari");
        public static FontAwesomeIcon Scribd => new FontAwesomeIcon("scribd");
        public static FontAwesomeIcon Sellsy => new FontAwesomeIcon("sellsy");
        public static FontAwesomeIcon Shirtsinbulk => new FontAwesomeIcon("shirtsinbulk");
        public static FontAwesomeIcon Simplybuilt => new FontAwesomeIcon("simplybuilt");
        public static FontAwesomeIcon Skyatlas => new FontAwesomeIcon("skyatlas");
        public static FontAwesomeIcon Skype => new FontAwesomeIcon("skype");
        public static FontAwesomeIcon Slack => new FontAwesomeIcon("slack");
        public static FontAwesomeIcon Slideshare => new FontAwesomeIcon("slideshare");
        public static FontAwesomeIcon Soundcloud => new FontAwesomeIcon("soundcloud");
        public static FontAwesomeIcon Spotify => new FontAwesomeIcon("spotify");
        public static FontAwesomeIcon StackExchange => new FontAwesomeIcon("stack-exchange");
        public static FontAwesomeIcon StackOverflow => new FontAwesomeIcon("stack-overflow");
        public static FontAwesomeIcon Steam => new FontAwesomeIcon("steam");
        public static FontAwesomeIcon SteamSquare => new FontAwesomeIcon("steam-square");
        public static FontAwesomeIcon Stumbleupon => new FontAwesomeIcon("stumbleupon");
        public static FontAwesomeIcon StumbleuponCircle => new FontAwesomeIcon("stumbleupon-circle");
        public static FontAwesomeIcon TencentWeibo => new FontAwesomeIcon("tencent-weibo");
        public static FontAwesomeIcon Trello => new FontAwesomeIcon("trello");
        public static FontAwesomeIcon Tripadvisor => new FontAwesomeIcon("tripadvisor");
        public static FontAwesomeIcon Tumblr => new FontAwesomeIcon("tumblr");
        public static FontAwesomeIcon TumblrSquare => new FontAwesomeIcon("tumblr-square");
        public static FontAwesomeIcon Twitch => new FontAwesomeIcon("twitch");
        public static FontAwesomeIcon Twitter => new FontAwesomeIcon("twitter");
        public static FontAwesomeIcon TwitterSquare => new FontAwesomeIcon("twitter-square");
        public static FontAwesomeIcon Usb => new FontAwesomeIcon("usb");
        public static FontAwesomeIcon Viacoin => new FontAwesomeIcon("viacoin");
        public static FontAwesomeIcon Vimeo => new FontAwesomeIcon("vimeo");
        public static FontAwesomeIcon VimeoSquare => new FontAwesomeIcon("vimeo-square");
        public static FontAwesomeIcon Vine => new FontAwesomeIcon("vine");
        public static FontAwesomeIcon Vk => new FontAwesomeIcon("vk");
        public static FontAwesomeIcon Wechat => new FontAwesomeIcon("wechat");
        public static FontAwesomeIcon Weibo => new FontAwesomeIcon("weibo");
        public static FontAwesomeIcon Weixin => new FontAwesomeIcon("weixin");
        public static FontAwesomeIcon Whatsapp => new FontAwesomeIcon("whatsapp");
        public static FontAwesomeIcon WikipediaW => new FontAwesomeIcon("wikipedia-w");
        public static FontAwesomeIcon Windows => new FontAwesomeIcon("windows");
        public static FontAwesomeIcon Wordpress => new FontAwesomeIcon("wordpress");
        public static FontAwesomeIcon Xing => new FontAwesomeIcon("xing");
        public static FontAwesomeIcon XingSquare => new FontAwesomeIcon("xing-square");
        public static FontAwesomeIcon YCombinator => new FontAwesomeIcon("y-combinator");
        public static FontAwesomeIcon YCombinatorSquare => new FontAwesomeIcon("y-combinator-square");
        public static FontAwesomeIcon Yahoo => new FontAwesomeIcon("yahoo");
        public static FontAwesomeIcon Yc => new FontAwesomeIcon("yc");
        public static FontAwesomeIcon YcSquare => new FontAwesomeIcon("yc-square");
        public static FontAwesomeIcon Yelp => new FontAwesomeIcon("yelp");
        public static FontAwesomeIcon Youtube => new FontAwesomeIcon("youtube");
        public static FontAwesomeIcon YoutubeSquare => new FontAwesomeIcon("youtube-square");
        public static FontAwesomeIcon HSquare => new FontAwesomeIcon("h-square");
        public static FontAwesomeIcon HospitalO => new FontAwesomeIcon("hospital-o");
        public static FontAwesomeIcon Medkit => new FontAwesomeIcon("medkit");
        public static FontAwesomeIcon Stethoscope => new FontAwesomeIcon("stethoscope");
        public static FontAwesomeIcon UserMd => new FontAwesomeIcon("user-md");

        #endregion
    }
}