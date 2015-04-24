using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public class Pathfinding
    {
        /// <summary>
        /// Finds a path from one room to another using the given algorithm, or A* by default. Returns null if a path is not found.
        /// </summary>
        /// <param name="start">The starting point</param>
        /// <param name="end">The end point</param>
        /// <param name="algorithm">The algorithm to find a path with</param>
        /// <returns>A list of rooms making a path, or null if there is not path.</returns>
        public List<Room> FindPath(Room start, Room end, Cave cave, PathfindingAlgorithm algorithm = PathfindingAlgorithm.A_STAR)
        {
            switch (algorithm)
            {
                case PathfindingAlgorithm.A_STAR: return FindAStarPath(start, end, cave);

                default: throw new Exception();
            }
        }

        private List<Room> FindAStarPath(Room start, Room end, Cave cave)
        {
            List<AStarNode<Room>> openNodes = new List<AStarNode<Room>>();
            List<Room> closedNodes = new List<Room>();
            Func<Room, IEnumerable<Room>> getNeighbors = (r) =>
            {
                return new List<int>(r.adjacentRooms).Select((a) => cave.GetRoom(a));
            };
            Func<Room, int> getEstimatedScore = (r) =>
            {
                //Manhattan distance. Not sure how to calculate this since rooms don't hold their actual position?
                return 0;
            };

            // Add original neighbors
            openNodes.AddRange(getNeighbors(start).Select((r) => new AStarNode<Room>(r, start)));
            closedNodes.Add(start);

            openNodes.Sort((a, b) =>
            {
                return 0;
            });

            throw new NotImplementedException();
        }
    }
    class AStarNode<T>
    {
        public AStarNode(T node, T parent)
        {
            this.node = node;
            this.parent = parent;
        }

        public T node;
        public T parent;
    }
    public enum PathfindingAlgorithm
    {
        A_STAR,
    }
}
