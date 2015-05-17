﻿using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HuntTheWumpus.SharedCode.GUI
{
    /// <summary>
    /// Uses ease-in/ease-out functions to transition a
    /// floating-point value over time based on a boolean
    /// dtate.
    /// </summary>
    public class StateAnimator
    {
        private Func<double, float> PositiveEasingFunction, NegativeEasingFunction;
        private double AnimationDuration;
        private double StartTime;
        private Latch StateLatch;

        private EdgeType PreviousActiveEdge = EdgeType.None;

        public float CurrentValue { get; protected set; }

        public StateAnimator(Func<double, float> PositiveEasingFunction, Func<double, float> NegativeEasingFunction, double AnimationDuration)
        {
            this.PositiveEasingFunction = PositiveEasingFunction;
            this.NegativeEasingFunction = NegativeEasingFunction;

            this.AnimationDuration = AnimationDuration;

            StateLatch = new Latch();
        }

        public void Update(GameTime Time, bool NewValue)
        {
            EdgeType Edge = StateLatch.ProcessValue(NewValue);

            if (Edge != EdgeType.None)
            {
                PreviousActiveEdge = Edge;
                StartTime = Time.TotalGameTime.TotalSeconds;
            }

            double NewCompletionPct = (Time.TotalGameTime.TotalSeconds - StartTime) / AnimationDuration;
            switch (PreviousActiveEdge)
            {
                case EdgeType.RisingEdge:
                    CurrentValue = PositiveEasingFunction(NewCompletionPct);
                    break;
                case EdgeType.FallingEdge:
                    CurrentValue = PositiveEasingFunction(NewCompletionPct);
                    break;
                default:
                    // TODO: Add param for default value
                    CurrentValue = 0;
                    break;

            }
        }
    }
}
