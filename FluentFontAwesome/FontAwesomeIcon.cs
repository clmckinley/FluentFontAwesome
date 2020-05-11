using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentFontAwesome
{
    public class FontAwesomeTagSettings
    {
        public string TagName { get; set; } = "i";
        public char Quote { get; set; } = '"';
        public bool AriaHidden { get; set; } = true;
        public FontAwesomeIcon MaskIcon = null;
        public string MaskId = null;
        public string Transform = null;


        public static FontAwesomeTagSettings Default = new FontAwesomeTagSettings();
    }

    public class FontAwesomeIcon
    {
        private string _name;

        private Size? _size = null;
        private bool _fixedWidth = false;
        private bool _bordered = false;
        private Pull _pull = FluentFontAwesome.Pull.None;
        private Animation _animation = FluentFontAwesome.Animation.None;
        private Rotation _rotation = FluentFontAwesome.Rotation.None;
        private Flip _flip = FluentFontAwesome.Flip.None;
        private Rendering _rendering = FluentFontAwesome.Rendering.Solid;
        private FontAwesomeIcon _maskIcon = null;
        private string _maskId = null;
        private string _transform = null;

        public FontAwesomeIcon()
        {
        }

        public FontAwesomeIcon(string name, Rendering? rendering = null)
        {
            Name(name, rendering);
        }
        public FontAwesomeIcon(FontAwesomeIconsEnum nameEnum, Rendering? rendering = null)
        {
            var name =  nameEnum.GetCssClassName();
            Name(name, rendering);
        }

        public string GetTag(FontAwesomeTagSettings tagSettings = null)
        {
            _size = _size ?? FluentFontAwesome.Size.Normal;
            tagSettings = tagSettings ?? FontAwesomeTagSettings.Default;
            var tagName = tagSettings.TagName;
            var quote = tagSettings.Quote;


            var attributes = string.Join(" ", GetAttributes(tagSettings).Select(s => $"{s.Key}={quote}{s.Value}{quote}"));
            return $"<{tagName} class={quote}{GetClass()}{quote} {attributes}></{tagName}>";
        }
        public List<KeyValuePair<string,string>> GetAttributes(FontAwesomeTagSettings tagSettings = null)
        {
            var rVal = new List<KeyValuePair<string, string>>();
            tagSettings = tagSettings ?? FontAwesomeTagSettings.Default;
            if(tagSettings.AriaHidden == true)
            {
                rVal.Add(new KeyValuePair<string, string>("aria-hidden", "true"));
            }
            if (tagSettings.MaskIcon != null)
            {
                rVal.Add(new KeyValuePair<string, string>("data-fa-mask", tagSettings.MaskIcon.GetClass()));
            }
            if (tagSettings.MaskId != null)
            {
                rVal.Add(new KeyValuePair<string, string>("data-fa-mask-id=", tagSettings.MaskId));
            }
            if (tagSettings.Transform != null)
            {
                rVal.Add(new KeyValuePair<string, string>("data-fa-transform=", tagSettings.Transform));
            }
            return rVal;
        }
            


        public string GetClass() => string.Join(" ", GetClasses());

        public IEnumerable<string> GetClasses()
        {
            switch(_rendering)
            {
                case FluentFontAwesome.Rendering.Solid:
                    yield return "fas";
                    break;
                case FluentFontAwesome.Rendering.Regular:
                    yield return "far";
                    break;
                case FluentFontAwesome.Rendering.Light:
                    yield return "fal";
                    break;
                case FluentFontAwesome.Rendering.Duotone:
                    yield return "fad";
                    break;
                case FluentFontAwesome.Rendering.Brands:
                    yield return "fab";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("_rendering");
            }


            yield return "fa-" + _name;

            switch (_size ?? FluentFontAwesome.Size.Normal)
            {
                case FluentFontAwesome.Size.xSmall:
                    yield return "fa-xs";
                    break;
                case FluentFontAwesome.Size.Small:
                    yield return "fa-sm";
                    break;
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
                case FluentFontAwesome.Size.x5:
                    yield return "fa-5x";
                    break;
                case FluentFontAwesome.Size.x7:
                    yield return "fa-7x";
                    break;
                case FluentFontAwesome.Size.x10:
                    yield return "fa-10x";
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
                    throw new ArgumentOutOfRangeException("_pull");
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
                    throw new ArgumentOutOfRangeException("_animation");
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
                    throw new ArgumentOutOfRangeException("_rotation");
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
                case FluentFontAwesome.Flip.Both:
                    yield return "fa-flip-both";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("_flip");

            }
        }

        #region Fluent Methods

        //[fluent regex]   : private (\w+) (_(\w+)).*;
        //[fluent replace] : public $1 $3() => $2;\npublic FontAwesomeIcon $3($1 $3)\n{\n\t$2 = $3;\n\treturn this;\n}\n

        public string Name() => _name;

        public FontAwesomeIcon Name(string name, Rendering? rendering = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            name = name.ToLower(); //make sure it is lower case;
            if (name.StartsWith("fa-"))
                name = name.Substring(3);

            _name = name;
           _rendering = rendering ?? RenderingExtensions.GetRendering(_name);

            return this;
        }

        public Size? Size() => _size;

        public FontAwesomeIcon Rendering(Rendering rendering)
        {
            _rendering = rendering;
            return this;
        }

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
        // [version] : 5.13.0

        public static FontAwesomeIcon Icon500px => new FontAwesomeIcon("500px");
        public static FontAwesomeIcon AccessibleIcon => new FontAwesomeIcon("accessible-icon");
        public static FontAwesomeIcon Accusoft => new FontAwesomeIcon("accusoft");
        public static FontAwesomeIcon AcquisitionsIncorporated => new FontAwesomeIcon("acquisitions-incorporated");
        public static FontAwesomeIcon Ad => new FontAwesomeIcon("ad");
        public static FontAwesomeIcon AddressBook => new FontAwesomeIcon("address-book");
        public static FontAwesomeIcon AddressCard => new FontAwesomeIcon("address-card");
        public static FontAwesomeIcon Adjust => new FontAwesomeIcon("adjust");
        public static FontAwesomeIcon Adn => new FontAwesomeIcon("adn");
        public static FontAwesomeIcon Adobe => new FontAwesomeIcon("adobe");
        public static FontAwesomeIcon Adversal => new FontAwesomeIcon("adversal");
        public static FontAwesomeIcon Affiliatetheme => new FontAwesomeIcon("affiliatetheme");
        public static FontAwesomeIcon AirFreshener => new FontAwesomeIcon("air-freshener");
        public static FontAwesomeIcon Airbnb => new FontAwesomeIcon("airbnb");
        public static FontAwesomeIcon Algolia => new FontAwesomeIcon("algolia");
        public static FontAwesomeIcon AlignCenter => new FontAwesomeIcon("align-center");
        public static FontAwesomeIcon AlignJustify => new FontAwesomeIcon("align-justify");
        public static FontAwesomeIcon AlignLeft => new FontAwesomeIcon("align-left");
        public static FontAwesomeIcon AlignRight => new FontAwesomeIcon("align-right");
        public static FontAwesomeIcon Alipay => new FontAwesomeIcon("alipay");
        public static FontAwesomeIcon Allergies => new FontAwesomeIcon("allergies");
        public static FontAwesomeIcon Amazon => new FontAwesomeIcon("amazon");
        public static FontAwesomeIcon AmazonPay => new FontAwesomeIcon("amazon-pay");
        public static FontAwesomeIcon Ambulance => new FontAwesomeIcon("ambulance");
        public static FontAwesomeIcon AmericanSignLanguageInterpreting => new FontAwesomeIcon("american-sign-language-interpreting");
        public static FontAwesomeIcon Amilia => new FontAwesomeIcon("amilia");
        public static FontAwesomeIcon Anchor => new FontAwesomeIcon("anchor");
        public static FontAwesomeIcon Android => new FontAwesomeIcon("android");
        public static FontAwesomeIcon Angellist => new FontAwesomeIcon("angellist");
        public static FontAwesomeIcon AngleDoubleDown => new FontAwesomeIcon("angle-double-down");
        public static FontAwesomeIcon AngleDoubleLeft => new FontAwesomeIcon("angle-double-left");
        public static FontAwesomeIcon AngleDoubleRight => new FontAwesomeIcon("angle-double-right");
        public static FontAwesomeIcon AngleDoubleUp => new FontAwesomeIcon("angle-double-up");
        public static FontAwesomeIcon AngleDown => new FontAwesomeIcon("angle-down");
        public static FontAwesomeIcon AngleLeft => new FontAwesomeIcon("angle-left");
        public static FontAwesomeIcon AngleRight => new FontAwesomeIcon("angle-right");
        public static FontAwesomeIcon AngleUp => new FontAwesomeIcon("angle-up");
        public static FontAwesomeIcon Angry => new FontAwesomeIcon("angry");
        public static FontAwesomeIcon Angrycreative => new FontAwesomeIcon("angrycreative");
        public static FontAwesomeIcon Angular => new FontAwesomeIcon("angular");
        public static FontAwesomeIcon Ankh => new FontAwesomeIcon("ankh");
        public static FontAwesomeIcon AppStore => new FontAwesomeIcon("app-store");
        public static FontAwesomeIcon AppStoreIos => new FontAwesomeIcon("app-store-ios");
        public static FontAwesomeIcon Apper => new FontAwesomeIcon("apper");
        public static FontAwesomeIcon Apple => new FontAwesomeIcon("apple");
        public static FontAwesomeIcon AppleAlt => new FontAwesomeIcon("apple-alt");
        public static FontAwesomeIcon ApplePay => new FontAwesomeIcon("apple-pay");
        public static FontAwesomeIcon Archive => new FontAwesomeIcon("archive");
        public static FontAwesomeIcon Archway => new FontAwesomeIcon("archway");
        public static FontAwesomeIcon ArrowAltCircleDown => new FontAwesomeIcon("arrow-alt-circle-down");
        public static FontAwesomeIcon ArrowAltCircleLeft => new FontAwesomeIcon("arrow-alt-circle-left");
        public static FontAwesomeIcon ArrowAltCircleRight => new FontAwesomeIcon("arrow-alt-circle-right");
        public static FontAwesomeIcon ArrowAltCircleUp => new FontAwesomeIcon("arrow-alt-circle-up");
        public static FontAwesomeIcon ArrowCircleDown => new FontAwesomeIcon("arrow-circle-down");
        public static FontAwesomeIcon ArrowCircleLeft => new FontAwesomeIcon("arrow-circle-left");
        public static FontAwesomeIcon ArrowCircleRight => new FontAwesomeIcon("arrow-circle-right");
        public static FontAwesomeIcon ArrowCircleUp => new FontAwesomeIcon("arrow-circle-up");
        public static FontAwesomeIcon ArrowDown => new FontAwesomeIcon("arrow-down");
        public static FontAwesomeIcon ArrowLeft => new FontAwesomeIcon("arrow-left");
        public static FontAwesomeIcon ArrowRight => new FontAwesomeIcon("arrow-right");
        public static FontAwesomeIcon ArrowUp => new FontAwesomeIcon("arrow-up");
        public static FontAwesomeIcon ArrowsAlt => new FontAwesomeIcon("arrows-alt");
        public static FontAwesomeIcon ArrowsAltH => new FontAwesomeIcon("arrows-alt-h");
        public static FontAwesomeIcon ArrowsAltV => new FontAwesomeIcon("arrows-alt-v");
        public static FontAwesomeIcon Artstation => new FontAwesomeIcon("artstation");
        public static FontAwesomeIcon AssistiveListeningSystems => new FontAwesomeIcon("assistive-listening-systems");
        public static FontAwesomeIcon Asterisk => new FontAwesomeIcon("asterisk");
        public static FontAwesomeIcon Asymmetrik => new FontAwesomeIcon("asymmetrik");
        public static FontAwesomeIcon At => new FontAwesomeIcon("at");
        public static FontAwesomeIcon Atlas => new FontAwesomeIcon("atlas");
        public static FontAwesomeIcon Atlassian => new FontAwesomeIcon("atlassian");
        public static FontAwesomeIcon Atom => new FontAwesomeIcon("atom");
        public static FontAwesomeIcon Audible => new FontAwesomeIcon("audible");
        public static FontAwesomeIcon AudioDescription => new FontAwesomeIcon("audio-description");
        public static FontAwesomeIcon Autoprefixer => new FontAwesomeIcon("autoprefixer");
        public static FontAwesomeIcon Avianex => new FontAwesomeIcon("avianex");
        public static FontAwesomeIcon Aviato => new FontAwesomeIcon("aviato");
        public static FontAwesomeIcon Award => new FontAwesomeIcon("award");
        public static FontAwesomeIcon Aws => new FontAwesomeIcon("aws");
        public static FontAwesomeIcon Baby => new FontAwesomeIcon("baby");
        public static FontAwesomeIcon BabyCarriage => new FontAwesomeIcon("baby-carriage");
        public static FontAwesomeIcon Backspace => new FontAwesomeIcon("backspace");
        public static FontAwesomeIcon Backward => new FontAwesomeIcon("backward");
        public static FontAwesomeIcon Bacon => new FontAwesomeIcon("bacon");
        public static FontAwesomeIcon BalanceScale => new FontAwesomeIcon("balance-scale");
        public static FontAwesomeIcon Ban => new FontAwesomeIcon("ban");
        public static FontAwesomeIcon BandAid => new FontAwesomeIcon("band-aid");
        public static FontAwesomeIcon Bandcamp => new FontAwesomeIcon("bandcamp");
        public static FontAwesomeIcon Barcode => new FontAwesomeIcon("barcode");
        public static FontAwesomeIcon Bars => new FontAwesomeIcon("bars");
        public static FontAwesomeIcon BaseballBall => new FontAwesomeIcon("baseball-ball");
        public static FontAwesomeIcon BasketballBall => new FontAwesomeIcon("basketball-ball");
        public static FontAwesomeIcon Bath => new FontAwesomeIcon("bath");
        public static FontAwesomeIcon BatteryEmpty => new FontAwesomeIcon("battery-empty");
        public static FontAwesomeIcon BatteryFull => new FontAwesomeIcon("battery-full");
        public static FontAwesomeIcon BatteryHalf => new FontAwesomeIcon("battery-half");
        public static FontAwesomeIcon BatteryQuarter => new FontAwesomeIcon("battery-quarter");
        public static FontAwesomeIcon BatteryThreeQuarters => new FontAwesomeIcon("battery-three-quarters");
        public static FontAwesomeIcon BattleNet => new FontAwesomeIcon("battle-net");
        public static FontAwesomeIcon Bed => new FontAwesomeIcon("bed");
        public static FontAwesomeIcon Beer => new FontAwesomeIcon("beer");
        public static FontAwesomeIcon Behance => new FontAwesomeIcon("behance");
        public static FontAwesomeIcon BehanceSquare => new FontAwesomeIcon("behance-square");
        public static FontAwesomeIcon Bell => new FontAwesomeIcon("bell");
        public static FontAwesomeIcon BellSlash => new FontAwesomeIcon("bell-slash");
        public static FontAwesomeIcon BezierCurve => new FontAwesomeIcon("bezier-curve");
        public static FontAwesomeIcon Bible => new FontAwesomeIcon("bible");
        public static FontAwesomeIcon Bicycle => new FontAwesomeIcon("bicycle");
        public static FontAwesomeIcon Bimobject => new FontAwesomeIcon("bimobject");
        public static FontAwesomeIcon Binoculars => new FontAwesomeIcon("binoculars");
        public static FontAwesomeIcon Biohazard => new FontAwesomeIcon("biohazard");
        public static FontAwesomeIcon BirthdayCake => new FontAwesomeIcon("birthday-cake");
        public static FontAwesomeIcon Bitbucket => new FontAwesomeIcon("bitbucket");
        public static FontAwesomeIcon Bitcoin => new FontAwesomeIcon("bitcoin");
        public static FontAwesomeIcon Bity => new FontAwesomeIcon("bity");
        public static FontAwesomeIcon BlackTie => new FontAwesomeIcon("black-tie");
        public static FontAwesomeIcon Blackberry => new FontAwesomeIcon("blackberry");
        public static FontAwesomeIcon Blender => new FontAwesomeIcon("blender");
        public static FontAwesomeIcon BlenderPhone => new FontAwesomeIcon("blender-phone");
        public static FontAwesomeIcon Blind => new FontAwesomeIcon("blind");
        public static FontAwesomeIcon Blog => new FontAwesomeIcon("blog");
        public static FontAwesomeIcon Blogger => new FontAwesomeIcon("blogger");
        public static FontAwesomeIcon BloggerB => new FontAwesomeIcon("blogger-b");
        public static FontAwesomeIcon Bluetooth => new FontAwesomeIcon("bluetooth");
        public static FontAwesomeIcon BluetoothB => new FontAwesomeIcon("bluetooth-b");
        public static FontAwesomeIcon Bold => new FontAwesomeIcon("bold");
        public static FontAwesomeIcon Bolt => new FontAwesomeIcon("bolt");
        public static FontAwesomeIcon Bomb => new FontAwesomeIcon("bomb");
        public static FontAwesomeIcon Bone => new FontAwesomeIcon("bone");
        public static FontAwesomeIcon Bong => new FontAwesomeIcon("bong");
        public static FontAwesomeIcon Book => new FontAwesomeIcon("book");
        public static FontAwesomeIcon BookDead => new FontAwesomeIcon("book-dead");
        public static FontAwesomeIcon BookMedical => new FontAwesomeIcon("book-medical");
        public static FontAwesomeIcon BookOpen => new FontAwesomeIcon("book-open");
        public static FontAwesomeIcon BookReader => new FontAwesomeIcon("book-reader");
        public static FontAwesomeIcon Bookmark => new FontAwesomeIcon("bookmark");
        public static FontAwesomeIcon Bootstrap => new FontAwesomeIcon("bootstrap");
        public static FontAwesomeIcon BowlingBall => new FontAwesomeIcon("bowling-ball");
        public static FontAwesomeIcon Box => new FontAwesomeIcon("box");
        public static FontAwesomeIcon BoxOpen => new FontAwesomeIcon("box-open");
        public static FontAwesomeIcon Boxes => new FontAwesomeIcon("boxes");
        public static FontAwesomeIcon Braille => new FontAwesomeIcon("braille");
        public static FontAwesomeIcon Brain => new FontAwesomeIcon("brain");
        public static FontAwesomeIcon BreadSlice => new FontAwesomeIcon("bread-slice");
        public static FontAwesomeIcon Briefcase => new FontAwesomeIcon("briefcase");
        public static FontAwesomeIcon BriefcaseMedical => new FontAwesomeIcon("briefcase-medical");
        public static FontAwesomeIcon BroadcastTower => new FontAwesomeIcon("broadcast-tower");
        public static FontAwesomeIcon Broom => new FontAwesomeIcon("broom");
        public static FontAwesomeIcon Brush => new FontAwesomeIcon("brush");
        public static FontAwesomeIcon Btc => new FontAwesomeIcon("btc");
        public static FontAwesomeIcon Buffer => new FontAwesomeIcon("buffer");
        public static FontAwesomeIcon Bug => new FontAwesomeIcon("bug");
        public static FontAwesomeIcon Building => new FontAwesomeIcon("building");
        public static FontAwesomeIcon Bullhorn => new FontAwesomeIcon("bullhorn");
        public static FontAwesomeIcon Bullseye => new FontAwesomeIcon("bullseye");
        public static FontAwesomeIcon Burn => new FontAwesomeIcon("burn");
        public static FontAwesomeIcon Buromobelexperte => new FontAwesomeIcon("buromobelexperte");
        public static FontAwesomeIcon Bus => new FontAwesomeIcon("bus");
        public static FontAwesomeIcon BusAlt => new FontAwesomeIcon("bus-alt");
        public static FontAwesomeIcon BusinessTime => new FontAwesomeIcon("business-time");
        public static FontAwesomeIcon Buysellads => new FontAwesomeIcon("buysellads");
        public static FontAwesomeIcon Calculator => new FontAwesomeIcon("calculator");
        public static FontAwesomeIcon Calendar => new FontAwesomeIcon("calendar");
        public static FontAwesomeIcon CalendarAlt => new FontAwesomeIcon("calendar-alt");
        public static FontAwesomeIcon CalendarCheck => new FontAwesomeIcon("calendar-check");
        public static FontAwesomeIcon CalendarDay => new FontAwesomeIcon("calendar-day");
        public static FontAwesomeIcon CalendarMinus => new FontAwesomeIcon("calendar-minus");
        public static FontAwesomeIcon CalendarPlus => new FontAwesomeIcon("calendar-plus");
        public static FontAwesomeIcon CalendarTimes => new FontAwesomeIcon("calendar-times");
        public static FontAwesomeIcon CalendarWeek => new FontAwesomeIcon("calendar-week");
        public static FontAwesomeIcon Camera => new FontAwesomeIcon("camera");
        public static FontAwesomeIcon CameraRetro => new FontAwesomeIcon("camera-retro");
        public static FontAwesomeIcon Campground => new FontAwesomeIcon("campground");
        public static FontAwesomeIcon CanadianMapleLeaf => new FontAwesomeIcon("canadian-maple-leaf");
        public static FontAwesomeIcon CandyCane => new FontAwesomeIcon("candy-cane");
        public static FontAwesomeIcon Cannabis => new FontAwesomeIcon("cannabis");
        public static FontAwesomeIcon Capsules => new FontAwesomeIcon("capsules");
        public static FontAwesomeIcon Car => new FontAwesomeIcon("car");
        public static FontAwesomeIcon CarAlt => new FontAwesomeIcon("car-alt");
        public static FontAwesomeIcon CarBattery => new FontAwesomeIcon("car-battery");
        public static FontAwesomeIcon CarCrash => new FontAwesomeIcon("car-crash");
        public static FontAwesomeIcon CarSide => new FontAwesomeIcon("car-side");
        public static FontAwesomeIcon CaretDown => new FontAwesomeIcon("caret-down");
        public static FontAwesomeIcon CaretLeft => new FontAwesomeIcon("caret-left");
        public static FontAwesomeIcon CaretRight => new FontAwesomeIcon("caret-right");
        public static FontAwesomeIcon CaretSquareDown => new FontAwesomeIcon("caret-square-down");
        public static FontAwesomeIcon CaretSquareLeft => new FontAwesomeIcon("caret-square-left");
        public static FontAwesomeIcon CaretSquareRight => new FontAwesomeIcon("caret-square-right");
        public static FontAwesomeIcon CaretSquareUp => new FontAwesomeIcon("caret-square-up");
        public static FontAwesomeIcon CaretUp => new FontAwesomeIcon("caret-up");
        public static FontAwesomeIcon Carrot => new FontAwesomeIcon("carrot");
        public static FontAwesomeIcon CartArrowDown => new FontAwesomeIcon("cart-arrow-down");
        public static FontAwesomeIcon CartPlus => new FontAwesomeIcon("cart-plus");
        public static FontAwesomeIcon CashRegister => new FontAwesomeIcon("cash-register");
        public static FontAwesomeIcon Cat => new FontAwesomeIcon("cat");
        public static FontAwesomeIcon CcAmazonPay => new FontAwesomeIcon("cc-amazon-pay");
        public static FontAwesomeIcon CcAmex => new FontAwesomeIcon("cc-amex");
        public static FontAwesomeIcon CcApplePay => new FontAwesomeIcon("cc-apple-pay");
        public static FontAwesomeIcon CcDinersClub => new FontAwesomeIcon("cc-diners-club");
        public static FontAwesomeIcon CcDiscover => new FontAwesomeIcon("cc-discover");
        public static FontAwesomeIcon CcJcb => new FontAwesomeIcon("cc-jcb");
        public static FontAwesomeIcon CcMastercard => new FontAwesomeIcon("cc-mastercard");
        public static FontAwesomeIcon CcPaypal => new FontAwesomeIcon("cc-paypal");
        public static FontAwesomeIcon CcStripe => new FontAwesomeIcon("cc-stripe");
        public static FontAwesomeIcon CcVisa => new FontAwesomeIcon("cc-visa");
        public static FontAwesomeIcon Centercode => new FontAwesomeIcon("centercode");
        public static FontAwesomeIcon Centos => new FontAwesomeIcon("centos");
        public static FontAwesomeIcon Certificate => new FontAwesomeIcon("certificate");
        public static FontAwesomeIcon Chair => new FontAwesomeIcon("chair");
        public static FontAwesomeIcon Chalkboard => new FontAwesomeIcon("chalkboard");
        public static FontAwesomeIcon ChalkboardTeacher => new FontAwesomeIcon("chalkboard-teacher");
        public static FontAwesomeIcon ChargingStation => new FontAwesomeIcon("charging-station");
        public static FontAwesomeIcon ChartArea => new FontAwesomeIcon("chart-area");
        public static FontAwesomeIcon ChartBar => new FontAwesomeIcon("chart-bar");
        public static FontAwesomeIcon ChartLine => new FontAwesomeIcon("chart-line");
        public static FontAwesomeIcon ChartPie => new FontAwesomeIcon("chart-pie");
        public static FontAwesomeIcon Check => new FontAwesomeIcon("check");
        public static FontAwesomeIcon CheckCircle => new FontAwesomeIcon("check-circle");
        public static FontAwesomeIcon CheckDouble => new FontAwesomeIcon("check-double");
        public static FontAwesomeIcon CheckSquare => new FontAwesomeIcon("check-square");
        public static FontAwesomeIcon Cheese => new FontAwesomeIcon("cheese");
        public static FontAwesomeIcon Chess => new FontAwesomeIcon("chess");
        public static FontAwesomeIcon ChessBishop => new FontAwesomeIcon("chess-bishop");
        public static FontAwesomeIcon ChessBoard => new FontAwesomeIcon("chess-board");
        public static FontAwesomeIcon ChessKing => new FontAwesomeIcon("chess-king");
        public static FontAwesomeIcon ChessKnight => new FontAwesomeIcon("chess-knight");
        public static FontAwesomeIcon ChessPawn => new FontAwesomeIcon("chess-pawn");
        public static FontAwesomeIcon ChessQueen => new FontAwesomeIcon("chess-queen");
        public static FontAwesomeIcon ChessRook => new FontAwesomeIcon("chess-rook");
        public static FontAwesomeIcon ChevronCircleDown => new FontAwesomeIcon("chevron-circle-down");
        public static FontAwesomeIcon ChevronCircleLeft => new FontAwesomeIcon("chevron-circle-left");
        public static FontAwesomeIcon ChevronCircleRight => new FontAwesomeIcon("chevron-circle-right");
        public static FontAwesomeIcon ChevronCircleUp => new FontAwesomeIcon("chevron-circle-up");
        public static FontAwesomeIcon ChevronDown => new FontAwesomeIcon("chevron-down");
        public static FontAwesomeIcon ChevronLeft => new FontAwesomeIcon("chevron-left");
        public static FontAwesomeIcon ChevronRight => new FontAwesomeIcon("chevron-right");
        public static FontAwesomeIcon ChevronUp => new FontAwesomeIcon("chevron-up");
        public static FontAwesomeIcon Child => new FontAwesomeIcon("child");
        public static FontAwesomeIcon Chrome => new FontAwesomeIcon("chrome");
        public static FontAwesomeIcon Chromecast => new FontAwesomeIcon("chromecast");
        public static FontAwesomeIcon Church => new FontAwesomeIcon("church");
        public static FontAwesomeIcon Circle => new FontAwesomeIcon("circle");
        public static FontAwesomeIcon CircleNotch => new FontAwesomeIcon("circle-notch");
        public static FontAwesomeIcon City => new FontAwesomeIcon("city");
        public static FontAwesomeIcon ClinicMedical => new FontAwesomeIcon("clinic-medical");
        public static FontAwesomeIcon Clipboard => new FontAwesomeIcon("clipboard");
        public static FontAwesomeIcon ClipboardCheck => new FontAwesomeIcon("clipboard-check");
        public static FontAwesomeIcon ClipboardList => new FontAwesomeIcon("clipboard-list");
        public static FontAwesomeIcon Clock => new FontAwesomeIcon("clock");
        public static FontAwesomeIcon Clone => new FontAwesomeIcon("clone");
        public static FontAwesomeIcon ClosedCaptioning => new FontAwesomeIcon("closed-captioning");
        public static FontAwesomeIcon Cloud => new FontAwesomeIcon("cloud");
        public static FontAwesomeIcon CloudDownloadAlt => new FontAwesomeIcon("cloud-download-alt");
        public static FontAwesomeIcon CloudMeatball => new FontAwesomeIcon("cloud-meatball");
        public static FontAwesomeIcon CloudMoon => new FontAwesomeIcon("cloud-moon");
        public static FontAwesomeIcon CloudMoonRain => new FontAwesomeIcon("cloud-moon-rain");
        public static FontAwesomeIcon CloudRain => new FontAwesomeIcon("cloud-rain");
        public static FontAwesomeIcon CloudShowersHeavy => new FontAwesomeIcon("cloud-showers-heavy");
        public static FontAwesomeIcon CloudSun => new FontAwesomeIcon("cloud-sun");
        public static FontAwesomeIcon CloudSunRain => new FontAwesomeIcon("cloud-sun-rain");
        public static FontAwesomeIcon CloudUploadAlt => new FontAwesomeIcon("cloud-upload-alt");
        public static FontAwesomeIcon Cloudscale => new FontAwesomeIcon("cloudscale");
        public static FontAwesomeIcon Cloudsmith => new FontAwesomeIcon("cloudsmith");
        public static FontAwesomeIcon Cloudversify => new FontAwesomeIcon("cloudversify");
        public static FontAwesomeIcon Cocktail => new FontAwesomeIcon("cocktail");
        public static FontAwesomeIcon Code => new FontAwesomeIcon("code");
        public static FontAwesomeIcon CodeBranch => new FontAwesomeIcon("code-branch");
        public static FontAwesomeIcon Codepen => new FontAwesomeIcon("codepen");
        public static FontAwesomeIcon Codiepie => new FontAwesomeIcon("codiepie");
        public static FontAwesomeIcon Coffee => new FontAwesomeIcon("coffee");
        public static FontAwesomeIcon Cog => new FontAwesomeIcon("cog");
        public static FontAwesomeIcon Cogs => new FontAwesomeIcon("cogs");
        public static FontAwesomeIcon Coins => new FontAwesomeIcon("coins");
        public static FontAwesomeIcon Columns => new FontAwesomeIcon("columns");
        public static FontAwesomeIcon Comment => new FontAwesomeIcon("comment");
        public static FontAwesomeIcon CommentAlt => new FontAwesomeIcon("comment-alt");
        public static FontAwesomeIcon CommentDollar => new FontAwesomeIcon("comment-dollar");
        public static FontAwesomeIcon CommentDots => new FontAwesomeIcon("comment-dots");
        public static FontAwesomeIcon CommentMedical => new FontAwesomeIcon("comment-medical");
        public static FontAwesomeIcon CommentSlash => new FontAwesomeIcon("comment-slash");
        public static FontAwesomeIcon Comments => new FontAwesomeIcon("comments");
        public static FontAwesomeIcon CommentsDollar => new FontAwesomeIcon("comments-dollar");
        public static FontAwesomeIcon CompactDisc => new FontAwesomeIcon("compact-disc");
        public static FontAwesomeIcon Compass => new FontAwesomeIcon("compass");
        public static FontAwesomeIcon Compress => new FontAwesomeIcon("compress");
        public static FontAwesomeIcon CompressArrowsAlt => new FontAwesomeIcon("compress-arrows-alt");
        public static FontAwesomeIcon ConciergeBell => new FontAwesomeIcon("concierge-bell");
        public static FontAwesomeIcon Confluence => new FontAwesomeIcon("confluence");
        public static FontAwesomeIcon Connectdevelop => new FontAwesomeIcon("connectdevelop");
        public static FontAwesomeIcon Contao => new FontAwesomeIcon("contao");
        public static FontAwesomeIcon Cookie => new FontAwesomeIcon("cookie");
        public static FontAwesomeIcon CookieBite => new FontAwesomeIcon("cookie-bite");
        public static FontAwesomeIcon Copy => new FontAwesomeIcon("copy");
        public static FontAwesomeIcon Copyright => new FontAwesomeIcon("copyright");
        public static FontAwesomeIcon Couch => new FontAwesomeIcon("couch");
        public static FontAwesomeIcon Cpanel => new FontAwesomeIcon("cpanel");
        public static FontAwesomeIcon CreativeCommons => new FontAwesomeIcon("creative-commons");
        public static FontAwesomeIcon CreativeCommonsBy => new FontAwesomeIcon("creative-commons-by");
        public static FontAwesomeIcon CreativeCommonsNc => new FontAwesomeIcon("creative-commons-nc");
        public static FontAwesomeIcon CreativeCommonsNcEu => new FontAwesomeIcon("creative-commons-nc-eu");
        public static FontAwesomeIcon CreativeCommonsNcJp => new FontAwesomeIcon("creative-commons-nc-jp");
        public static FontAwesomeIcon CreativeCommonsNd => new FontAwesomeIcon("creative-commons-nd");
        public static FontAwesomeIcon CreativeCommonsPd => new FontAwesomeIcon("creative-commons-pd");
        public static FontAwesomeIcon CreativeCommonsPdAlt => new FontAwesomeIcon("creative-commons-pd-alt");
        public static FontAwesomeIcon CreativeCommonsRemix => new FontAwesomeIcon("creative-commons-remix");
        public static FontAwesomeIcon CreativeCommonsSa => new FontAwesomeIcon("creative-commons-sa");
        public static FontAwesomeIcon CreativeCommonsSampling => new FontAwesomeIcon("creative-commons-sampling");
        public static FontAwesomeIcon CreativeCommonsSamplingPlus => new FontAwesomeIcon("creative-commons-sampling-plus");
        public static FontAwesomeIcon CreativeCommonsShare => new FontAwesomeIcon("creative-commons-share");
        public static FontAwesomeIcon CreativeCommonsZero => new FontAwesomeIcon("creative-commons-zero");
        public static FontAwesomeIcon CreditCard => new FontAwesomeIcon("credit-card");
        public static FontAwesomeIcon CriticalRole => new FontAwesomeIcon("critical-role");
        public static FontAwesomeIcon Crop => new FontAwesomeIcon("crop");
        public static FontAwesomeIcon CropAlt => new FontAwesomeIcon("crop-alt");
        public static FontAwesomeIcon Cross => new FontAwesomeIcon("cross");
        public static FontAwesomeIcon Crosshairs => new FontAwesomeIcon("crosshairs");
        public static FontAwesomeIcon Crow => new FontAwesomeIcon("crow");
        public static FontAwesomeIcon Crown => new FontAwesomeIcon("crown");
        public static FontAwesomeIcon Crutch => new FontAwesomeIcon("crutch");
        public static FontAwesomeIcon Css3 => new FontAwesomeIcon("css3");
        public static FontAwesomeIcon Css3Alt => new FontAwesomeIcon("css3-alt");
        public static FontAwesomeIcon Cube => new FontAwesomeIcon("cube");
        public static FontAwesomeIcon Cubes => new FontAwesomeIcon("cubes");
        public static FontAwesomeIcon Cut => new FontAwesomeIcon("cut");
        public static FontAwesomeIcon Cuttlefish => new FontAwesomeIcon("cuttlefish");
        public static FontAwesomeIcon DAndD => new FontAwesomeIcon("d-and-d");
        public static FontAwesomeIcon DAndDBeyond => new FontAwesomeIcon("d-and-d-beyond");
        public static FontAwesomeIcon Dashcube => new FontAwesomeIcon("dashcube");
        public static FontAwesomeIcon Database => new FontAwesomeIcon("database");
        public static FontAwesomeIcon Deaf => new FontAwesomeIcon("deaf");
        public static FontAwesomeIcon Delicious => new FontAwesomeIcon("delicious");
        public static FontAwesomeIcon Democrat => new FontAwesomeIcon("democrat");
        public static FontAwesomeIcon Deploydog => new FontAwesomeIcon("deploydog");
        public static FontAwesomeIcon Deskpro => new FontAwesomeIcon("deskpro");
        public static FontAwesomeIcon Desktop => new FontAwesomeIcon("desktop");
        public static FontAwesomeIcon Dev => new FontAwesomeIcon("dev");
        public static FontAwesomeIcon Deviantart => new FontAwesomeIcon("deviantart");
        public static FontAwesomeIcon Dharmachakra => new FontAwesomeIcon("dharmachakra");
        public static FontAwesomeIcon Dhl => new FontAwesomeIcon("dhl");
        public static FontAwesomeIcon Diagnoses => new FontAwesomeIcon("diagnoses");
        public static FontAwesomeIcon Diaspora => new FontAwesomeIcon("diaspora");
        public static FontAwesomeIcon Dice => new FontAwesomeIcon("dice");
        public static FontAwesomeIcon DiceD20 => new FontAwesomeIcon("dice-d20");
        public static FontAwesomeIcon DiceD6 => new FontAwesomeIcon("dice-d6");
        public static FontAwesomeIcon DiceFive => new FontAwesomeIcon("dice-five");
        public static FontAwesomeIcon DiceFour => new FontAwesomeIcon("dice-four");
        public static FontAwesomeIcon DiceOne => new FontAwesomeIcon("dice-one");
        public static FontAwesomeIcon DiceSix => new FontAwesomeIcon("dice-six");
        public static FontAwesomeIcon DiceThree => new FontAwesomeIcon("dice-three");
        public static FontAwesomeIcon DiceTwo => new FontAwesomeIcon("dice-two");
        public static FontAwesomeIcon Digg => new FontAwesomeIcon("digg");
        public static FontAwesomeIcon DigitalOcean => new FontAwesomeIcon("digital-ocean");
        public static FontAwesomeIcon DigitalTachograph => new FontAwesomeIcon("digital-tachograph");
        public static FontAwesomeIcon Directions => new FontAwesomeIcon("directions");
        public static FontAwesomeIcon Discord => new FontAwesomeIcon("discord");
        public static FontAwesomeIcon Discourse => new FontAwesomeIcon("discourse");
        public static FontAwesomeIcon Divide => new FontAwesomeIcon("divide");
        public static FontAwesomeIcon Dizzy => new FontAwesomeIcon("dizzy");
        public static FontAwesomeIcon Dna => new FontAwesomeIcon("dna");
        public static FontAwesomeIcon Dochub => new FontAwesomeIcon("dochub");
        public static FontAwesomeIcon Docker => new FontAwesomeIcon("docker");
        public static FontAwesomeIcon Dog => new FontAwesomeIcon("dog");
        public static FontAwesomeIcon DollarSign => new FontAwesomeIcon("dollar-sign");
        public static FontAwesomeIcon Dolly => new FontAwesomeIcon("dolly");
        public static FontAwesomeIcon DollyFlatbed => new FontAwesomeIcon("dolly-flatbed");
        public static FontAwesomeIcon Donate => new FontAwesomeIcon("donate");
        public static FontAwesomeIcon DoorClosed => new FontAwesomeIcon("door-closed");
        public static FontAwesomeIcon DoorOpen => new FontAwesomeIcon("door-open");
        public static FontAwesomeIcon DotCircle => new FontAwesomeIcon("dot-circle");
        public static FontAwesomeIcon Dove => new FontAwesomeIcon("dove");
        public static FontAwesomeIcon Download => new FontAwesomeIcon("download");
        public static FontAwesomeIcon Draft2digital => new FontAwesomeIcon("draft2digital");
        public static FontAwesomeIcon DraftingCompass => new FontAwesomeIcon("drafting-compass");
        public static FontAwesomeIcon Dragon => new FontAwesomeIcon("dragon");
        public static FontAwesomeIcon DrawPolygon => new FontAwesomeIcon("draw-polygon");
        public static FontAwesomeIcon Dribbble => new FontAwesomeIcon("dribbble");
        public static FontAwesomeIcon DribbbleSquare => new FontAwesomeIcon("dribbble-square");
        public static FontAwesomeIcon Dropbox => new FontAwesomeIcon("dropbox");
        public static FontAwesomeIcon Drum => new FontAwesomeIcon("drum");
        public static FontAwesomeIcon DrumSteelpan => new FontAwesomeIcon("drum-steelpan");
        public static FontAwesomeIcon DrumstickBite => new FontAwesomeIcon("drumstick-bite");
        public static FontAwesomeIcon Drupal => new FontAwesomeIcon("drupal");
        public static FontAwesomeIcon Dumbbell => new FontAwesomeIcon("dumbbell");
        public static FontAwesomeIcon Dumpster => new FontAwesomeIcon("dumpster");
        public static FontAwesomeIcon DumpsterFire => new FontAwesomeIcon("dumpster-fire");
        public static FontAwesomeIcon Dungeon => new FontAwesomeIcon("dungeon");
        public static FontAwesomeIcon Dyalog => new FontAwesomeIcon("dyalog");
        public static FontAwesomeIcon Earlybirds => new FontAwesomeIcon("earlybirds");
        public static FontAwesomeIcon Ebay => new FontAwesomeIcon("ebay");
        public static FontAwesomeIcon Edge => new FontAwesomeIcon("edge");
        public static FontAwesomeIcon Edit => new FontAwesomeIcon("edit");
        public static FontAwesomeIcon Egg => new FontAwesomeIcon("egg");
        public static FontAwesomeIcon Eject => new FontAwesomeIcon("eject");
        public static FontAwesomeIcon Elementor => new FontAwesomeIcon("elementor");
        public static FontAwesomeIcon EllipsisH => new FontAwesomeIcon("ellipsis-h");
        public static FontAwesomeIcon EllipsisV => new FontAwesomeIcon("ellipsis-v");
        public static FontAwesomeIcon Ello => new FontAwesomeIcon("ello");
        public static FontAwesomeIcon Ember => new FontAwesomeIcon("ember");
        public static FontAwesomeIcon Empire => new FontAwesomeIcon("empire");
        public static FontAwesomeIcon Envelope => new FontAwesomeIcon("envelope");
        public static FontAwesomeIcon EnvelopeOpen => new FontAwesomeIcon("envelope-open");
        public static FontAwesomeIcon EnvelopeOpenText => new FontAwesomeIcon("envelope-open-text");
        public static FontAwesomeIcon EnvelopeSquare => new FontAwesomeIcon("envelope-square");
        public static FontAwesomeIcon Envira => new FontAwesomeIcon("envira");
        public static FontAwesomeIcon Equals => new FontAwesomeIcon("equals");
        public static FontAwesomeIcon Eraser => new FontAwesomeIcon("eraser");
        public static FontAwesomeIcon Erlang => new FontAwesomeIcon("erlang");
        public static FontAwesomeIcon Ethereum => new FontAwesomeIcon("ethereum");
        public static FontAwesomeIcon Ethernet => new FontAwesomeIcon("ethernet");
        public static FontAwesomeIcon Etsy => new FontAwesomeIcon("etsy");
        public static FontAwesomeIcon EuroSign => new FontAwesomeIcon("euro-sign");
        public static FontAwesomeIcon Evernote => new FontAwesomeIcon("evernote");
        public static FontAwesomeIcon ExchangeAlt => new FontAwesomeIcon("exchange-alt");
        public static FontAwesomeIcon Exclamation => new FontAwesomeIcon("exclamation");
        public static FontAwesomeIcon ExclamationCircle => new FontAwesomeIcon("exclamation-circle");
        public static FontAwesomeIcon ExclamationTriangle => new FontAwesomeIcon("exclamation-triangle");
        public static FontAwesomeIcon Expand => new FontAwesomeIcon("expand");
        public static FontAwesomeIcon ExpandArrowsAlt => new FontAwesomeIcon("expand-arrows-alt");
        public static FontAwesomeIcon Expeditedssl => new FontAwesomeIcon("expeditedssl");
        public static FontAwesomeIcon ExternalLinkAlt => new FontAwesomeIcon("external-link-alt");
        public static FontAwesomeIcon ExternalLinkSquareAlt => new FontAwesomeIcon("external-link-square-alt");
        public static FontAwesomeIcon Eye => new FontAwesomeIcon("eye");
        public static FontAwesomeIcon EyeDropper => new FontAwesomeIcon("eye-dropper");
        public static FontAwesomeIcon EyeSlash => new FontAwesomeIcon("eye-slash");
        public static FontAwesomeIcon Facebook => new FontAwesomeIcon("facebook");
        public static FontAwesomeIcon FacebookF => new FontAwesomeIcon("facebook-f");
        public static FontAwesomeIcon FacebookMessenger => new FontAwesomeIcon("facebook-messenger");
        public static FontAwesomeIcon FacebookSquare => new FontAwesomeIcon("facebook-square");
        public static FontAwesomeIcon FantasyFlightGames => new FontAwesomeIcon("fantasy-flight-games");
        public static FontAwesomeIcon FastBackward => new FontAwesomeIcon("fast-backward");
        public static FontAwesomeIcon FastForward => new FontAwesomeIcon("fast-forward");
        public static FontAwesomeIcon Fax => new FontAwesomeIcon("fax");
        public static FontAwesomeIcon Feather => new FontAwesomeIcon("feather");
        public static FontAwesomeIcon FeatherAlt => new FontAwesomeIcon("feather-alt");
        public static FontAwesomeIcon Fedex => new FontAwesomeIcon("fedex");
        public static FontAwesomeIcon Fedora => new FontAwesomeIcon("fedora");
        public static FontAwesomeIcon Female => new FontAwesomeIcon("female");
        public static FontAwesomeIcon FighterJet => new FontAwesomeIcon("fighter-jet");
        public static FontAwesomeIcon Figma => new FontAwesomeIcon("figma");
        public static FontAwesomeIcon File => new FontAwesomeIcon("file");
        public static FontAwesomeIcon FileAlt => new FontAwesomeIcon("file-alt");
        public static FontAwesomeIcon FileArchive => new FontAwesomeIcon("file-archive");
        public static FontAwesomeIcon FileAudio => new FontAwesomeIcon("file-audio");
        public static FontAwesomeIcon FileCode => new FontAwesomeIcon("file-code");
        public static FontAwesomeIcon FileContract => new FontAwesomeIcon("file-contract");
        public static FontAwesomeIcon FileCsv => new FontAwesomeIcon("file-csv");
        public static FontAwesomeIcon FileDownload => new FontAwesomeIcon("file-download");
        public static FontAwesomeIcon FileExcel => new FontAwesomeIcon("file-excel");
        public static FontAwesomeIcon FileExport => new FontAwesomeIcon("file-export");
        public static FontAwesomeIcon FileImage => new FontAwesomeIcon("file-image");
        public static FontAwesomeIcon FileImport => new FontAwesomeIcon("file-import");
        public static FontAwesomeIcon FileInvoice => new FontAwesomeIcon("file-invoice");
        public static FontAwesomeIcon FileInvoiceDollar => new FontAwesomeIcon("file-invoice-dollar");
        public static FontAwesomeIcon FileMedical => new FontAwesomeIcon("file-medical");
        public static FontAwesomeIcon FileMedicalAlt => new FontAwesomeIcon("file-medical-alt");
        public static FontAwesomeIcon FilePdf => new FontAwesomeIcon("file-pdf");
        public static FontAwesomeIcon FilePowerpoint => new FontAwesomeIcon("file-powerpoint");
        public static FontAwesomeIcon FilePrescription => new FontAwesomeIcon("file-prescription");
        public static FontAwesomeIcon FileSignature => new FontAwesomeIcon("file-signature");
        public static FontAwesomeIcon FileUpload => new FontAwesomeIcon("file-upload");
        public static FontAwesomeIcon FileVideo => new FontAwesomeIcon("file-video");
        public static FontAwesomeIcon FileWord => new FontAwesomeIcon("file-word");
        public static FontAwesomeIcon Fill => new FontAwesomeIcon("fill");
        public static FontAwesomeIcon FillDrip => new FontAwesomeIcon("fill-drip");
        public static FontAwesomeIcon Film => new FontAwesomeIcon("film");
        public static FontAwesomeIcon Filter => new FontAwesomeIcon("filter");
        public static FontAwesomeIcon Fingerprint => new FontAwesomeIcon("fingerprint");
        public static FontAwesomeIcon Fire => new FontAwesomeIcon("fire");
        public static FontAwesomeIcon FireAlt => new FontAwesomeIcon("fire-alt");
        public static FontAwesomeIcon FireExtinguisher => new FontAwesomeIcon("fire-extinguisher");
        public static FontAwesomeIcon Firefox => new FontAwesomeIcon("firefox");
        public static FontAwesomeIcon FirstAid => new FontAwesomeIcon("first-aid");
        public static FontAwesomeIcon FirstOrder => new FontAwesomeIcon("first-order");
        public static FontAwesomeIcon FirstOrderAlt => new FontAwesomeIcon("first-order-alt");
        public static FontAwesomeIcon Firstdraft => new FontAwesomeIcon("firstdraft");
        public static FontAwesomeIcon Fish => new FontAwesomeIcon("fish");
        public static FontAwesomeIcon FistRaised => new FontAwesomeIcon("fist-raised");
        public static FontAwesomeIcon Flag => new FontAwesomeIcon("flag");
        public static FontAwesomeIcon FlagCheckered => new FontAwesomeIcon("flag-checkered");
        public static FontAwesomeIcon FlagUsa => new FontAwesomeIcon("flag-usa");
        public static FontAwesomeIcon Flask => new FontAwesomeIcon("flask");
        public static FontAwesomeIcon Flickr => new FontAwesomeIcon("flickr");
        public static FontAwesomeIcon Flipboard => new FontAwesomeIcon("flipboard");
        public static FontAwesomeIcon Flushed => new FontAwesomeIcon("flushed");
        public static FontAwesomeIcon Fly => new FontAwesomeIcon("fly");
        public static FontAwesomeIcon Folder => new FontAwesomeIcon("folder");
        public static FontAwesomeIcon FolderMinus => new FontAwesomeIcon("folder-minus");
        public static FontAwesomeIcon FolderOpen => new FontAwesomeIcon("folder-open");
        public static FontAwesomeIcon FolderPlus => new FontAwesomeIcon("folder-plus");
        public static FontAwesomeIcon Font => new FontAwesomeIcon("font");
        public static FontAwesomeIcon FontAwesome => new FontAwesomeIcon("font-awesome");
        public static FontAwesomeIcon FontAwesomeAlt => new FontAwesomeIcon("font-awesome-alt");
        public static FontAwesomeIcon FontAwesomeFlag => new FontAwesomeIcon("font-awesome-flag");
        public static FontAwesomeIcon FontAwesomeLogoFull => new FontAwesomeIcon("font-awesome-logo-full");
        public static FontAwesomeIcon Fonticons => new FontAwesomeIcon("fonticons");
        public static FontAwesomeIcon FonticonsFi => new FontAwesomeIcon("fonticons-fi");
        public static FontAwesomeIcon FootballBall => new FontAwesomeIcon("football-ball");
        public static FontAwesomeIcon FortAwesome => new FontAwesomeIcon("fort-awesome");
        public static FontAwesomeIcon FortAwesomeAlt => new FontAwesomeIcon("fort-awesome-alt");
        public static FontAwesomeIcon Forumbee => new FontAwesomeIcon("forumbee");
        public static FontAwesomeIcon Forward => new FontAwesomeIcon("forward");
        public static FontAwesomeIcon Foursquare => new FontAwesomeIcon("foursquare");
        public static FontAwesomeIcon FreeCodeCamp => new FontAwesomeIcon("free-code-camp");
        public static FontAwesomeIcon Freebsd => new FontAwesomeIcon("freebsd");
        public static FontAwesomeIcon Frog => new FontAwesomeIcon("frog");
        public static FontAwesomeIcon Frown => new FontAwesomeIcon("frown");
        public static FontAwesomeIcon FrownOpen => new FontAwesomeIcon("frown-open");
        public static FontAwesomeIcon Fulcrum => new FontAwesomeIcon("fulcrum");
        public static FontAwesomeIcon FunnelDollar => new FontAwesomeIcon("funnel-dollar");
        public static FontAwesomeIcon Futbol => new FontAwesomeIcon("futbol");
        public static FontAwesomeIcon GalacticRepublic => new FontAwesomeIcon("galactic-republic");
        public static FontAwesomeIcon GalacticSenate => new FontAwesomeIcon("galactic-senate");
        public static FontAwesomeIcon Gamepad => new FontAwesomeIcon("gamepad");
        public static FontAwesomeIcon GasPump => new FontAwesomeIcon("gas-pump");
        public static FontAwesomeIcon Gavel => new FontAwesomeIcon("gavel");
        public static FontAwesomeIcon Gem => new FontAwesomeIcon("gem");
        public static FontAwesomeIcon Genderless => new FontAwesomeIcon("genderless");
        public static FontAwesomeIcon GetPocket => new FontAwesomeIcon("get-pocket");
        public static FontAwesomeIcon Gg => new FontAwesomeIcon("gg");
        public static FontAwesomeIcon GgCircle => new FontAwesomeIcon("gg-circle");
        public static FontAwesomeIcon Ghost => new FontAwesomeIcon("ghost");
        public static FontAwesomeIcon Gift => new FontAwesomeIcon("gift");
        public static FontAwesomeIcon Gifts => new FontAwesomeIcon("gifts");
        public static FontAwesomeIcon Git => new FontAwesomeIcon("git");
        public static FontAwesomeIcon GitSquare => new FontAwesomeIcon("git-square");
        public static FontAwesomeIcon Github => new FontAwesomeIcon("github");
        public static FontAwesomeIcon GithubAlt => new FontAwesomeIcon("github-alt");
        public static FontAwesomeIcon GithubSquare => new FontAwesomeIcon("github-square");
        public static FontAwesomeIcon Gitkraken => new FontAwesomeIcon("gitkraken");
        public static FontAwesomeIcon Gitlab => new FontAwesomeIcon("gitlab");
        public static FontAwesomeIcon Gitter => new FontAwesomeIcon("gitter");
        public static FontAwesomeIcon GlassCheers => new FontAwesomeIcon("glass-cheers");
        public static FontAwesomeIcon GlassMartini => new FontAwesomeIcon("glass-martini");
        public static FontAwesomeIcon GlassMartiniAlt => new FontAwesomeIcon("glass-martini-alt");
        public static FontAwesomeIcon GlassWhiskey => new FontAwesomeIcon("glass-whiskey");
        public static FontAwesomeIcon Glasses => new FontAwesomeIcon("glasses");
        public static FontAwesomeIcon Glide => new FontAwesomeIcon("glide");
        public static FontAwesomeIcon GlideG => new FontAwesomeIcon("glide-g");
        public static FontAwesomeIcon Globe => new FontAwesomeIcon("globe");
        public static FontAwesomeIcon GlobeAfrica => new FontAwesomeIcon("globe-africa");
        public static FontAwesomeIcon GlobeAmericas => new FontAwesomeIcon("globe-americas");
        public static FontAwesomeIcon GlobeAsia => new FontAwesomeIcon("globe-asia");
        public static FontAwesomeIcon GlobeEurope => new FontAwesomeIcon("globe-europe");
        public static FontAwesomeIcon Gofore => new FontAwesomeIcon("gofore");
        public static FontAwesomeIcon GolfBall => new FontAwesomeIcon("golf-ball");
        public static FontAwesomeIcon Goodreads => new FontAwesomeIcon("goodreads");
        public static FontAwesomeIcon GoodreadsG => new FontAwesomeIcon("goodreads-g");
        public static FontAwesomeIcon Google => new FontAwesomeIcon("google");
        public static FontAwesomeIcon GoogleDrive => new FontAwesomeIcon("google-drive");
        public static FontAwesomeIcon GooglePlay => new FontAwesomeIcon("google-play");
        public static FontAwesomeIcon GooglePlus => new FontAwesomeIcon("google-plus");
        public static FontAwesomeIcon GooglePlusG => new FontAwesomeIcon("google-plus-g");
        public static FontAwesomeIcon GooglePlusSquare => new FontAwesomeIcon("google-plus-square");
        public static FontAwesomeIcon GoogleWallet => new FontAwesomeIcon("google-wallet");
        public static FontAwesomeIcon Gopuram => new FontAwesomeIcon("gopuram");
        public static FontAwesomeIcon GraduationCap => new FontAwesomeIcon("graduation-cap");
        public static FontAwesomeIcon Gratipay => new FontAwesomeIcon("gratipay");
        public static FontAwesomeIcon Grav => new FontAwesomeIcon("grav");
        public static FontAwesomeIcon GreaterThan => new FontAwesomeIcon("greater-than");
        public static FontAwesomeIcon GreaterThanEqual => new FontAwesomeIcon("greater-than-equal");
        public static FontAwesomeIcon Grimace => new FontAwesomeIcon("grimace");
        public static FontAwesomeIcon Grin => new FontAwesomeIcon("grin");
        public static FontAwesomeIcon GrinAlt => new FontAwesomeIcon("grin-alt");
        public static FontAwesomeIcon GrinBeam => new FontAwesomeIcon("grin-beam");
        public static FontAwesomeIcon GrinBeamSweat => new FontAwesomeIcon("grin-beam-sweat");
        public static FontAwesomeIcon GrinHearts => new FontAwesomeIcon("grin-hearts");
        public static FontAwesomeIcon GrinSquint => new FontAwesomeIcon("grin-squint");
        public static FontAwesomeIcon GrinSquintTears => new FontAwesomeIcon("grin-squint-tears");
        public static FontAwesomeIcon GrinStars => new FontAwesomeIcon("grin-stars");
        public static FontAwesomeIcon GrinTears => new FontAwesomeIcon("grin-tears");
        public static FontAwesomeIcon GrinTongue => new FontAwesomeIcon("grin-tongue");
        public static FontAwesomeIcon GrinTongueSquint => new FontAwesomeIcon("grin-tongue-squint");
        public static FontAwesomeIcon GrinTongueWink => new FontAwesomeIcon("grin-tongue-wink");
        public static FontAwesomeIcon GrinWink => new FontAwesomeIcon("grin-wink");
        public static FontAwesomeIcon GripHorizontal => new FontAwesomeIcon("grip-horizontal");
        public static FontAwesomeIcon GripLines => new FontAwesomeIcon("grip-lines");
        public static FontAwesomeIcon GripLinesVertical => new FontAwesomeIcon("grip-lines-vertical");
        public static FontAwesomeIcon GripVertical => new FontAwesomeIcon("grip-vertical");
        public static FontAwesomeIcon Gripfire => new FontAwesomeIcon("gripfire");
        public static FontAwesomeIcon Grunt => new FontAwesomeIcon("grunt");
        public static FontAwesomeIcon Guitar => new FontAwesomeIcon("guitar");
        public static FontAwesomeIcon Gulp => new FontAwesomeIcon("gulp");
        public static FontAwesomeIcon HSquare => new FontAwesomeIcon("h-square");
        public static FontAwesomeIcon HackerNews => new FontAwesomeIcon("hacker-news");
        public static FontAwesomeIcon HackerNewsSquare => new FontAwesomeIcon("hacker-news-square");
        public static FontAwesomeIcon Hackerrank => new FontAwesomeIcon("hackerrank");
        public static FontAwesomeIcon Hamburger => new FontAwesomeIcon("hamburger");
        public static FontAwesomeIcon Hammer => new FontAwesomeIcon("hammer");
        public static FontAwesomeIcon Hamsa => new FontAwesomeIcon("hamsa");
        public static FontAwesomeIcon HandHolding => new FontAwesomeIcon("hand-holding");
        public static FontAwesomeIcon HandHoldingHeart => new FontAwesomeIcon("hand-holding-heart");
        public static FontAwesomeIcon HandHoldingUsd => new FontAwesomeIcon("hand-holding-usd");
        public static FontAwesomeIcon HandLizard => new FontAwesomeIcon("hand-lizard");
        public static FontAwesomeIcon HandMiddleFinger => new FontAwesomeIcon("hand-middle-finger");
        public static FontAwesomeIcon HandPaper => new FontAwesomeIcon("hand-paper");
        public static FontAwesomeIcon HandPeace => new FontAwesomeIcon("hand-peace");
        public static FontAwesomeIcon HandPointDown => new FontAwesomeIcon("hand-point-down");
        public static FontAwesomeIcon HandPointLeft => new FontAwesomeIcon("hand-point-left");
        public static FontAwesomeIcon HandPointRight => new FontAwesomeIcon("hand-point-right");
        public static FontAwesomeIcon HandPointUp => new FontAwesomeIcon("hand-point-up");
        public static FontAwesomeIcon HandPointer => new FontAwesomeIcon("hand-pointer");
        public static FontAwesomeIcon HandRock => new FontAwesomeIcon("hand-rock");
        public static FontAwesomeIcon HandScissors => new FontAwesomeIcon("hand-scissors");
        public static FontAwesomeIcon HandSpock => new FontAwesomeIcon("hand-spock");
        public static FontAwesomeIcon Hands => new FontAwesomeIcon("hands");
        public static FontAwesomeIcon HandsHelping => new FontAwesomeIcon("hands-helping");
        public static FontAwesomeIcon Handshake => new FontAwesomeIcon("handshake");
        public static FontAwesomeIcon Hanukiah => new FontAwesomeIcon("hanukiah");
        public static FontAwesomeIcon HardHat => new FontAwesomeIcon("hard-hat");
        public static FontAwesomeIcon Hashtag => new FontAwesomeIcon("hashtag");
        public static FontAwesomeIcon HatWizard => new FontAwesomeIcon("hat-wizard");
        public static FontAwesomeIcon Haykal => new FontAwesomeIcon("haykal");
        public static FontAwesomeIcon Hdd => new FontAwesomeIcon("hdd");
        public static FontAwesomeIcon Heading => new FontAwesomeIcon("heading");
        public static FontAwesomeIcon Headphones => new FontAwesomeIcon("headphones");
        public static FontAwesomeIcon HeadphonesAlt => new FontAwesomeIcon("headphones-alt");
        public static FontAwesomeIcon Headset => new FontAwesomeIcon("headset");
        public static FontAwesomeIcon Heart => new FontAwesomeIcon("heart");
        public static FontAwesomeIcon HeartBroken => new FontAwesomeIcon("heart-broken");
        public static FontAwesomeIcon Heartbeat => new FontAwesomeIcon("heartbeat");
        public static FontAwesomeIcon Helicopter => new FontAwesomeIcon("helicopter");
        public static FontAwesomeIcon Highlighter => new FontAwesomeIcon("highlighter");
        public static FontAwesomeIcon Hiking => new FontAwesomeIcon("hiking");
        public static FontAwesomeIcon Hippo => new FontAwesomeIcon("hippo");
        public static FontAwesomeIcon Hips => new FontAwesomeIcon("hips");
        public static FontAwesomeIcon HireAHelper => new FontAwesomeIcon("hire-a-helper");
        public static FontAwesomeIcon History => new FontAwesomeIcon("history");
        public static FontAwesomeIcon HockeyPuck => new FontAwesomeIcon("hockey-puck");
        public static FontAwesomeIcon HollyBerry => new FontAwesomeIcon("holly-berry");
        public static FontAwesomeIcon Home => new FontAwesomeIcon("home");
        public static FontAwesomeIcon Hooli => new FontAwesomeIcon("hooli");
        public static FontAwesomeIcon Hornbill => new FontAwesomeIcon("hornbill");
        public static FontAwesomeIcon Horse => new FontAwesomeIcon("horse");
        public static FontAwesomeIcon HorseHead => new FontAwesomeIcon("horse-head");
        public static FontAwesomeIcon Hospital => new FontAwesomeIcon("hospital");
        public static FontAwesomeIcon HospitalAlt => new FontAwesomeIcon("hospital-alt");
        public static FontAwesomeIcon HospitalSymbol => new FontAwesomeIcon("hospital-symbol");
        public static FontAwesomeIcon HotTub => new FontAwesomeIcon("hot-tub");
        public static FontAwesomeIcon Hotdog => new FontAwesomeIcon("hotdog");
        public static FontAwesomeIcon Hotel => new FontAwesomeIcon("hotel");
        public static FontAwesomeIcon Hotjar => new FontAwesomeIcon("hotjar");
        public static FontAwesomeIcon Hourglass => new FontAwesomeIcon("hourglass");
        public static FontAwesomeIcon HourglassEnd => new FontAwesomeIcon("hourglass-end");
        public static FontAwesomeIcon HourglassHalf => new FontAwesomeIcon("hourglass-half");
        public static FontAwesomeIcon HourglassStart => new FontAwesomeIcon("hourglass-start");
        public static FontAwesomeIcon HouseDamage => new FontAwesomeIcon("house-damage");
        public static FontAwesomeIcon Houzz => new FontAwesomeIcon("houzz");
        public static FontAwesomeIcon Hryvnia => new FontAwesomeIcon("hryvnia");
        public static FontAwesomeIcon Html5 => new FontAwesomeIcon("html5");
        public static FontAwesomeIcon Hubspot => new FontAwesomeIcon("hubspot");
        public static FontAwesomeIcon ICursor => new FontAwesomeIcon("i-cursor");
        public static FontAwesomeIcon IceCream => new FontAwesomeIcon("ice-cream");
        public static FontAwesomeIcon Icicles => new FontAwesomeIcon("icicles");
        public static FontAwesomeIcon IdBadge => new FontAwesomeIcon("id-badge");
        public static FontAwesomeIcon IdCard => new FontAwesomeIcon("id-card");
        public static FontAwesomeIcon IdCardAlt => new FontAwesomeIcon("id-card-alt");
        public static FontAwesomeIcon Igloo => new FontAwesomeIcon("igloo");
        public static FontAwesomeIcon Image => new FontAwesomeIcon("image");
        public static FontAwesomeIcon Images => new FontAwesomeIcon("images");
        public static FontAwesomeIcon Imdb => new FontAwesomeIcon("imdb");
        public static FontAwesomeIcon Inbox => new FontAwesomeIcon("inbox");
        public static FontAwesomeIcon Indent => new FontAwesomeIcon("indent");
        public static FontAwesomeIcon Industry => new FontAwesomeIcon("industry");
        public static FontAwesomeIcon Infinity => new FontAwesomeIcon("infinity");
        public static FontAwesomeIcon Info => new FontAwesomeIcon("info");
        public static FontAwesomeIcon InfoCircle => new FontAwesomeIcon("info-circle");
        public static FontAwesomeIcon Instagram => new FontAwesomeIcon("instagram");
        public static FontAwesomeIcon Intercom => new FontAwesomeIcon("intercom");
        public static FontAwesomeIcon InternetExplorer => new FontAwesomeIcon("internet-explorer");
        public static FontAwesomeIcon Invision => new FontAwesomeIcon("invision");
        public static FontAwesomeIcon Ioxhost => new FontAwesomeIcon("ioxhost");
        public static FontAwesomeIcon Italic => new FontAwesomeIcon("italic");
        public static FontAwesomeIcon ItchIo => new FontAwesomeIcon("itch-io");
        public static FontAwesomeIcon Itunes => new FontAwesomeIcon("itunes");
        public static FontAwesomeIcon ItunesNote => new FontAwesomeIcon("itunes-note");
        public static FontAwesomeIcon Java => new FontAwesomeIcon("java");
        public static FontAwesomeIcon Jedi => new FontAwesomeIcon("jedi");
        public static FontAwesomeIcon JediOrder => new FontAwesomeIcon("jedi-order");
        public static FontAwesomeIcon Jenkins => new FontAwesomeIcon("jenkins");
        public static FontAwesomeIcon Jira => new FontAwesomeIcon("jira");
        public static FontAwesomeIcon Joget => new FontAwesomeIcon("joget");
        public static FontAwesomeIcon Joint => new FontAwesomeIcon("joint");
        public static FontAwesomeIcon Joomla => new FontAwesomeIcon("joomla");
        public static FontAwesomeIcon JournalWhills => new FontAwesomeIcon("journal-whills");
        public static FontAwesomeIcon Js => new FontAwesomeIcon("js");
        public static FontAwesomeIcon JsSquare => new FontAwesomeIcon("js-square");
        public static FontAwesomeIcon Jsfiddle => new FontAwesomeIcon("jsfiddle");
        public static FontAwesomeIcon Kaaba => new FontAwesomeIcon("kaaba");
        public static FontAwesomeIcon Kaggle => new FontAwesomeIcon("kaggle");
        public static FontAwesomeIcon Key => new FontAwesomeIcon("key");
        public static FontAwesomeIcon Keybase => new FontAwesomeIcon("keybase");
        public static FontAwesomeIcon Keyboard => new FontAwesomeIcon("keyboard");
        public static FontAwesomeIcon Keycdn => new FontAwesomeIcon("keycdn");
        public static FontAwesomeIcon Khanda => new FontAwesomeIcon("khanda");
        public static FontAwesomeIcon Kickstarter => new FontAwesomeIcon("kickstarter");
        public static FontAwesomeIcon KickstarterK => new FontAwesomeIcon("kickstarter-k");
        public static FontAwesomeIcon Kiss => new FontAwesomeIcon("kiss");
        public static FontAwesomeIcon KissBeam => new FontAwesomeIcon("kiss-beam");
        public static FontAwesomeIcon KissWinkHeart => new FontAwesomeIcon("kiss-wink-heart");
        public static FontAwesomeIcon KiwiBird => new FontAwesomeIcon("kiwi-bird");
        public static FontAwesomeIcon Korvue => new FontAwesomeIcon("korvue");
        public static FontAwesomeIcon Landmark => new FontAwesomeIcon("landmark");
        public static FontAwesomeIcon Language => new FontAwesomeIcon("language");
        public static FontAwesomeIcon Laptop => new FontAwesomeIcon("laptop");
        public static FontAwesomeIcon LaptopCode => new FontAwesomeIcon("laptop-code");
        public static FontAwesomeIcon LaptopMedical => new FontAwesomeIcon("laptop-medical");
        public static FontAwesomeIcon Laravel => new FontAwesomeIcon("laravel");
        public static FontAwesomeIcon Lastfm => new FontAwesomeIcon("lastfm");
        public static FontAwesomeIcon LastfmSquare => new FontAwesomeIcon("lastfm-square");
        public static FontAwesomeIcon Laugh => new FontAwesomeIcon("laugh");
        public static FontAwesomeIcon LaughBeam => new FontAwesomeIcon("laugh-beam");
        public static FontAwesomeIcon LaughSquint => new FontAwesomeIcon("laugh-squint");
        public static FontAwesomeIcon LaughWink => new FontAwesomeIcon("laugh-wink");
        public static FontAwesomeIcon LayerGroup => new FontAwesomeIcon("layer-group");
        public static FontAwesomeIcon Leaf => new FontAwesomeIcon("leaf");
        public static FontAwesomeIcon Leanpub => new FontAwesomeIcon("leanpub");
        public static FontAwesomeIcon Lemon => new FontAwesomeIcon("lemon");
        public static FontAwesomeIcon Less => new FontAwesomeIcon("less");
        public static FontAwesomeIcon LessThan => new FontAwesomeIcon("less-than");
        public static FontAwesomeIcon LessThanEqual => new FontAwesomeIcon("less-than-equal");
        public static FontAwesomeIcon LevelDownAlt => new FontAwesomeIcon("level-down-alt");
        public static FontAwesomeIcon LevelUpAlt => new FontAwesomeIcon("level-up-alt");
        public static FontAwesomeIcon LifeRing => new FontAwesomeIcon("life-ring");
        public static FontAwesomeIcon Lightbulb => new FontAwesomeIcon("lightbulb");
        public static FontAwesomeIcon Line => new FontAwesomeIcon("line");
        public static FontAwesomeIcon Link => new FontAwesomeIcon("link");
        public static FontAwesomeIcon Linkedin => new FontAwesomeIcon("linkedin");
        public static FontAwesomeIcon LinkedinIn => new FontAwesomeIcon("linkedin-in");
        public static FontAwesomeIcon Linode => new FontAwesomeIcon("linode");
        public static FontAwesomeIcon Linux => new FontAwesomeIcon("linux");
        public static FontAwesomeIcon LiraSign => new FontAwesomeIcon("lira-sign");
        public static FontAwesomeIcon List => new FontAwesomeIcon("list");
        public static FontAwesomeIcon ListAlt => new FontAwesomeIcon("list-alt");
        public static FontAwesomeIcon ListOl => new FontAwesomeIcon("list-ol");
        public static FontAwesomeIcon ListUl => new FontAwesomeIcon("list-ul");
        public static FontAwesomeIcon LocationArrow => new FontAwesomeIcon("location-arrow");
        public static FontAwesomeIcon Lock => new FontAwesomeIcon("lock");
        public static FontAwesomeIcon LockOpen => new FontAwesomeIcon("lock-open");
        public static FontAwesomeIcon LongArrowAltDown => new FontAwesomeIcon("long-arrow-alt-down");
        public static FontAwesomeIcon LongArrowAltLeft => new FontAwesomeIcon("long-arrow-alt-left");
        public static FontAwesomeIcon LongArrowAltRight => new FontAwesomeIcon("long-arrow-alt-right");
        public static FontAwesomeIcon LongArrowAltUp => new FontAwesomeIcon("long-arrow-alt-up");
        public static FontAwesomeIcon LowVision => new FontAwesomeIcon("low-vision");
        public static FontAwesomeIcon LuggageCart => new FontAwesomeIcon("luggage-cart");
        public static FontAwesomeIcon Lyft => new FontAwesomeIcon("lyft");
        public static FontAwesomeIcon Magento => new FontAwesomeIcon("magento");
        public static FontAwesomeIcon Magic => new FontAwesomeIcon("magic");
        public static FontAwesomeIcon Magnet => new FontAwesomeIcon("magnet");
        public static FontAwesomeIcon MailBulk => new FontAwesomeIcon("mail-bulk");
        public static FontAwesomeIcon Mailchimp => new FontAwesomeIcon("mailchimp");
        public static FontAwesomeIcon Male => new FontAwesomeIcon("male");
        public static FontAwesomeIcon Mandalorian => new FontAwesomeIcon("mandalorian");
        public static FontAwesomeIcon Map => new FontAwesomeIcon("map");
        public static FontAwesomeIcon MapMarked => new FontAwesomeIcon("map-marked");
        public static FontAwesomeIcon MapMarkedAlt => new FontAwesomeIcon("map-marked-alt");
        public static FontAwesomeIcon MapMarker => new FontAwesomeIcon("map-marker");
        public static FontAwesomeIcon MapMarkerAlt => new FontAwesomeIcon("map-marker-alt");
        public static FontAwesomeIcon MapPin => new FontAwesomeIcon("map-pin");
        public static FontAwesomeIcon MapSigns => new FontAwesomeIcon("map-signs");
        public static FontAwesomeIcon Markdown => new FontAwesomeIcon("markdown");
        public static FontAwesomeIcon Marker => new FontAwesomeIcon("marker");
        public static FontAwesomeIcon Mars => new FontAwesomeIcon("mars");
        public static FontAwesomeIcon MarsDouble => new FontAwesomeIcon("mars-double");
        public static FontAwesomeIcon MarsStroke => new FontAwesomeIcon("mars-stroke");
        public static FontAwesomeIcon MarsStrokeH => new FontAwesomeIcon("mars-stroke-h");
        public static FontAwesomeIcon MarsStrokeV => new FontAwesomeIcon("mars-stroke-v");
        public static FontAwesomeIcon Mask => new FontAwesomeIcon("mask");
        public static FontAwesomeIcon Mastodon => new FontAwesomeIcon("mastodon");
        public static FontAwesomeIcon Maxcdn => new FontAwesomeIcon("maxcdn");
        public static FontAwesomeIcon Medal => new FontAwesomeIcon("medal");
        public static FontAwesomeIcon Medapps => new FontAwesomeIcon("medapps");
        public static FontAwesomeIcon Medium => new FontAwesomeIcon("medium");
        public static FontAwesomeIcon MediumM => new FontAwesomeIcon("medium-m");
        public static FontAwesomeIcon Medkit => new FontAwesomeIcon("medkit");
        public static FontAwesomeIcon Medrt => new FontAwesomeIcon("medrt");
        public static FontAwesomeIcon Meetup => new FontAwesomeIcon("meetup");
        public static FontAwesomeIcon Megaport => new FontAwesomeIcon("megaport");
        public static FontAwesomeIcon Meh => new FontAwesomeIcon("meh");
        public static FontAwesomeIcon MehBlank => new FontAwesomeIcon("meh-blank");
        public static FontAwesomeIcon MehRollingEyes => new FontAwesomeIcon("meh-rolling-eyes");
        public static FontAwesomeIcon Memory => new FontAwesomeIcon("memory");
        public static FontAwesomeIcon Mendeley => new FontAwesomeIcon("mendeley");
        public static FontAwesomeIcon Menorah => new FontAwesomeIcon("menorah");
        public static FontAwesomeIcon Mercury => new FontAwesomeIcon("mercury");
        public static FontAwesomeIcon Meteor => new FontAwesomeIcon("meteor");
        public static FontAwesomeIcon Microchip => new FontAwesomeIcon("microchip");
        public static FontAwesomeIcon Microphone => new FontAwesomeIcon("microphone");
        public static FontAwesomeIcon MicrophoneAlt => new FontAwesomeIcon("microphone-alt");
        public static FontAwesomeIcon MicrophoneAltSlash => new FontAwesomeIcon("microphone-alt-slash");
        public static FontAwesomeIcon MicrophoneSlash => new FontAwesomeIcon("microphone-slash");
        public static FontAwesomeIcon Microscope => new FontAwesomeIcon("microscope");
        public static FontAwesomeIcon Microsoft => new FontAwesomeIcon("microsoft");
        public static FontAwesomeIcon Minus => new FontAwesomeIcon("minus");
        public static FontAwesomeIcon MinusCircle => new FontAwesomeIcon("minus-circle");
        public static FontAwesomeIcon MinusSquare => new FontAwesomeIcon("minus-square");
        public static FontAwesomeIcon Mitten => new FontAwesomeIcon("mitten");
        public static FontAwesomeIcon Mix => new FontAwesomeIcon("mix");
        public static FontAwesomeIcon Mixcloud => new FontAwesomeIcon("mixcloud");
        public static FontAwesomeIcon Mizuni => new FontAwesomeIcon("mizuni");
        public static FontAwesomeIcon Mobile => new FontAwesomeIcon("mobile");
        public static FontAwesomeIcon MobileAlt => new FontAwesomeIcon("mobile-alt");
        public static FontAwesomeIcon Modx => new FontAwesomeIcon("modx");
        public static FontAwesomeIcon Monero => new FontAwesomeIcon("monero");
        public static FontAwesomeIcon MoneyBill => new FontAwesomeIcon("money-bill");
        public static FontAwesomeIcon MoneyBillAlt => new FontAwesomeIcon("money-bill-alt");
        public static FontAwesomeIcon MoneyBillWave => new FontAwesomeIcon("money-bill-wave");
        public static FontAwesomeIcon MoneyBillWaveAlt => new FontAwesomeIcon("money-bill-wave-alt");
        public static FontAwesomeIcon MoneyCheck => new FontAwesomeIcon("money-check");
        public static FontAwesomeIcon MoneyCheckAlt => new FontAwesomeIcon("money-check-alt");
        public static FontAwesomeIcon Monument => new FontAwesomeIcon("monument");
        public static FontAwesomeIcon Moon => new FontAwesomeIcon("moon");
        public static FontAwesomeIcon MortarPestle => new FontAwesomeIcon("mortar-pestle");
        public static FontAwesomeIcon Mosque => new FontAwesomeIcon("mosque");
        public static FontAwesomeIcon Motorcycle => new FontAwesomeIcon("motorcycle");
        public static FontAwesomeIcon Mountain => new FontAwesomeIcon("mountain");
        public static FontAwesomeIcon MousePointer => new FontAwesomeIcon("mouse-pointer");
        public static FontAwesomeIcon MugHot => new FontAwesomeIcon("mug-hot");
        public static FontAwesomeIcon Music => new FontAwesomeIcon("music");
        public static FontAwesomeIcon Napster => new FontAwesomeIcon("napster");
        public static FontAwesomeIcon Neos => new FontAwesomeIcon("neos");
        public static FontAwesomeIcon NetworkWired => new FontAwesomeIcon("network-wired");
        public static FontAwesomeIcon Neuter => new FontAwesomeIcon("neuter");
        public static FontAwesomeIcon Newspaper => new FontAwesomeIcon("newspaper");
        public static FontAwesomeIcon Nimblr => new FontAwesomeIcon("nimblr");
        public static FontAwesomeIcon NintendoSwitch => new FontAwesomeIcon("nintendo-switch");
        public static FontAwesomeIcon Node => new FontAwesomeIcon("node");
        public static FontAwesomeIcon NodeJs => new FontAwesomeIcon("node-js");
        public static FontAwesomeIcon NotEqual => new FontAwesomeIcon("not-equal");
        public static FontAwesomeIcon NotesMedical => new FontAwesomeIcon("notes-medical");
        public static FontAwesomeIcon Npm => new FontAwesomeIcon("npm");
        public static FontAwesomeIcon Ns8 => new FontAwesomeIcon("ns8");
        public static FontAwesomeIcon Nutritionix => new FontAwesomeIcon("nutritionix");
        public static FontAwesomeIcon ObjectGroup => new FontAwesomeIcon("object-group");
        public static FontAwesomeIcon ObjectUngroup => new FontAwesomeIcon("object-ungroup");
        public static FontAwesomeIcon Odnoklassniki => new FontAwesomeIcon("odnoklassniki");
        public static FontAwesomeIcon OdnoklassnikiSquare => new FontAwesomeIcon("odnoklassniki-square");
        public static FontAwesomeIcon OilCan => new FontAwesomeIcon("oil-can");
        public static FontAwesomeIcon OldRepublic => new FontAwesomeIcon("old-republic");
        public static FontAwesomeIcon Om => new FontAwesomeIcon("om");
        public static FontAwesomeIcon Opencart => new FontAwesomeIcon("opencart");
        public static FontAwesomeIcon Openid => new FontAwesomeIcon("openid");
        public static FontAwesomeIcon Opera => new FontAwesomeIcon("opera");
        public static FontAwesomeIcon OptinMonster => new FontAwesomeIcon("optin-monster");
        public static FontAwesomeIcon Osi => new FontAwesomeIcon("osi");
        public static FontAwesomeIcon Otter => new FontAwesomeIcon("otter");
        public static FontAwesomeIcon Outdent => new FontAwesomeIcon("outdent");
        public static FontAwesomeIcon Page4 => new FontAwesomeIcon("page4");
        public static FontAwesomeIcon Pagelines => new FontAwesomeIcon("pagelines");
        public static FontAwesomeIcon Pager => new FontAwesomeIcon("pager");
        public static FontAwesomeIcon PaintBrush => new FontAwesomeIcon("paint-brush");
        public static FontAwesomeIcon PaintRoller => new FontAwesomeIcon("paint-roller");
        public static FontAwesomeIcon Palette => new FontAwesomeIcon("palette");
        public static FontAwesomeIcon Palfed => new FontAwesomeIcon("palfed");
        public static FontAwesomeIcon Pallet => new FontAwesomeIcon("pallet");
        public static FontAwesomeIcon PaperPlane => new FontAwesomeIcon("paper-plane");
        public static FontAwesomeIcon Paperclip => new FontAwesomeIcon("paperclip");
        public static FontAwesomeIcon ParachuteBox => new FontAwesomeIcon("parachute-box");
        public static FontAwesomeIcon Paragraph => new FontAwesomeIcon("paragraph");
        public static FontAwesomeIcon Parking => new FontAwesomeIcon("parking");
        public static FontAwesomeIcon Passport => new FontAwesomeIcon("passport");
        public static FontAwesomeIcon Pastafarianism => new FontAwesomeIcon("pastafarianism");
        public static FontAwesomeIcon Paste => new FontAwesomeIcon("paste");
        public static FontAwesomeIcon Patreon => new FontAwesomeIcon("patreon");
        public static FontAwesomeIcon Pause => new FontAwesomeIcon("pause");
        public static FontAwesomeIcon PauseCircle => new FontAwesomeIcon("pause-circle");
        public static FontAwesomeIcon Paw => new FontAwesomeIcon("paw");
        public static FontAwesomeIcon Paypal => new FontAwesomeIcon("paypal");
        public static FontAwesomeIcon Peace => new FontAwesomeIcon("peace");
        public static FontAwesomeIcon Pen => new FontAwesomeIcon("pen");
        public static FontAwesomeIcon PenAlt => new FontAwesomeIcon("pen-alt");
        public static FontAwesomeIcon PenFancy => new FontAwesomeIcon("pen-fancy");
        public static FontAwesomeIcon PenNib => new FontAwesomeIcon("pen-nib");
        public static FontAwesomeIcon PenSquare => new FontAwesomeIcon("pen-square");
        public static FontAwesomeIcon PencilAlt => new FontAwesomeIcon("pencil-alt");
        public static FontAwesomeIcon PencilRuler => new FontAwesomeIcon("pencil-ruler");
        public static FontAwesomeIcon PennyArcade => new FontAwesomeIcon("penny-arcade");
        public static FontAwesomeIcon PeopleCarry => new FontAwesomeIcon("people-carry");
        public static FontAwesomeIcon PepperHot => new FontAwesomeIcon("pepper-hot");
        public static FontAwesomeIcon Percent => new FontAwesomeIcon("percent");
        public static FontAwesomeIcon Percentage => new FontAwesomeIcon("percentage");
        public static FontAwesomeIcon Periscope => new FontAwesomeIcon("periscope");
        public static FontAwesomeIcon PersonBooth => new FontAwesomeIcon("person-booth");
        public static FontAwesomeIcon Phabricator => new FontAwesomeIcon("phabricator");
        public static FontAwesomeIcon PhoenixFramework => new FontAwesomeIcon("phoenix-framework");
        public static FontAwesomeIcon PhoenixSquadron => new FontAwesomeIcon("phoenix-squadron");
        public static FontAwesomeIcon Phone => new FontAwesomeIcon("phone");
        public static FontAwesomeIcon PhoneSlash => new FontAwesomeIcon("phone-slash");
        public static FontAwesomeIcon PhoneSquare => new FontAwesomeIcon("phone-square");
        public static FontAwesomeIcon PhoneVolume => new FontAwesomeIcon("phone-volume");
        public static FontAwesomeIcon Php => new FontAwesomeIcon("php");
        public static FontAwesomeIcon PiedPiper => new FontAwesomeIcon("pied-piper");
        public static FontAwesomeIcon PiedPiperAlt => new FontAwesomeIcon("pied-piper-alt");
        public static FontAwesomeIcon PiedPiperHat => new FontAwesomeIcon("pied-piper-hat");
        public static FontAwesomeIcon PiedPiperPp => new FontAwesomeIcon("pied-piper-pp");
        public static FontAwesomeIcon PiggyBank => new FontAwesomeIcon("piggy-bank");
        public static FontAwesomeIcon Pills => new FontAwesomeIcon("pills");
        public static FontAwesomeIcon Pinterest => new FontAwesomeIcon("pinterest");
        public static FontAwesomeIcon PinterestP => new FontAwesomeIcon("pinterest-p");
        public static FontAwesomeIcon PinterestSquare => new FontAwesomeIcon("pinterest-square");
        public static FontAwesomeIcon PizzaSlice => new FontAwesomeIcon("pizza-slice");
        public static FontAwesomeIcon PlaceOfWorship => new FontAwesomeIcon("place-of-worship");
        public static FontAwesomeIcon Plane => new FontAwesomeIcon("plane");
        public static FontAwesomeIcon PlaneArrival => new FontAwesomeIcon("plane-arrival");
        public static FontAwesomeIcon PlaneDeparture => new FontAwesomeIcon("plane-departure");
        public static FontAwesomeIcon Play => new FontAwesomeIcon("play");
        public static FontAwesomeIcon PlayCircle => new FontAwesomeIcon("play-circle");
        public static FontAwesomeIcon Playstation => new FontAwesomeIcon("playstation");
        public static FontAwesomeIcon Plug => new FontAwesomeIcon("plug");
        public static FontAwesomeIcon Plus => new FontAwesomeIcon("plus");
        public static FontAwesomeIcon PlusCircle => new FontAwesomeIcon("plus-circle");
        public static FontAwesomeIcon PlusSquare => new FontAwesomeIcon("plus-square");
        public static FontAwesomeIcon Podcast => new FontAwesomeIcon("podcast");
        public static FontAwesomeIcon Poll => new FontAwesomeIcon("poll");
        public static FontAwesomeIcon PollH => new FontAwesomeIcon("poll-h");
        public static FontAwesomeIcon Poo => new FontAwesomeIcon("poo");
        public static FontAwesomeIcon PooStorm => new FontAwesomeIcon("poo-storm");
        public static FontAwesomeIcon Poop => new FontAwesomeIcon("poop");
        public static FontAwesomeIcon Portrait => new FontAwesomeIcon("portrait");
        public static FontAwesomeIcon PoundSign => new FontAwesomeIcon("pound-sign");
        public static FontAwesomeIcon PowerOff => new FontAwesomeIcon("power-off");
        public static FontAwesomeIcon Pray => new FontAwesomeIcon("pray");
        public static FontAwesomeIcon PrayingHands => new FontAwesomeIcon("praying-hands");
        public static FontAwesomeIcon Prescription => new FontAwesomeIcon("prescription");
        public static FontAwesomeIcon PrescriptionBottle => new FontAwesomeIcon("prescription-bottle");
        public static FontAwesomeIcon PrescriptionBottleAlt => new FontAwesomeIcon("prescription-bottle-alt");
        public static FontAwesomeIcon Print => new FontAwesomeIcon("print");
        public static FontAwesomeIcon Procedures => new FontAwesomeIcon("procedures");
        public static FontAwesomeIcon ProductHunt => new FontAwesomeIcon("product-hunt");
        public static FontAwesomeIcon ProjectDiagram => new FontAwesomeIcon("project-diagram");
        public static FontAwesomeIcon Pushed => new FontAwesomeIcon("pushed");
        public static FontAwesomeIcon PuzzlePiece => new FontAwesomeIcon("puzzle-piece");
        public static FontAwesomeIcon Python => new FontAwesomeIcon("python");
        public static FontAwesomeIcon Qq => new FontAwesomeIcon("qq");
        public static FontAwesomeIcon Qrcode => new FontAwesomeIcon("qrcode");
        public static FontAwesomeIcon Question => new FontAwesomeIcon("question");
        public static FontAwesomeIcon QuestionCircle => new FontAwesomeIcon("question-circle");
        public static FontAwesomeIcon Quidditch => new FontAwesomeIcon("quidditch");
        public static FontAwesomeIcon Quinscape => new FontAwesomeIcon("quinscape");
        public static FontAwesomeIcon Quora => new FontAwesomeIcon("quora");
        public static FontAwesomeIcon QuoteLeft => new FontAwesomeIcon("quote-left");
        public static FontAwesomeIcon QuoteRight => new FontAwesomeIcon("quote-right");
        public static FontAwesomeIcon Quran => new FontAwesomeIcon("quran");
        public static FontAwesomeIcon RProject => new FontAwesomeIcon("r-project");
        public static FontAwesomeIcon Radiation => new FontAwesomeIcon("radiation");
        public static FontAwesomeIcon RadiationAlt => new FontAwesomeIcon("radiation-alt");
        public static FontAwesomeIcon Rainbow => new FontAwesomeIcon("rainbow");
        public static FontAwesomeIcon Random => new FontAwesomeIcon("random");
        public static FontAwesomeIcon RaspberryPi => new FontAwesomeIcon("raspberry-pi");
        public static FontAwesomeIcon Ravelry => new FontAwesomeIcon("ravelry");
        public static FontAwesomeIcon React => new FontAwesomeIcon("react");
        public static FontAwesomeIcon Reacteurope => new FontAwesomeIcon("reacteurope");
        public static FontAwesomeIcon Readme => new FontAwesomeIcon("readme");
        public static FontAwesomeIcon Rebel => new FontAwesomeIcon("rebel");
        public static FontAwesomeIcon Receipt => new FontAwesomeIcon("receipt");
        public static FontAwesomeIcon Recycle => new FontAwesomeIcon("recycle");
        public static FontAwesomeIcon RedRiver => new FontAwesomeIcon("red-river");
        public static FontAwesomeIcon Reddit => new FontAwesomeIcon("reddit");
        public static FontAwesomeIcon RedditAlien => new FontAwesomeIcon("reddit-alien");
        public static FontAwesomeIcon RedditSquare => new FontAwesomeIcon("reddit-square");
        public static FontAwesomeIcon Redhat => new FontAwesomeIcon("redhat");
        public static FontAwesomeIcon Redo => new FontAwesomeIcon("redo");
        public static FontAwesomeIcon RedoAlt => new FontAwesomeIcon("redo-alt");
        public static FontAwesomeIcon Registered => new FontAwesomeIcon("registered");
        public static FontAwesomeIcon Renren => new FontAwesomeIcon("renren");
        public static FontAwesomeIcon Reply => new FontAwesomeIcon("reply");
        public static FontAwesomeIcon ReplyAll => new FontAwesomeIcon("reply-all");
        public static FontAwesomeIcon Replyd => new FontAwesomeIcon("replyd");
        public static FontAwesomeIcon Republican => new FontAwesomeIcon("republican");
        public static FontAwesomeIcon Researchgate => new FontAwesomeIcon("researchgate");
        public static FontAwesomeIcon Resolving => new FontAwesomeIcon("resolving");
        public static FontAwesomeIcon Restroom => new FontAwesomeIcon("restroom");
        public static FontAwesomeIcon Retweet => new FontAwesomeIcon("retweet");
        public static FontAwesomeIcon Rev => new FontAwesomeIcon("rev");
        public static FontAwesomeIcon Ribbon => new FontAwesomeIcon("ribbon");
        public static FontAwesomeIcon Ring => new FontAwesomeIcon("ring");
        public static FontAwesomeIcon Road => new FontAwesomeIcon("road");
        public static FontAwesomeIcon Robot => new FontAwesomeIcon("robot");
        public static FontAwesomeIcon Rocket => new FontAwesomeIcon("rocket");
        public static FontAwesomeIcon Rocketchat => new FontAwesomeIcon("rocketchat");
        public static FontAwesomeIcon Rockrms => new FontAwesomeIcon("rockrms");
        public static FontAwesomeIcon Route => new FontAwesomeIcon("route");
        public static FontAwesomeIcon Rss => new FontAwesomeIcon("rss");
        public static FontAwesomeIcon RssSquare => new FontAwesomeIcon("rss-square");
        public static FontAwesomeIcon RubleSign => new FontAwesomeIcon("ruble-sign");
        public static FontAwesomeIcon Ruler => new FontAwesomeIcon("ruler");
        public static FontAwesomeIcon RulerCombined => new FontAwesomeIcon("ruler-combined");
        public static FontAwesomeIcon RulerHorizontal => new FontAwesomeIcon("ruler-horizontal");
        public static FontAwesomeIcon RulerVertical => new FontAwesomeIcon("ruler-vertical");
        public static FontAwesomeIcon Running => new FontAwesomeIcon("running");
        public static FontAwesomeIcon RupeeSign => new FontAwesomeIcon("rupee-sign");
        public static FontAwesomeIcon SadCry => new FontAwesomeIcon("sad-cry");
        public static FontAwesomeIcon SadTear => new FontAwesomeIcon("sad-tear");
        public static FontAwesomeIcon Safari => new FontAwesomeIcon("safari");
        public static FontAwesomeIcon Salesforce => new FontAwesomeIcon("salesforce");
        public static FontAwesomeIcon Sass => new FontAwesomeIcon("sass");
        public static FontAwesomeIcon Satellite => new FontAwesomeIcon("satellite");
        public static FontAwesomeIcon SatelliteDish => new FontAwesomeIcon("satellite-dish");
        public static FontAwesomeIcon Save => new FontAwesomeIcon("save");
        public static FontAwesomeIcon Schlix => new FontAwesomeIcon("schlix");
        public static FontAwesomeIcon School => new FontAwesomeIcon("school");
        public static FontAwesomeIcon Screwdriver => new FontAwesomeIcon("screwdriver");
        public static FontAwesomeIcon Scribd => new FontAwesomeIcon("scribd");
        public static FontAwesomeIcon Scroll => new FontAwesomeIcon("scroll");
        public static FontAwesomeIcon SdCard => new FontAwesomeIcon("sd-card");
        public static FontAwesomeIcon Search => new FontAwesomeIcon("search");
        public static FontAwesomeIcon SearchDollar => new FontAwesomeIcon("search-dollar");
        public static FontAwesomeIcon SearchLocation => new FontAwesomeIcon("search-location");
        public static FontAwesomeIcon SearchMinus => new FontAwesomeIcon("search-minus");
        public static FontAwesomeIcon SearchPlus => new FontAwesomeIcon("search-plus");
        public static FontAwesomeIcon Searchengin => new FontAwesomeIcon("searchengin");
        public static FontAwesomeIcon Seedling => new FontAwesomeIcon("seedling");
        public static FontAwesomeIcon Sellcast => new FontAwesomeIcon("sellcast");
        public static FontAwesomeIcon Sellsy => new FontAwesomeIcon("sellsy");
        public static FontAwesomeIcon Server => new FontAwesomeIcon("server");
        public static FontAwesomeIcon Servicestack => new FontAwesomeIcon("servicestack");
        public static FontAwesomeIcon Shapes => new FontAwesomeIcon("shapes");
        public static FontAwesomeIcon Share => new FontAwesomeIcon("share");
        public static FontAwesomeIcon ShareAlt => new FontAwesomeIcon("share-alt");
        public static FontAwesomeIcon ShareAltSquare => new FontAwesomeIcon("share-alt-square");
        public static FontAwesomeIcon ShareSquare => new FontAwesomeIcon("share-square");
        public static FontAwesomeIcon ShekelSign => new FontAwesomeIcon("shekel-sign");
        public static FontAwesomeIcon ShieldAlt => new FontAwesomeIcon("shield-alt");
        public static FontAwesomeIcon Ship => new FontAwesomeIcon("ship");
        public static FontAwesomeIcon ShippingFast => new FontAwesomeIcon("shipping-fast");
        public static FontAwesomeIcon Shirtsinbulk => new FontAwesomeIcon("shirtsinbulk");
        public static FontAwesomeIcon ShoePrints => new FontAwesomeIcon("shoe-prints");
        public static FontAwesomeIcon ShoppingBag => new FontAwesomeIcon("shopping-bag");
        public static FontAwesomeIcon ShoppingBasket => new FontAwesomeIcon("shopping-basket");
        public static FontAwesomeIcon ShoppingCart => new FontAwesomeIcon("shopping-cart");
        public static FontAwesomeIcon Shopware => new FontAwesomeIcon("shopware");
        public static FontAwesomeIcon Shower => new FontAwesomeIcon("shower");
        public static FontAwesomeIcon ShuttleVan => new FontAwesomeIcon("shuttle-van");
        public static FontAwesomeIcon Sign => new FontAwesomeIcon("sign");
        public static FontAwesomeIcon SignInAlt => new FontAwesomeIcon("sign-in-alt");
        public static FontAwesomeIcon SignLanguage => new FontAwesomeIcon("sign-language");
        public static FontAwesomeIcon SignOutAlt => new FontAwesomeIcon("sign-out-alt");
        public static FontAwesomeIcon Signal => new FontAwesomeIcon("signal");
        public static FontAwesomeIcon Signature => new FontAwesomeIcon("signature");
        public static FontAwesomeIcon SimCard => new FontAwesomeIcon("sim-card");
        public static FontAwesomeIcon Simplybuilt => new FontAwesomeIcon("simplybuilt");
        public static FontAwesomeIcon Sistrix => new FontAwesomeIcon("sistrix");
        public static FontAwesomeIcon Sitemap => new FontAwesomeIcon("sitemap");
        public static FontAwesomeIcon Sith => new FontAwesomeIcon("sith");
        public static FontAwesomeIcon Skating => new FontAwesomeIcon("skating");
        public static FontAwesomeIcon Sketch => new FontAwesomeIcon("sketch");
        public static FontAwesomeIcon Skiing => new FontAwesomeIcon("skiing");
        public static FontAwesomeIcon SkiingNordic => new FontAwesomeIcon("skiing-nordic");
        public static FontAwesomeIcon Skull => new FontAwesomeIcon("skull");
        public static FontAwesomeIcon SkullCrossbones => new FontAwesomeIcon("skull-crossbones");
        public static FontAwesomeIcon Skyatlas => new FontAwesomeIcon("skyatlas");
        public static FontAwesomeIcon Skype => new FontAwesomeIcon("skype");
        public static FontAwesomeIcon Slack => new FontAwesomeIcon("slack");
        public static FontAwesomeIcon SlackHash => new FontAwesomeIcon("slack-hash");
        public static FontAwesomeIcon Slash => new FontAwesomeIcon("slash");
        public static FontAwesomeIcon Sleigh => new FontAwesomeIcon("sleigh");
        public static FontAwesomeIcon SlidersH => new FontAwesomeIcon("sliders-h");
        public static FontAwesomeIcon Slideshare => new FontAwesomeIcon("slideshare");
        public static FontAwesomeIcon Smile => new FontAwesomeIcon("smile");
        public static FontAwesomeIcon SmileBeam => new FontAwesomeIcon("smile-beam");
        public static FontAwesomeIcon SmileWink => new FontAwesomeIcon("smile-wink");
        public static FontAwesomeIcon Smog => new FontAwesomeIcon("smog");
        public static FontAwesomeIcon Smoking => new FontAwesomeIcon("smoking");
        public static FontAwesomeIcon SmokingBan => new FontAwesomeIcon("smoking-ban");
        public static FontAwesomeIcon Sms => new FontAwesomeIcon("sms");
        public static FontAwesomeIcon Snapchat => new FontAwesomeIcon("snapchat");
        public static FontAwesomeIcon SnapchatGhost => new FontAwesomeIcon("snapchat-ghost");
        public static FontAwesomeIcon SnapchatSquare => new FontAwesomeIcon("snapchat-square");
        public static FontAwesomeIcon Snowboarding => new FontAwesomeIcon("snowboarding");
        public static FontAwesomeIcon Snowflake => new FontAwesomeIcon("snowflake");
        public static FontAwesomeIcon Snowman => new FontAwesomeIcon("snowman");
        public static FontAwesomeIcon Snowplow => new FontAwesomeIcon("snowplow");
        public static FontAwesomeIcon Socks => new FontAwesomeIcon("socks");
        public static FontAwesomeIcon SolarPanel => new FontAwesomeIcon("solar-panel");
        public static FontAwesomeIcon Sort => new FontAwesomeIcon("sort");
        public static FontAwesomeIcon SortAlphaDown => new FontAwesomeIcon("sort-alpha-down");
        public static FontAwesomeIcon SortAlphaUp => new FontAwesomeIcon("sort-alpha-up");
        public static FontAwesomeIcon SortAmountDown => new FontAwesomeIcon("sort-amount-down");
        public static FontAwesomeIcon SortAmountUp => new FontAwesomeIcon("sort-amount-up");
        public static FontAwesomeIcon SortDown => new FontAwesomeIcon("sort-down");
        public static FontAwesomeIcon SortNumericDown => new FontAwesomeIcon("sort-numeric-down");
        public static FontAwesomeIcon SortNumericUp => new FontAwesomeIcon("sort-numeric-up");
        public static FontAwesomeIcon SortUp => new FontAwesomeIcon("sort-up");
        public static FontAwesomeIcon Soundcloud => new FontAwesomeIcon("soundcloud");
        public static FontAwesomeIcon Sourcetree => new FontAwesomeIcon("sourcetree");
        public static FontAwesomeIcon Spa => new FontAwesomeIcon("spa");
        public static FontAwesomeIcon SpaceShuttle => new FontAwesomeIcon("space-shuttle");
        public static FontAwesomeIcon Speakap => new FontAwesomeIcon("speakap");
        public static FontAwesomeIcon SpeakerDeck => new FontAwesomeIcon("speaker-deck");
        public static FontAwesomeIcon Spider => new FontAwesomeIcon("spider");
        public static FontAwesomeIcon Spinner => new FontAwesomeIcon("spinner");
        public static FontAwesomeIcon Splotch => new FontAwesomeIcon("splotch");
        public static FontAwesomeIcon Spotify => new FontAwesomeIcon("spotify");
        public static FontAwesomeIcon SprayCan => new FontAwesomeIcon("spray-can");
        public static FontAwesomeIcon Square => new FontAwesomeIcon("square");
        public static FontAwesomeIcon SquareFull => new FontAwesomeIcon("square-full");
        public static FontAwesomeIcon SquareRootAlt => new FontAwesomeIcon("square-root-alt");
        public static FontAwesomeIcon Squarespace => new FontAwesomeIcon("squarespace");
        public static FontAwesomeIcon StackExchange => new FontAwesomeIcon("stack-exchange");
        public static FontAwesomeIcon StackOverflow => new FontAwesomeIcon("stack-overflow");
        public static FontAwesomeIcon Stamp => new FontAwesomeIcon("stamp");
        public static FontAwesomeIcon Star => new FontAwesomeIcon("star");
        public static FontAwesomeIcon StarAndCrescent => new FontAwesomeIcon("star-and-crescent");
        public static FontAwesomeIcon StarHalf => new FontAwesomeIcon("star-half");
        public static FontAwesomeIcon StarHalfAlt => new FontAwesomeIcon("star-half-alt");
        public static FontAwesomeIcon StarOfDavid => new FontAwesomeIcon("star-of-david");
        public static FontAwesomeIcon StarOfLife => new FontAwesomeIcon("star-of-life");
        public static FontAwesomeIcon Staylinked => new FontAwesomeIcon("staylinked");
        public static FontAwesomeIcon Steam => new FontAwesomeIcon("steam");
        public static FontAwesomeIcon SteamSquare => new FontAwesomeIcon("steam-square");
        public static FontAwesomeIcon SteamSymbol => new FontAwesomeIcon("steam-symbol");
        public static FontAwesomeIcon StepBackward => new FontAwesomeIcon("step-backward");
        public static FontAwesomeIcon StepForward => new FontAwesomeIcon("step-forward");
        public static FontAwesomeIcon Stethoscope => new FontAwesomeIcon("stethoscope");
        public static FontAwesomeIcon StickerMule => new FontAwesomeIcon("sticker-mule");
        public static FontAwesomeIcon StickyNote => new FontAwesomeIcon("sticky-note");
        public static FontAwesomeIcon Stop => new FontAwesomeIcon("stop");
        public static FontAwesomeIcon StopCircle => new FontAwesomeIcon("stop-circle");
        public static FontAwesomeIcon Stopwatch => new FontAwesomeIcon("stopwatch");
        public static FontAwesomeIcon Store => new FontAwesomeIcon("store");
        public static FontAwesomeIcon StoreAlt => new FontAwesomeIcon("store-alt");
        public static FontAwesomeIcon Strava => new FontAwesomeIcon("strava");
        public static FontAwesomeIcon Stream => new FontAwesomeIcon("stream");
        public static FontAwesomeIcon StreetView => new FontAwesomeIcon("street-view");
        public static FontAwesomeIcon Strikethrough => new FontAwesomeIcon("strikethrough");
        public static FontAwesomeIcon Stripe => new FontAwesomeIcon("stripe");
        public static FontAwesomeIcon StripeS => new FontAwesomeIcon("stripe-s");
        public static FontAwesomeIcon Stroopwafel => new FontAwesomeIcon("stroopwafel");
        public static FontAwesomeIcon Studiovinari => new FontAwesomeIcon("studiovinari");
        public static FontAwesomeIcon Stumbleupon => new FontAwesomeIcon("stumbleupon");
        public static FontAwesomeIcon StumbleuponCircle => new FontAwesomeIcon("stumbleupon-circle");
        public static FontAwesomeIcon Subscript => new FontAwesomeIcon("subscript");
        public static FontAwesomeIcon Subway => new FontAwesomeIcon("subway");
        public static FontAwesomeIcon Suitcase => new FontAwesomeIcon("suitcase");
        public static FontAwesomeIcon SuitcaseRolling => new FontAwesomeIcon("suitcase-rolling");
        public static FontAwesomeIcon Sun => new FontAwesomeIcon("sun");
        public static FontAwesomeIcon Superpowers => new FontAwesomeIcon("superpowers");
        public static FontAwesomeIcon Superscript => new FontAwesomeIcon("superscript");
        public static FontAwesomeIcon Supple => new FontAwesomeIcon("supple");
        public static FontAwesomeIcon Surprise => new FontAwesomeIcon("surprise");
        public static FontAwesomeIcon Suse => new FontAwesomeIcon("suse");
        public static FontAwesomeIcon Swatchbook => new FontAwesomeIcon("swatchbook");
        public static FontAwesomeIcon Swimmer => new FontAwesomeIcon("swimmer");
        public static FontAwesomeIcon SwimmingPool => new FontAwesomeIcon("swimming-pool");
        public static FontAwesomeIcon Symfony => new FontAwesomeIcon("symfony");
        public static FontAwesomeIcon Synagogue => new FontAwesomeIcon("synagogue");
        public static FontAwesomeIcon Sync => new FontAwesomeIcon("sync");
        public static FontAwesomeIcon SyncAlt => new FontAwesomeIcon("sync-alt");
        public static FontAwesomeIcon Syringe => new FontAwesomeIcon("syringe");
        public static FontAwesomeIcon Table => new FontAwesomeIcon("table");
        public static FontAwesomeIcon TableTennis => new FontAwesomeIcon("table-tennis");
        public static FontAwesomeIcon Tablet => new FontAwesomeIcon("tablet");
        public static FontAwesomeIcon TabletAlt => new FontAwesomeIcon("tablet-alt");
        public static FontAwesomeIcon Tablets => new FontAwesomeIcon("tablets");
        public static FontAwesomeIcon TachometerAlt => new FontAwesomeIcon("tachometer-alt");
        public static FontAwesomeIcon Tag => new FontAwesomeIcon("tag");
        public static FontAwesomeIcon Tags => new FontAwesomeIcon("tags");
        public static FontAwesomeIcon Tape => new FontAwesomeIcon("tape");
        public static FontAwesomeIcon Tasks => new FontAwesomeIcon("tasks");
        public static FontAwesomeIcon Taxi => new FontAwesomeIcon("taxi");
        public static FontAwesomeIcon Teamspeak => new FontAwesomeIcon("teamspeak");
        public static FontAwesomeIcon Teeth => new FontAwesomeIcon("teeth");
        public static FontAwesomeIcon TeethOpen => new FontAwesomeIcon("teeth-open");
        public static FontAwesomeIcon Telegram => new FontAwesomeIcon("telegram");
        public static FontAwesomeIcon TelegramPlane => new FontAwesomeIcon("telegram-plane");
        public static FontAwesomeIcon TemperatureHigh => new FontAwesomeIcon("temperature-high");
        public static FontAwesomeIcon TemperatureLow => new FontAwesomeIcon("temperature-low");
        public static FontAwesomeIcon TencentWeibo => new FontAwesomeIcon("tencent-weibo");
        public static FontAwesomeIcon Tenge => new FontAwesomeIcon("tenge");
        public static FontAwesomeIcon Terminal => new FontAwesomeIcon("terminal");
        public static FontAwesomeIcon TextHeight => new FontAwesomeIcon("text-height");
        public static FontAwesomeIcon TextWidth => new FontAwesomeIcon("text-width");
        public static FontAwesomeIcon Th => new FontAwesomeIcon("th");
        public static FontAwesomeIcon ThLarge => new FontAwesomeIcon("th-large");
        public static FontAwesomeIcon ThList => new FontAwesomeIcon("th-list");
        public static FontAwesomeIcon TheRedYeti => new FontAwesomeIcon("the-red-yeti");
        public static FontAwesomeIcon TheaterMasks => new FontAwesomeIcon("theater-masks");
        public static FontAwesomeIcon Themeco => new FontAwesomeIcon("themeco");
        public static FontAwesomeIcon Themeisle => new FontAwesomeIcon("themeisle");
        public static FontAwesomeIcon Thermometer => new FontAwesomeIcon("thermometer");
        public static FontAwesomeIcon ThermometerEmpty => new FontAwesomeIcon("thermometer-empty");
        public static FontAwesomeIcon ThermometerFull => new FontAwesomeIcon("thermometer-full");
        public static FontAwesomeIcon ThermometerHalf => new FontAwesomeIcon("thermometer-half");
        public static FontAwesomeIcon ThermometerQuarter => new FontAwesomeIcon("thermometer-quarter");
        public static FontAwesomeIcon ThermometerThreeQuarters => new FontAwesomeIcon("thermometer-three-quarters");
        public static FontAwesomeIcon ThinkPeaks => new FontAwesomeIcon("think-peaks");
        public static FontAwesomeIcon ThumbsDown => new FontAwesomeIcon("thumbs-down");
        public static FontAwesomeIcon ThumbsUp => new FontAwesomeIcon("thumbs-up");
        public static FontAwesomeIcon Thumbtack => new FontAwesomeIcon("thumbtack");
        public static FontAwesomeIcon TicketAlt => new FontAwesomeIcon("ticket-alt");
        public static FontAwesomeIcon Times => new FontAwesomeIcon("times");
        public static FontAwesomeIcon TimesCircle => new FontAwesomeIcon("times-circle");
        public static FontAwesomeIcon Tint => new FontAwesomeIcon("tint");
        public static FontAwesomeIcon TintSlash => new FontAwesomeIcon("tint-slash");
        public static FontAwesomeIcon Tired => new FontAwesomeIcon("tired");
        public static FontAwesomeIcon ToggleOff => new FontAwesomeIcon("toggle-off");
        public static FontAwesomeIcon ToggleOn => new FontAwesomeIcon("toggle-on");
        public static FontAwesomeIcon Toilet => new FontAwesomeIcon("toilet");
        public static FontAwesomeIcon ToiletPaper => new FontAwesomeIcon("toilet-paper");
        public static FontAwesomeIcon Toolbox => new FontAwesomeIcon("toolbox");
        public static FontAwesomeIcon Tools => new FontAwesomeIcon("tools");
        public static FontAwesomeIcon Tooth => new FontAwesomeIcon("tooth");
        public static FontAwesomeIcon Torah => new FontAwesomeIcon("torah");
        public static FontAwesomeIcon ToriiGate => new FontAwesomeIcon("torii-gate");
        public static FontAwesomeIcon Tractor => new FontAwesomeIcon("tractor");
        public static FontAwesomeIcon TradeFederation => new FontAwesomeIcon("trade-federation");
        public static FontAwesomeIcon Trademark => new FontAwesomeIcon("trademark");
        public static FontAwesomeIcon TrafficLight => new FontAwesomeIcon("traffic-light");
        public static FontAwesomeIcon Train => new FontAwesomeIcon("train");
        public static FontAwesomeIcon Tram => new FontAwesomeIcon("tram");
        public static FontAwesomeIcon Transgender => new FontAwesomeIcon("transgender");
        public static FontAwesomeIcon TransgenderAlt => new FontAwesomeIcon("transgender-alt");
        public static FontAwesomeIcon Trash => new FontAwesomeIcon("trash");
        public static FontAwesomeIcon TrashAlt => new FontAwesomeIcon("trash-alt");
        public static FontAwesomeIcon TrashRestore => new FontAwesomeIcon("trash-restore");
        public static FontAwesomeIcon TrashRestoreAlt => new FontAwesomeIcon("trash-restore-alt");
        public static FontAwesomeIcon Tree => new FontAwesomeIcon("tree");
        public static FontAwesomeIcon Trello => new FontAwesomeIcon("trello");
        public static FontAwesomeIcon Tripadvisor => new FontAwesomeIcon("tripadvisor");
        public static FontAwesomeIcon Trophy => new FontAwesomeIcon("trophy");
        public static FontAwesomeIcon Truck => new FontAwesomeIcon("truck");
        public static FontAwesomeIcon TruckLoading => new FontAwesomeIcon("truck-loading");
        public static FontAwesomeIcon TruckMonster => new FontAwesomeIcon("truck-monster");
        public static FontAwesomeIcon TruckMoving => new FontAwesomeIcon("truck-moving");
        public static FontAwesomeIcon TruckPickup => new FontAwesomeIcon("truck-pickup");
        public static FontAwesomeIcon Tshirt => new FontAwesomeIcon("tshirt");
        public static FontAwesomeIcon Tty => new FontAwesomeIcon("tty");
        public static FontAwesomeIcon Tumblr => new FontAwesomeIcon("tumblr");
        public static FontAwesomeIcon TumblrSquare => new FontAwesomeIcon("tumblr-square");
        public static FontAwesomeIcon Tv => new FontAwesomeIcon("tv");
        public static FontAwesomeIcon Twitch => new FontAwesomeIcon("twitch");
        public static FontAwesomeIcon Twitter => new FontAwesomeIcon("twitter");
        public static FontAwesomeIcon TwitterSquare => new FontAwesomeIcon("twitter-square");
        public static FontAwesomeIcon Typo3 => new FontAwesomeIcon("typo3");
        public static FontAwesomeIcon Uber => new FontAwesomeIcon("uber");
        public static FontAwesomeIcon Ubuntu => new FontAwesomeIcon("ubuntu");
        public static FontAwesomeIcon Uikit => new FontAwesomeIcon("uikit");
        public static FontAwesomeIcon Umbrella => new FontAwesomeIcon("umbrella");
        public static FontAwesomeIcon UmbrellaBeach => new FontAwesomeIcon("umbrella-beach");
        public static FontAwesomeIcon Underline => new FontAwesomeIcon("underline");
        public static FontAwesomeIcon Undo => new FontAwesomeIcon("undo");
        public static FontAwesomeIcon UndoAlt => new FontAwesomeIcon("undo-alt");
        public static FontAwesomeIcon Uniregistry => new FontAwesomeIcon("uniregistry");
        public static FontAwesomeIcon UniversalAccess => new FontAwesomeIcon("universal-access");
        public static FontAwesomeIcon University => new FontAwesomeIcon("university");
        public static FontAwesomeIcon Unlink => new FontAwesomeIcon("unlink");
        public static FontAwesomeIcon Unlock => new FontAwesomeIcon("unlock");
        public static FontAwesomeIcon UnlockAlt => new FontAwesomeIcon("unlock-alt");
        public static FontAwesomeIcon Untappd => new FontAwesomeIcon("untappd");
        public static FontAwesomeIcon Upload => new FontAwesomeIcon("upload");
        public static FontAwesomeIcon Ups => new FontAwesomeIcon("ups");
        public static FontAwesomeIcon Usb => new FontAwesomeIcon("usb");
        public static FontAwesomeIcon User => new FontAwesomeIcon("user");
        public static FontAwesomeIcon UserAlt => new FontAwesomeIcon("user-alt");
        public static FontAwesomeIcon UserAltSlash => new FontAwesomeIcon("user-alt-slash");
        public static FontAwesomeIcon UserAstronaut => new FontAwesomeIcon("user-astronaut");
        public static FontAwesomeIcon UserCheck => new FontAwesomeIcon("user-check");
        public static FontAwesomeIcon UserCircle => new FontAwesomeIcon("user-circle");
        public static FontAwesomeIcon UserClock => new FontAwesomeIcon("user-clock");
        public static FontAwesomeIcon UserCog => new FontAwesomeIcon("user-cog");
        public static FontAwesomeIcon UserEdit => new FontAwesomeIcon("user-edit");
        public static FontAwesomeIcon UserFriends => new FontAwesomeIcon("user-friends");
        public static FontAwesomeIcon UserGraduate => new FontAwesomeIcon("user-graduate");
        public static FontAwesomeIcon UserInjured => new FontAwesomeIcon("user-injured");
        public static FontAwesomeIcon UserLock => new FontAwesomeIcon("user-lock");
        public static FontAwesomeIcon UserMd => new FontAwesomeIcon("user-md");
        public static FontAwesomeIcon UserMinus => new FontAwesomeIcon("user-minus");
        public static FontAwesomeIcon UserNinja => new FontAwesomeIcon("user-ninja");
        public static FontAwesomeIcon UserNurse => new FontAwesomeIcon("user-nurse");
        public static FontAwesomeIcon UserPlus => new FontAwesomeIcon("user-plus");
        public static FontAwesomeIcon UserSecret => new FontAwesomeIcon("user-secret");
        public static FontAwesomeIcon UserShield => new FontAwesomeIcon("user-shield");
        public static FontAwesomeIcon UserSlash => new FontAwesomeIcon("user-slash");
        public static FontAwesomeIcon UserTag => new FontAwesomeIcon("user-tag");
        public static FontAwesomeIcon UserTie => new FontAwesomeIcon("user-tie");
        public static FontAwesomeIcon UserTimes => new FontAwesomeIcon("user-times");
        public static FontAwesomeIcon Users => new FontAwesomeIcon("users");
        public static FontAwesomeIcon UsersCog => new FontAwesomeIcon("users-cog");
        public static FontAwesomeIcon Usps => new FontAwesomeIcon("usps");
        public static FontAwesomeIcon Ussunnah => new FontAwesomeIcon("ussunnah");
        public static FontAwesomeIcon UtensilSpoon => new FontAwesomeIcon("utensil-spoon");
        public static FontAwesomeIcon Utensils => new FontAwesomeIcon("utensils");
        public static FontAwesomeIcon Vaadin => new FontAwesomeIcon("vaadin");
        public static FontAwesomeIcon VectorSquare => new FontAwesomeIcon("vector-square");
        public static FontAwesomeIcon Venus => new FontAwesomeIcon("venus");
        public static FontAwesomeIcon VenusDouble => new FontAwesomeIcon("venus-double");
        public static FontAwesomeIcon VenusMars => new FontAwesomeIcon("venus-mars");
        public static FontAwesomeIcon Viacoin => new FontAwesomeIcon("viacoin");
        public static FontAwesomeIcon Viadeo => new FontAwesomeIcon("viadeo");
        public static FontAwesomeIcon ViadeoSquare => new FontAwesomeIcon("viadeo-square");
        public static FontAwesomeIcon Vial => new FontAwesomeIcon("vial");
        public static FontAwesomeIcon Vials => new FontAwesomeIcon("vials");
        public static FontAwesomeIcon Viber => new FontAwesomeIcon("viber");
        public static FontAwesomeIcon Video => new FontAwesomeIcon("video");
        public static FontAwesomeIcon VideoSlash => new FontAwesomeIcon("video-slash");
        public static FontAwesomeIcon Vihara => new FontAwesomeIcon("vihara");
        public static FontAwesomeIcon Vimeo => new FontAwesomeIcon("vimeo");
        public static FontAwesomeIcon VimeoSquare => new FontAwesomeIcon("vimeo-square");
        public static FontAwesomeIcon VimeoV => new FontAwesomeIcon("vimeo-v");
        public static FontAwesomeIcon Vine => new FontAwesomeIcon("vine");
        public static FontAwesomeIcon Vk => new FontAwesomeIcon("vk");
        public static FontAwesomeIcon Vnv => new FontAwesomeIcon("vnv");
        public static FontAwesomeIcon VolleyballBall => new FontAwesomeIcon("volleyball-ball");
        public static FontAwesomeIcon VolumeDown => new FontAwesomeIcon("volume-down");
        public static FontAwesomeIcon VolumeMute => new FontAwesomeIcon("volume-mute");
        public static FontAwesomeIcon VolumeOff => new FontAwesomeIcon("volume-off");
        public static FontAwesomeIcon VolumeUp => new FontAwesomeIcon("volume-up");
        public static FontAwesomeIcon VoteYea => new FontAwesomeIcon("vote-yea");
        public static FontAwesomeIcon VrCardboard => new FontAwesomeIcon("vr-cardboard");
        public static FontAwesomeIcon Vuejs => new FontAwesomeIcon("vuejs");
        public static FontAwesomeIcon Walking => new FontAwesomeIcon("walking");
        public static FontAwesomeIcon Wallet => new FontAwesomeIcon("wallet");
        public static FontAwesomeIcon Warehouse => new FontAwesomeIcon("warehouse");
        public static FontAwesomeIcon Water => new FontAwesomeIcon("water");
        public static FontAwesomeIcon WaveSquare => new FontAwesomeIcon("wave-square");
        public static FontAwesomeIcon Waze => new FontAwesomeIcon("waze");
        public static FontAwesomeIcon Weebly => new FontAwesomeIcon("weebly");
        public static FontAwesomeIcon Weibo => new FontAwesomeIcon("weibo");
        public static FontAwesomeIcon Weight => new FontAwesomeIcon("weight");
        public static FontAwesomeIcon WeightHanging => new FontAwesomeIcon("weight-hanging");
        public static FontAwesomeIcon Weixin => new FontAwesomeIcon("weixin");
        public static FontAwesomeIcon Whatsapp => new FontAwesomeIcon("whatsapp");
        public static FontAwesomeIcon WhatsappSquare => new FontAwesomeIcon("whatsapp-square");
        public static FontAwesomeIcon Wheelchair => new FontAwesomeIcon("wheelchair");
        public static FontAwesomeIcon Whmcs => new FontAwesomeIcon("whmcs");
        public static FontAwesomeIcon Wifi => new FontAwesomeIcon("wifi");
        public static FontAwesomeIcon WikipediaW => new FontAwesomeIcon("wikipedia-w");
        public static FontAwesomeIcon Wind => new FontAwesomeIcon("wind");
        public static FontAwesomeIcon WindowClose => new FontAwesomeIcon("window-close");
        public static FontAwesomeIcon WindowMaximize => new FontAwesomeIcon("window-maximize");
        public static FontAwesomeIcon WindowMinimize => new FontAwesomeIcon("window-minimize");
        public static FontAwesomeIcon WindowRestore => new FontAwesomeIcon("window-restore");
        public static FontAwesomeIcon Windows => new FontAwesomeIcon("windows");
        public static FontAwesomeIcon WineBottle => new FontAwesomeIcon("wine-bottle");
        public static FontAwesomeIcon WineGlass => new FontAwesomeIcon("wine-glass");
        public static FontAwesomeIcon WineGlassAlt => new FontAwesomeIcon("wine-glass-alt");
        public static FontAwesomeIcon Wix => new FontAwesomeIcon("wix");
        public static FontAwesomeIcon WizardsOfTheCoast => new FontAwesomeIcon("wizards-of-the-coast");
        public static FontAwesomeIcon WolfPackBattalion => new FontAwesomeIcon("wolf-pack-battalion");
        public static FontAwesomeIcon WonSign => new FontAwesomeIcon("won-sign");
        public static FontAwesomeIcon Wordpress => new FontAwesomeIcon("wordpress");
        public static FontAwesomeIcon WordpressSimple => new FontAwesomeIcon("wordpress-simple");
        public static FontAwesomeIcon Wpbeginner => new FontAwesomeIcon("wpbeginner");
        public static FontAwesomeIcon Wpexplorer => new FontAwesomeIcon("wpexplorer");
        public static FontAwesomeIcon Wpforms => new FontAwesomeIcon("wpforms");
        public static FontAwesomeIcon Wpressr => new FontAwesomeIcon("wpressr");
        public static FontAwesomeIcon Wrench => new FontAwesomeIcon("wrench");
        public static FontAwesomeIcon XRay => new FontAwesomeIcon("x-ray");
        public static FontAwesomeIcon Xbox => new FontAwesomeIcon("xbox");
        public static FontAwesomeIcon Xing => new FontAwesomeIcon("xing");
        public static FontAwesomeIcon XingSquare => new FontAwesomeIcon("xing-square");
        public static FontAwesomeIcon YCombinator => new FontAwesomeIcon("y-combinator");
        public static FontAwesomeIcon Yahoo => new FontAwesomeIcon("yahoo");
        public static FontAwesomeIcon Yammer => new FontAwesomeIcon("yammer");
        public static FontAwesomeIcon Yandex => new FontAwesomeIcon("yandex");
        public static FontAwesomeIcon YandexInternational => new FontAwesomeIcon("yandex-international");
        public static FontAwesomeIcon Yarn => new FontAwesomeIcon("yarn");
        public static FontAwesomeIcon Yelp => new FontAwesomeIcon("yelp");
        public static FontAwesomeIcon YenSign => new FontAwesomeIcon("yen-sign");
        public static FontAwesomeIcon YinYang => new FontAwesomeIcon("yin-yang");
        public static FontAwesomeIcon Yoast => new FontAwesomeIcon("yoast");
        public static FontAwesomeIcon Youtube => new FontAwesomeIcon("youtube");
        public static FontAwesomeIcon YoutubeSquare => new FontAwesomeIcon("youtube-square");
        public static FontAwesomeIcon Zhihu => new FontAwesomeIcon("zhihu");




        #endregion
    }
}