using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Pacman
{ 
    public class GhostPathFinder
    {
        Level _level;

        public GhostPathFinder(Level level)
            => _level = level;

        // A* algorithm from https://lsreg.ru/realizaciya-algoritma-poiska-a-na-c/
        public List<Point> FindPath(Point previousLocation, Point start, Point goal)
        {
            // step 1
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();
            // step 2
            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                // step 3
                var currentNode = openSet.OrderBy(node 
                    => node.EstimateFullPathLength).First();
                // step 4
                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);

                // step 5
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);
                // step 6
                foreach (var neighbourNode in GetNeighbours(currentNode, goal, previousLocation))
                {
                    // step 7
                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node 
                        => node.Position == neighbourNode.Position);
                    // step 8
                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        // step 9
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }
            // step 10
            return new List<Point>() { start };
        }

        private float GetHeuristicPathLength(Point from, Point to)
             => Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);

        private Collection<PathNode> GetNeighbours(PathNode pathNode, Point goal, Point previousLocation)
        {
            var result = new Collection<PathNode>();

            Point[] neighbourPoints = new Point[4];
            neighbourPoints[0] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[1] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);
            neighbourPoints[3] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);

            foreach (var point in neighbourPoints)
            {
                if (point == previousLocation) 
                    continue;

                if (!_level.IsWalkableForGhost(point))
                    continue;

                PathNode neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart + 1,
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };

                result.Add(neighbourNode);
            }
            return result;
        }

        private List<Point> GetPathForNode(PathNode pathNode)
        {
            List<Point> result = new List<Point>();

            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();

            return result;
        }
    }
}