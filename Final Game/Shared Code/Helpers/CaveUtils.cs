using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheWumpus.SharedCode.Helpers
{
    public static class CaveUtils
    {
        public static CaveLayoutStatus CheckIfValid(IDictionary<int, Room> Cave, int NumMinConnections = -1, int NumMaxConnections = -1)
        {

            // Make sure that all connections lead to valid rooms,
            //  and that all connections are bi-directional
            bool AllConnectionsValid = Cave.Values.All(
                   e => e.AdjacentRooms.All(
                       r => r == -1 || (Cave.ContainsKey(r) && Cave[r].AdjacentRooms.Contains(e.RoomID))
                   ));

            Dictionary<int, int> ValidatedRooms = Cave.ToDictionary(pair => pair.Key, pair => 0);
            List<int> UnmarkedRooms = ValidatedRooms.Keys.ToList();

            Action<Room> MarkConnectedRooms = null;
            MarkConnectedRooms = (Room Room) =>
            {
                if (!UnmarkedRooms.Contains(Room.RoomID))
                    return;
                UnmarkedRooms.Remove(Room.RoomID);

                foreach (int ID in (from TmpID in Room.AdjacentRooms where TmpID >= 0 select TmpID))
                {
                    ValidatedRooms[ID]++;
                    MarkConnectedRooms(Cave[ID]);
                }

            };

            MarkConnectedRooms(Cave.First().Value);

            int[] UnreachableRooms = UnmarkedRooms.ToArray();

            int[] TooFewConnections = ValidatedRooms
                .Where(Pair => NumMinConnections != -1 && Pair.Value < NumMinConnections)
                .Select(Pair => Pair.Key)
                .ToArray();

            int[] TooManyConnections = ValidatedRooms
                .Where(Pair => NumMaxConnections != -1 && Pair.Value > NumMaxConnections)
                .Select(Pair => Pair.Key)
                .ToArray();

            CaveLayoutStatus Result = CaveLayoutStatus.None;

            if (UnreachableRooms.Length > 0)
                Result |= CaveLayoutStatus.UnreachableRooms;

            if (TooFewConnections.Length > 0)
                Result |= CaveLayoutStatus.TooFewConnections;

            if (TooManyConnections.Length > 0)
                Result |= CaveLayoutStatus.TooManyConnections;

            if (!AllConnectionsValid)
                Result |= CaveLayoutStatus.MismatchedConnections;

            return Result;
        }
    }
}
