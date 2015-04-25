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
            IPriorityQueue<AStarNode> openNodes = new HeapPriorityQueue<AStarNode>(MAX_TRAVERSED_ROOMS);
            List<AStarNode> closedNodes = new List<AStarNode>();

            // Add the start node with an F cost of 0
            openNodes.Enqueue(new AStarNode(start), 0);

            while (openNodes.Count != 0)
            {
                //The one with the least F cost
                AStarNode current = openNodes.Dequeue();
                closedNodes.Add(current);

                foreach (AStarNode neighbor in getNeighbors(current, cave))
                {
                    // if we already processed this node
                    if (closedNodes.Contains<AStarNode>(neighbor)) continue;

                    int fCost = GetEstimatedScore(neighbor.node, end, cave) + neighbor.ParentCount;

                    if (openNodes.Contains<AStarNode>(neighbor))
                    {
                        double priority = -1;
                        foreach (AStarNode node in openNodes)
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
                            AStarNode currentNode = neighbor;
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
        private static IEnumerable<AStarNode> getNeighbors(AStarNode center, Cave cave)
        {
            return new List<int>(center.node.adjacentRooms).Select((a) => new AStarNode(cave.GetRoom(a), center));
        }
        private class AStarNode : PriorityQueueNode
        {
            public AStarNode(Room node)
            {
                this.node = node;
                this.parent = null;
            }

            public AStarNode(Room node, AStarNode parent)
            {
                this.node = node;
                this.parent = parent;
            }

            public Room node;
            public AStarNode parent;

            public int ParentCount
            {
                get
                {
                    if (parent == null) return 0;
                    return 1 + parent.ParentCount;
                }
            }

            //Should this method compare both the nodes and the parents or just the nodes?
            public override bool Equals(object other)
            {
                if (other == null)
                    return false;

                if (other.GetType() != GetType())
                    return false;

                return node.Equals(((AStarNode)other).node);
            }

            public override int GetHashCode()
            {
                if (node.Equals(null))
                    return base.GetHashCode();

                return GetType().GetHashCode() ^ node.GetHashCode();
            }
        }
    }

    public enum PathfindingAlgorithm
    {
        A_STAR,
    }
}
