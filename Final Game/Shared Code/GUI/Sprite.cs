using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// A sprite with a texture, position, size, and rotation to be drawn
    /// on a <code>SpriteBatch</code>.
    /// </summary>
    public class Sprite2D
    {
        public const float OpacityThreshold = 0.00001f;

        public int RenderWidth
        {
            get
            {
                return (Texture.Width * ScaleX).ToInt();
            }
            set
            {
                ScaleX = value / (float)Texture.Width;
            }
        }

        public int RenderHeight
        {
            get
            {
                return (Texture.Height * ScaleY).ToInt();
            }
            set
            {
                ScaleY = value / (float)Texture.Height;
            }
        }

        public float HalfWidth { get { return RenderWidth / 2f; } }
        public float HalfHeight { get { return RenderHeight / 2f; } }

        // Use integers for now (could use a Vector2 instead)
        public int RenderX { get { return (int)Position.X; } set { Position.X = value; } }
        public int RenderY { get { return (int)Position.Y; } set { Position.Y = value; } }

        public float Rotation { get; set; }

        public float ScaleX { get; set; }
        public float ScaleY { get; set; }

        public Texture2D Texture { get; set; }
        public float Opacity { get; set; }
        public Color DrawColor { get; set; }

        protected Dictionary<AnimationType, SpriteAnimation> Animations;

        public Vector2 Position;

        /// <summary>
        /// The center of rotatio, in texture coordinates
        /// </summary>
        public Vector2 Origin;

        public bool IsTransparent
        {
            get { return Opacity < OpacityThreshold; }
        }

        public Sprite2D(
            Texture2D Texture,
            int X = 0, int Y = 0,
            int? Width = null, int? Height = null,
            float Rotation = 0,
            float Scale = 1,
            float Opacity = 1,
            Color? DrawColor = null,
            Dictionary<AnimationType, SpriteAnimation> Animations = null)
        {
            this.Texture = Texture;

            this.RenderX = X;
            this.RenderY = Y;

            if (Width.HasValue)
                this.RenderWidth = Width.Value;
            else
                this.RenderWidth = Texture.Width;

            if (Height.HasValue)
                this.RenderHeight = Height.Value;
            else
                this.RenderHeight = Texture.Height;

            this.Rotation = Rotation;
            this.ScaleX = Scale;
            this.ScaleY = Scale;

            this.Opacity = Opacity;
            this.DrawColor = DrawColor ?? Color.White;

            this.Animations = Animations ?? new Dictionary<AnimationType, SpriteAnimation>();
        }

        public virtual void Initialize()
        {
            foreach (var Animation in this.Animations)
                Animation.Value.Initialize(this);
        }

        public void AddAnimation(AnimationType Type, SpriteAnimation Animation)
        {
            this.Animations.Add(Type, Animation);
            Animation.Initialize(this);
        }

        public void Draw(SpriteBatch Target)
        {
            if (Texture != null)
                Target.Draw(Texture, position: Position, rotation: Rotation, color: DrawColor * Opacity, scale: new Vector2(ScaleX, ScaleY), origin: Origin);
        }

        public void Update(GameTime Time)
        {
            foreach (var Animation in Animations)
            {
                if (Animation.Value.IsStarted)
                {
                    Animation.Value.Update(Time);

                    if (Animation.Value.IsFinished)
                    {
                        Animation.Value.IsStarted = false;
                        Animation.Value.Reset();
                    }
                }
            }
        }

        public void Reset()
        {
            RenderWidth = RenderHeight = 0;
            RenderX = RenderY = 0;
            Rotation = 0f;

            foreach (var Animation in Animations)
            {
                Animation.Value.Reset();
                Animation.Value.IsStarted = false;
            }
        }

        public void CenterOrigin()
        {
            Origin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        }

        public bool StartAnimation(AnimationType Type)
        {
            if (Animations.ContainsKey(Type))
                Animations[Type].IsStarted = true;

            return Animations.ContainsKey(Type);
        }

        public bool StopAnimation(AnimationType Type)
        {
            if (Animations.ContainsKey(Type))
                Animations[Type].IsStarted = false;

            return Animations.ContainsKey(Type);
        }

        public bool? GetAnimationState(AnimationType Type)
        {
            if (Animations.ContainsKey(Type))
                return Animations[Type].IsFinished;
            else return null;
        }

        public SpriteAnimation GetAnimation(AnimationType Type)
        {
            if (!Animations.ContainsKey(Type))
                return null;
            return Animations[Type];
        }
    }
}
