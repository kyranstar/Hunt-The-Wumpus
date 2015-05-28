using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.Helpers
{

    /// <summary>
    /// A class that holds methods of pathfinding from one room to another
    /// </summary>
    public class Pathfinding
    {
        /// <summary>
        /// Finds a path from one room to another using the given algorithm, or A* by default. Returns null if a path is not found.
        /// </summary>
        /// <param name="start">The starting point</param>
        /// <param name="end">The end point</param>
        /// <param name="algorithm">The algorithm to find a path with</param>
        /// <returns>A list of rooms making a path, or null if there is not path. Does not contain the start point, but contains the end point. </returns>
        public static List<Room> FindPath(Room start, Room end, Cave cave, bool avoidHazards)
        {
            if (start.RoomID == end.RoomID) return null;

            // Maximum possible path goes through all rooms, so set the size to that
            IPriorityQueue<AStarNode> openNodes = new HeapPriorityQueue<AStarNode>(cave.Rooms.Length);
            IList<AStarNode> closedNodes = new List<AStarNode>();

            // Add the start node with an F cost of 0
            openNodes.Enqueue(new AStarNode(start), 0);

            while (openNodes.Count != 0)
            {
                //The one with the least F cost
                AStarNode current = openNodes.Dequeue();
                closedNodes.Add(current);

                foreach (AStarNode neighbor in getNeighbors(current, cave))
                {
                    // if we already processed this node, skip it
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
                    // if we are not avoiding the hazard in this room
                    else if (!(avoidHazards && (neighbor.node.HasBats || neighbor.node.HasPit)))
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
        /// <summary>
        /// Gives the estimated f-cost between two rooms. Uses the manhattan distance.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="cave"></param>
        /// <returns></returns>
        private static int GetEstimatedScore(Room start, Room end, Cave cave)
        {
            Vector2 startPos = cave.RoomLayout[start.RoomID].RoomPosition;
            Vector2 endPos = cave.RoomLayout[end.RoomID].RoomPosition;
            // Manhattan distance
            return (Math.Abs(startPos.X - endPos.X) + Math.Abs(startPos.Y - endPos.Y)).ToInt();
        }
        /// <summary>
        /// Returns the neighbor rooms of a room.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="cave"></param>
        /// <returns></returns>
        private static IEnumerable<AStarNode> getNeighbors(AStarNode center, Cave cave)
        {
            return center.node.AdjacentRooms.Where(i => i != -1).Select(roomIndex => new AStarNode(cave.GetRoom(roomIndex), center));
        }
        /// <summary>
        /// A node for the AStar algorithm. Holds all parents.
        /// </summary>
        private class AStarNode : PriorityQueueNode
        {
            public readonly Room node;
            public readonly AStarNode parent;

            public AStarNode(Room node)
            {
                this.node = node;
                parent = null;
            }

            public AStarNode(Room node, AStarNode parent)
            {
                this.node = node;
                this.parent = parent;
            }
            /// <summary>
            /// The number of parents this node has.
            /// </summary>
            public int ParentCount
            {
                get
                {
                    if (parent == null) return 0;
                    return 1 + parent.ParentCount;
                }
            }

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
}
