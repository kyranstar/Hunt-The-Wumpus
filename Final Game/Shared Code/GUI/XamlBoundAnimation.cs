using EmptyKeys.UserInterface;
using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class XamlBoundAnimation
    {
        public const string XamlAnimationGroupName = "XamlAnimation";

        protected StateAnimator InternalAnimator;
        protected float PreviousNotifiedValue = float.PositiveInfinity;

        protected XamlAnimationDescriptor AnimationDesc;

        protected Action<string, string> RaisePropertyChangedForGroup;

        [PropertyGroup(XamlAnimationGroupName)]
        public float CurrentValue { get; protected set; }

        public XamlBoundAnimation(XamlAnimationDescriptor AnimationDesc, Action<string, string> RaisePropertyChangedForGroup)
        {
            this.RaisePropertyChangedForGroup = RaisePropertyChangedForGroup;
            this.AnimationDesc = AnimationDesc;

            // TODO: Accept custom easing
            InternalAnimator = new StateAnimator(
                Pct =>
                {
                    // TODO: Add math to do cubic Bezier curve:
                    // (0.165, 0.84), (0.44, 1)
                    return (float)Math.Pow(Pct, 2);
                },
                Pct =>
                {
                    return (float)-Math.Pow(Pct, 2) + 1;
                },
            AnimationDesc.TransitionDuration);
        }

        public void Update(GameTime gameTime, bool newValue)
        {
            InternalAnimator.Update(gameTime, newValue);
            CurrentValue = MathHelper.Clamp(InternalAnimator.CurrentValue, AnimationDesc.MinValue, AnimationDesc.MaxValue);

            if (MathHelper.Distance(CurrentValue, PreviousNotifiedValue) > AnimationDesc.NotifyThreshold)
            {
                RaisePropertyChangedForGroup(XamlAnimationGroupName, AnimationDesc.SecondaryGroupName);
                PreviousNotifiedValue = CurrentValue;
            }
        }

        public void Reset()
        {
            PreviousNotifiedValue = float.PositiveInfinity;
            InternalAnimator.Reset();
        }
    }

    public class XamlBoundOpacityAnimation : XamlBoundAnimation
    {
        public XamlBoundOpacityAnimation(XamlAnimationDescriptor AnimationDesc, Action<string, string> RaisePropertyChangedForGroup)
            : base(AnimationDesc, RaisePropertyChangedForGroup)
        {

        }

        [PropertyGroup(XamlAnimationGroupName)]
        public Visibility Visibility
        {
            get
            {
                return CurrentValue > AnimationDesc.NotifyThreshold ? Visibility.Visible : Visibility.Hidden;
            }
        }
    }

    public class XamlAnimationDescriptor
    {
        public float MinValue = float.NegativeInfinity;
        public float MaxValue = float.PositiveInfinity;
        public float TransitionDuration = 1;
        public float NotifyThreshold = 0.01f;
        public string SecondaryGroupName = null;
    }
}
