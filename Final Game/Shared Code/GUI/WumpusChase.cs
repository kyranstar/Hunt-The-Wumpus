using HuntTheWumpus.SharedCode.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.GUI
{
    public class WumpusChaseAnimation
    {
        private Sprite2D Wumpus, PlayerCharacter;
        private Texture2D WumpusTexture, PlayerTexture, DotTexture;
        private SnakePath Path;

        private int Width, Height, GridSize;
        private double SecondsPerMove, LastMoveTime;

        private int PreviousSnakeDirection = 0;

        private const float DotScale = 0.03f;
        private readonly Vector2 DotScaleVector = new Vector2(DotScale);
        private Vector2 DotOffset;

        private Random Random = new Random();

        public WumpusChaseAnimation(int width, int height, int gridSize, int pathLength, double secondsPerMove)
        {
            Width = width;
            Height = height;
            GridSize = gridSize;
            SecondsPerMove = secondsPerMove;

            Path = new SnakePath(pathLength, GetPointOfInterest());
        }

        public void LoadContent(ContentManager Content)
        {
            // Load the textures
            DotTexture = Content.Load<Texture2D>("Images/Dot");
            WumpusTexture = Content.Load<Texture2D>("Images/Wumpus");
            PlayerTexture = Content.Load<Texture2D>("Images/Character");

            DotOffset.X = DotTexture.Width / 2f * DotScale;
            DotOffset.Y = DotTexture.Height / 2f * DotScale;
        }

        public void Initialize()
        {
            // Create the sprites for the Wumpus and character
            PlayerCharacter = new Sprite2D(PlayerTexture, Scale: 0.1f);
            Wumpus = new Sprite2D(WumpusTexture, Scale: 0.1f);

            PlayerCharacter.CenterOrigin();
            Wumpus.CenterOrigin();

            // Add the movement animations to make it less abrupt
            PlayerCharacter.AddAnimation(AnimationType.MoveToNewMenuTile, new SpriteMoveAnimation(dist => dist / 75f));
            Wumpus.AddAnimation(AnimationType.MoveToNewMenuTile, new SpriteMoveAnimation(dist => dist / 75f));

            // Initialize the animations
            PlayerCharacter.Initialize();
            Wumpus.Initialize();

            // Start the animations
            PlayerCharacter.StartAnimation(AnimationType.MoveToNewMenuTile);
            Wumpus.StartAnimation(AnimationType.MoveToNewMenuTile);
        }

        public void Update(GameTime time)
        {
            // Only update periodically (not every frame)
            if(time.TotalGameTime.TotalSeconds - LastMoveTime > SecondsPerMove)
            {
                // Update the last updated time
                LastMoveTime = time.TotalGameTime.TotalSeconds;

                // Add the new position
                PickNextPosition();

                // Set the new animation targets
                (Wumpus.GetAnimation(AnimationType.MoveToNewMenuTile) as SpriteMoveAnimation).TargetPosition = Path.LatestPoint;
                (PlayerCharacter.GetAnimation(AnimationType.MoveToNewMenuTile) as SpriteMoveAnimation).TargetPosition = Path.EarliestPoint;
            }

            // Update the animations every frame
            Wumpus.Update(time);
            PlayerCharacter.Update(time);
        }

        public void Draw(SpriteBatch target)
        {
            // Draw the dots
            foreach (Vector2 Point in Path.Points.Skip(1).DropLast())
                target.Draw(DotTexture, position: Point - DotOffset, scale: DotScaleVector);

            // Draw the sprites
            PlayerCharacter.Draw(target);
            Wumpus.Draw(target);
        }

        /// <summary>
        /// Gets a new point of interest. The POI is the mouse position
        /// if it is inside the window, or a random point otherwise.
        /// </summary>
        /// <returns>The new target</returns>
        private Vector2 GetPointOfInterest()
        {
            Vector2 MousePos = Mouse.GetState().Position.ToVector2();

            // If the mouse is out of the window, just pick a random point
            if (IsOutOfBounds(MousePos))
                return GetRandomPoint();
            // Otherwise, return the mouse's position
            else
                return MousePos;
        }

        private void PickNextPosition()
        {
            // Start with the current point as a default
            Vector2 CurrentPoint = Path.LatestPoint;

            // Create a list to store the valid moves
            List<Vector2> ValidMoves = new List<Vector2>();

            // Try each direction
            for(int Direction = 0; Direction < 4; Direction++)
            {
                // Calculate the target point after the move
                Vector2 NewPoint = GetPointAtDirection(CurrentPoint, Direction);
                // Add it to the list if it is valid
                if (ValidateNewPoint(NewPoint))
                    ValidMoves.Add(NewPoint);
            }
            
            // If there aren't any valid moves (we have trapped ourselves)
            // restart at a random position
            if (ValidMoves.Count <= 0)
                Path = new SnakePath(Path.Length, GetPointOfInterest());
            else
            {
                // Get the mouse position
                Vector2 MousePos = Mouse.GetState().Position.ToVector2();

                // If it isn't in our game window, just move randomly
                if (IsOutOfBounds(MousePos))
                    Path.AddPoint(ValidMoves.GetRandom());
                // If the mouse is in the window, find the closest valid move and
                // add it to the snake path
                else
                    Path.AddPoint(ValidMoves.OrderBy(p => p.Distance(MousePos)).First());
            }
        }

        private Vector2 GetRandomPoint()
        {
            return new Vector2(Random.Next(Width), Random.Next(Height));
        }

        private Vector2 GetPointAtDirection(Vector2 CurrentPoint, int Direction)
    {
        switch (Direction)
        {
            case 0:
                return CurrentPoint - new Vector2(0, GridSize);
            case 1:
                return CurrentPoint + new Vector2(GridSize, 0);
            case 2:
                return CurrentPoint + new Vector2(0, GridSize);
            case 3:
                return CurrentPoint - new Vector2(GridSize, 0);
        }
        return CurrentPoint;
    }

        private bool ValidateNewPoint(Vector2 NewPoint)
        {
            // Check if we are out-of-bounds
            bool OutOfBounds = IsOutOfBounds(NewPoint);

            // Get the list of points that we would have if we
            // added the test point
            List<Vector2> NewPoints = Path.Points.ToList();
            NewPoints.Add(NewPoint);

            // Count the destinct points
            int NumDistinctPoints = NewPoints.Distinct().Count();
            // Count the duplicate (non-destinct) points
            int NumDuplicatePoints = NewPoints.Count - NumDistinctPoints;

            // Decide if we've met the conditions
            return !OutOfBounds && NumDuplicatePoints == 0;
        }

        private bool IsOutOfBounds(Vector2 Point)
        {
            // Check if the point is within our target area
            return Point.X < 0 || Point.X > Width || Point.Y < 0 || Point.Y > Height;
        }
    }
}
