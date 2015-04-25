using Microsoft.Xna.Framework;
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
        public static List<Room> FindPath(Room start, Room end, Cave cave, PathfindingAlgorithm algorithm = PathfindingAlgorithm.A_STAR)
        {
            switch (algorithm)
            {
                case PathfindingAlgorithm.A_STAR: return FindAStarPath(start, end, cave);

                default: throw new Exception();
            }
        }

        private static List<Room> FindAStarPath(Room start, Room end, Cave cave)
        {
            int MAX_TRAVERSED_ROOMS = cave.getRoomDict().Count;
            IPriorityQueue<AStarNode<Room>> openNodes = new HeapPriorityQueue<AStarNode<Room>>(MAX_TRAVERSED_ROOMS);
            List<AStarNode<Room>> closedNodes = new List<AStarNode<Room>>();

            Func<AStarNode<Room>, IEnumerable<AStarNode<Room>>> getNeighbors = (r) =>
            {
                return new List<int>(r.node.adjacentRooms).Select((a) => new AStarNode<Room>(cave.GetRoom(a), r));
            };
            // Add the start node with an F cost of 0
            openNodes.Enqueue(new AStarNode<Room>(start), 0);

            while (openNodes.Count != 0)
            {
                //The one with the least F cost
                AStarNode<Room> current = openNodes.Dequeue();
                closedNodes.Add(current);

                foreach (AStarNode<Room> neighbor in getNeighbors(current))
                {
                    // if we already processed this node
                    if (closedNodes.Contains<AStarNode<Room>>(neighbor)) continue;

                    int fCost = GetEstimatedScore(neighbor.node, end, cave) + neighbor.ParentCount;

                    if (openNodes.Contains<AStarNode<Room>>(neighbor))
                    {
                        double priority = -1;
                        foreach (AStarNode<Room> node in openNodes)
                        {
                            if (node.Equals(neighbor))
                            {
                                priority = node.Priority;
                                break;
                            }
                        }
                        if (fCost < priority)
                        {
                            openNodes.UpdatePriority(neighbor, fCost);
                        }
                    }
                    else
                    {
                        openNodes.Enqueue(neighbor, fCost);
                        if (neighbor.node.Equals(end))
                        {
                            // found the path
                            List<Room> path = new List<Room>();
                            AStarNode<Room> currentNode = neighbor;
                            while (currentNode.parent != null)
                            {
                                path.Insert(0, currentNode.node);
                                currentNode = currentNode.parent;
                            }
                            return path;
                        }
                    }
                }

            }
            // path not found
            return null;
        }
        private static int GetEstimatedScore(Room start, Room end, Cave cave)
        {
            Vector2 startPos = cave.RoomLayout[start.roomId].RoomPosition;
            Vector2 endPos = cave.RoomLayout[end.roomId].RoomPosition;
            // Manhattan distance
            return (int)Math.Round(Math.Abs(startPos.X - endPos.X) + Math.Abs(startPos.Y - endPos.Y));
        }
    }
    class AStarNode<T> : PriorityQueueNode
    {
        public AStarNode(T node)
        {
            this.node = node;
            this.parent = null;
        }

        public AStarNode(T node, AStarNode<T> parent)
        {
            this.node = node;
            this.parent = parent;
        }

        public T node;
        public AStarNode<T> parent;

        public int ParentCount
        {
            get
            {
                if (parent == null) return 0;
                return 1 + parent.ParentCount;
            }
        }

        //Should this method compare both the nodes and the parents or just the nodes?
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            bool sameKey = node.Equals(((AStarNode<T>)obj).node);

            if (sameKey && node.Equals(default(T)))
                return ReferenceEquals(this, obj);

            return sameKey;
        }

        public override int GetHashCode()
        {
            if (node.Equals(default(T)))
                return base.GetHashCode();

            return GetType().GetHashCode() ^ node.GetHashCode();
        }
    }
    public enum PathfindingAlgorithm
    {
        A_STAR,
    }
}
