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
        private Texture2D DotTexture;
        private SnakePath Path;

        private int Width, Height, GridSize;
        private double SecondsPerMove, LastMoveTime;

        private int PreviousSnakeDirection = 0;

        private Random Random = new Random();

        public WumpusChaseAnimation(int width, int height, int gridSize, int pathLength, double secondsPerMove)
        {
            Width = width;
            Height = height;
            GridSize = gridSize;
            SecondsPerMove = secondsPerMove;

            Path = new SnakePath(pathLength);
        }

        public void LoadContent(ContentManager Content)
        {
            PlayerCharacter = new Sprite2D(Content.Load<Texture2D>("Images/Wumpus"), Scale: 0.1f);
            Wumpus = new Sprite2D(Content.Load<Texture2D>("Images/Character"), Scale: 0.1f);

            PlayerCharacter.AddAnimation(AnimationType.MoveToNewMenuTile, new SpriteMoveAnimation(100));
            Wumpus.AddAnimation(AnimationType.MoveToNewMenuTile, new SpriteMoveAnimation(100));

            PlayerCharacter.Initialize();
            Wumpus.Initialize();

            PlayerCharacter.StartAnimation(AnimationType.MoveToNewMenuTile);
            Wumpus.StartAnimation(AnimationType.MoveToNewMenuTile);

            DotTexture = Content.Load<Texture2D>("Images/Dot");
        }

        public void Update(GameTime time)
        {
            if(time.TotalGameTime.TotalSeconds - LastMoveTime > SecondsPerMove)
            {
                LastMoveTime = time.TotalGameTime.TotalSeconds;

                PickNextPosition();

                (Wumpus.GetAnimation(AnimationType.MoveToNewMenuTile) as SpriteMoveAnimation).TargetPosition = Path.EarliestPoint;
                (PlayerCharacter.GetAnimation(AnimationType.MoveToNewMenuTile) as SpriteMoveAnimation).TargetPosition = Path.LatestPoint;
            }

            Wumpus.Update(time);
            PlayerCharacter.Update(time);
        }

        public void Draw(SpriteBatch target)
        {
            PlayerCharacter.Draw(target);
            Wumpus.Draw(target);

            foreach(Vector2 Point in Path.Points.Skip(1).DropLast())
            {
                target.Draw(DotTexture, position: Point, scale: new Vector2(15));
            }
        }

        private void PickNextPosition()
        {
            Vector2 CurrentPoint = Path.LatestPoint;
            List<Vector2> ValidMoves = new List<Vector2>();

            for(int Direction = 0; Direction < 4; Direction++)
            {
                Vector2 NewPoint = GetPointAtDirection(CurrentPoint, Direction);
                if (ValidateNewPoint(NewPoint))
                    ValidMoves.Add(NewPoint);
            }

            if (ValidMoves.Count <= 0)
                Path = new SnakePath(Path.Length, GetRandomPoint());
            else
            {
                Vector2 MousePos = Mouse.GetState().Position.ToVector2();

                if (IsOutOfBounds(MousePos))
                    Path.AddPoint(ValidMoves.GetRandom());
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
                break;
            case 3:
                return CurrentPoint - new Vector2(GridSize, 0);
        }
        return CurrentPoint;
    }

        private bool ValidateNewPoint(Vector2 NewPoint)
        {
            // Check if we are out-of-bounds
            bool OutOfBounds = IsOutOfBounds(NewPoint);

            List<Vector2> NewPoints = Path.Points.ToList();
            NewPoints.Add(NewPoint);

            int NumDistinctPoints = NewPoints.Distinct().Count();
            int NumDuplicatePoints = NewPoints.Count - NumDistinctPoints;

            return !OutOfBounds && NumDuplicatePoints == 0;
        }

        private bool IsOutOfBounds(Vector2 Point)
        {
            return Point.X < 0 || Point.X > Width || Point.Y < 0 || Point.Y > Height;
        }
    }
}
