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

        public int RenderWidth { get; set; }
        public int RenderHeight { get; set; }

        public float HalfWidth { get { return RenderWidth / 2f; } }
        public float HalfHeight { get { return RenderHeight / 2f; } }

        // Use integers for now (could use a Vector2 instead)
        public int RenderX { get { return (int)Position.X; } set { Position.X = value; } }
        public int RenderY { get { return (int)Position.Y; } set { Position.Y = value; } }

        public float Rotation { get; set; }
        public float Scale { get; set; }

        public Texture2D Texture { get; set; }
        public float Opacity { get; set; }
        public Color DrawColor { get; set; }

        protected Dictionary<AnimationType, SpriteAnimation> Animations;

        public Vector2 Position;

        public Rectangle? TargetArea
        {
            get
            {
                return new Rectangle(RenderX, RenderY, RenderWidth, RenderHeight);
            }

            set
            {
                if (value.HasValue)
                {
                    RenderWidth = value.Value.Width;
                    RenderHeight = value.Value.Height;
                    RenderX = value.Value.X;
                    RenderY = value.Value.Y;
                }
            }
        }

        public bool IsTransparent
        {
            get { return Opacity < OpacityThreshold; }
        }

        public Sprite2D(
            Texture2D Texture,
            Rectangle? Target = null,
            int X = 0, int Y = 0,
            int? Width = null, int? Height = null,
            float Rotation = 0,
            float Scale = 1,
            float Opacity = 1,
            Color? DrawColor = null,
            Dictionary<AnimationType, SpriteAnimation> Animations = null)
        {
            if (Target.HasValue)
                this.TargetArea = TargetArea;
            else
            {
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
            }

            this.Rotation = Rotation;
            this.Scale = Scale;

            this.Opacity = Opacity;
            this.Texture = Texture;
            this.DrawColor = DrawColor ?? Color.White;

            this.Animations = Animations ?? new Dictionary<AnimationType, SpriteAnimation>();

            foreach (var Animation in this.Animations)
                Animation.Value.Initialize(this);
        }

        public virtual void Initialize()
        {
            // Don't do anything
        }

        public void AddAnimation(AnimationType Type, SpriteAnimation Animation)
        {
            this.Animations.Add(Type, Animation);
            Animation.Initialize(this);
        }

        public void Draw(SpriteBatch Target)
        {
            if (Texture != null)
                Target.Draw(Texture, destinationRectangle: TargetArea, rotation: Rotation, color: DrawColor * Opacity, scale: new Vector2(Scale));
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
    }
}
