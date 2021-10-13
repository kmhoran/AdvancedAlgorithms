using System;
using System.Collections.Generic;
using System.Text;
using Structures;
using Structures.Enums;

namespace Scenarios.PriorityQueue
{
    /**
     * Priority queues are crucial to implementing Dijkstra and A* algorithms, described in detail 
     *   in chapter 14, sections 14.4 and 14.5. [The] running time of these fundamental algorithms 
     *   on graphs (which compute the minimum distance to a target) heavily depends on the 
     *   implementation chosen for the priority queue, and upgrading from a binary to a d-ary heap 
     *   can provide a consistent speedup.
     *
     * - La Rocca
     */

    public static class AStarPathfinding
    {
        public static void Go(int mapSize)
        {
            Console.WriteLine($"Found shortest path on a {mapSize}x{mapSize} grid");
            Random rando = new();
            Coordinates startCoords = new()
            {
                X = rando.Next(0, mapSize),
                Y = rando.Next(0, mapSize),
            };
            Coordinates goalCoords = new()
            {
                X = rando.Next(0, mapSize),
                Y = rando.Next(0, mapSize),
            };

            if (startCoords.Equals(goalCoords))
            {
                Console.WriteLine($"What are the odds ( 1/{mapSize * mapSize} )? Start and goal are the same. Nothing to see here...");
                return;
            }

            string startId = GetCoodinateId(startCoords);

            PathStep staringStep = new()
            {
                Id = startId,
                Location = startCoords,
                DistanceFromStart = 0,
                DistanceToGoal = Distance(startCoords, goalCoords)
            };

            var openQueue = new PriorityQueue<PathStep>(new PriorityQueueOptions { HeapType = HeapType.MinHeap, MaxLeafCount = 4 });
            var priorityMap = new Dictionary<string, int>();
            var closedList = new Dictionary<string, bool>();


            int startPriority = staringStep.Priority;
            openQueue.Insert(staringStep, startPriority);
            priorityMap.Add(startId, startPriority);
            closedList.Add(startId, true);

            PathStep goal = null;

            while (openQueue.Count > 0)
            {
                var currentStep = openQueue.Top();
                if (currentStep.Location.Equals(goalCoords))
                {
                    goal = currentStep;
                    break;
                }
                for (int x = -1; x < 2; x++)
                {
                    for (int y = 1; y > -2; y--)
                    {
                        if (x == 0 && y == 0) continue;

                        Coordinates neighborCoords = new()
                        {
                            X = currentStep.Location.X + x,
                            Y = currentStep.Location.Y + y
                        };
                        if (neighborCoords.X >= mapSize || neighborCoords.Y >= mapSize) continue;
                        if (neighborCoords.X < 0 || neighborCoords.Y < 0) continue;
                        string id = GetCoodinateId(neighborCoords);
                        if (closedList.ContainsKey(id)) continue;

                        var neighbor = new PathStep()
                        {
                            Id = id,
                            Parent = currentStep,
                            Location = neighborCoords,
                            DistanceFromStart = currentStep.DistanceFromStart + 1,
                            DistanceToGoal = Distance(neighborCoords, goalCoords)
                        };
                        int neighborPriority = neighbor.Priority;
                        if (priorityMap.ContainsKey(id))
                            if (priorityMap[id] < neighborPriority) continue;

                        priorityMap[id] = neighborPriority;
                        openQueue.Insert(neighbor, neighborPriority);
                    }
                }
                closedList[currentStep.Id] = true;
            }

            if (goal == null) Console.WriteLine("Could not Find Solution :(");

            char[,] map = new char[mapSize, mapSize];

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = mapSize - 1; y >= 0; y--)
                {
                    map[x, y] = '#';
                }
            }

            map[startCoords.X, startCoords.Y] = 'S';
            map[goalCoords.X, goalCoords.Y] = 'G';

            var current = goal.Parent;
            while (current != null)
            {
                if (current.Location.Equals(startCoords)) break;
                map[current.Location.X, current.Location.Y] = '*';
                current = current.Parent;
            }

            StringBuilder sb = new();
            for (int y = mapSize - 1; y >= 0; y--)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    sb.Append(map[x, y]);
                }
                sb.Append('\n');
            }
            Console.WriteLine(sb.ToString());
        }

        static int Distance(Coordinates a, Coordinates b)
        {
            int x = (a.X * 100) - (b.X * 100);
            int y = (a.Y * 100) - (b.Y * 100);
            return (int)Math.Floor(Math.Sqrt(x * x + y * y));
        }
        static string GetCoodinateId(Coordinates coords)
        {
            return $"x{coords.X}y{coords.Y}";
        }


    }

    internal struct Coordinates
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        internal bool Equals(Coordinates other)
          => X == other.X && Y == other.Y;
    }

    internal class PathStep : IEquatable<PathStep>
    {
        internal string Id { get; set; }
        internal PathStep Parent { get; set; }
        internal Coordinates Location { get; set; }
        internal int DistanceFromStart { get; set; }
        internal int DistanceToGoal { get; set; }
        internal int Priority => DistanceFromStart + DistanceToGoal;

        public bool Equals(PathStep other)
        {
            if (!Location.Equals(other.Location)) return false;
            if (DistanceFromStart != other.DistanceFromStart) return false;
            if (Parent != other.Parent) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PathStep);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

}