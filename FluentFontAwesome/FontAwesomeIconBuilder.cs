using System;

namespace FluentFontAwesome
{
    public class FontAwesomeIconBuilder
    {
        private string _name;

        private Size _size = FluentFontAwesome.Size.Normal;
        private bool _fixedWidth = false;
        private bool _bordered = false;
        private Pull _pull = FluentFontAwesome.Pull.None;
        private Animation _animation = FluentFontAwesome.Animation.None;
        private Rotation _rotation = FluentFontAwesome.Rotation.None;
        private Flip _flip = FluentFontAwesome.Flip.None;

        public FontAwesomeIconBuilder()
        {
        }

        public FontAwesomeIconBuilder(string name)
        {
            Name(_name);
        }

        public FontAwesomeIconBuilder(FontAwesomeIcon icon)
        {
            _name = icon.Name;
            _size = icon.Size;
            _fixedWidth = icon.FixedWidth;
            _bordered = icon.Bordered;
            _pull = icon.Pull;
            _animation = icon.Animation;
            _rotation = icon.Rotation;
            _flip = icon.Flip;
        }

        public FontAwesomeIcon Icon 
            => new FontAwesomeIcon(_name)
            {
                Size = _size,
                FixedWidth = _fixedWidth,
                Bordered = _bordered,
                Pull = _pull,
                Animation = _animation,
                Rotation = _rotation,
                Flip = _flip
            };
        
        #region Fluent Methods

        public FontAwesomeIconBuilder Name(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));

            return this;
        }

        public FontAwesomeIconBuilder Size(Size size)
        {
            _size = size;
            return this;
        }
        public FontAwesomeIconBuilder Large() => Size(FluentFontAwesome.Size.Large);
        public FontAwesomeIconBuilder Scale2x() => Size(FluentFontAwesome.Size.x2);
        public FontAwesomeIconBuilder Scale3x() => Size(FluentFontAwesome.Size.x3);
        public FontAwesomeIconBuilder Scale4x() => Size(FluentFontAwesome.Size.x4);
        public FontAwesomeIconBuilder Scale5x() => Size(FluentFontAwesome.Size.x5);

        public FontAwesomeIconBuilder FixedWidth(bool fixedWidth = true)
        {
            _fixedWidth = fixedWidth;
            return this;
        }
        
        public FontAwesomeIconBuilder Bordered(bool bordered = true)
        {
            _bordered = bordered;
            return this;
        }
        
        public FontAwesomeIconBuilder Pull(Pull pull = FluentFontAwesome.Pull.Right)
        {
            _pull = pull;
            return this;
        }
        public FontAwesomeIconBuilder PullRight() => Pull(FluentFontAwesome.Pull.Right);
        public FontAwesomeIconBuilder PullLeft() => Pull(FluentFontAwesome.Pull.Left);


        public FontAwesomeIconBuilder Animate(Animation animation)
        {
            _animation = animation;
            return this;
        }
        public FontAwesomeIconBuilder Spin() => Animate(Animation.Spin);
        public FontAwesomeIconBuilder Pulse() => Animate(Animation.Pulse);
        

        public FontAwesomeIconBuilder Rotate(Rotation rotation)
        {
            _rotation = rotation;
            return this;
        }
        public FontAwesomeIconBuilder Rotate90() => Rotate(Rotation.Rotate90);
        public FontAwesomeIconBuilder Rotate180() => Rotate(Rotation.Rotate180);
        public FontAwesomeIconBuilder Rotate270() => Rotate(Rotation.Rotate270);
        
        
        public FontAwesomeIconBuilder Flip(Flip flip = FluentFontAwesome.Flip.Horizontal)
        {
            _flip = flip;
            return this;
        }
        public FontAwesomeIconBuilder FlipHorizontal() => Flip(FluentFontAwesome.Flip.Horizontal);
        public FontAwesomeIconBuilder FlipVertical() => Flip(FluentFontAwesome.Flip.Vertical);

        #endregion

        public static implicit operator FontAwesomeIcon(FontAwesomeIconBuilder iconBuilder)
            => iconBuilder.Icon;
    }
}