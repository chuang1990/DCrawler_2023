using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    private ITraversableTilemap m_Tilemap;

    public PathFinder(ITraversableTilemap tilemap)
    {
        m_Tilemap = tilemap;
	}

    public Vector2Int[] FindPath(Vector2Int from, Vector2Int to)
    {
        var closedSet = new HashSet<Vector2Int>();
        var openSet = new HashSet<Vector2Int> { from };
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var gScore = new Dictionary<Vector2Int, float> { { from, 0 } };
        var fScore = new Dictionary<Vector2Int, float> { { from, HeuristicCostEstimate(from, to) } };

        while (openSet.Count > 0)
        {
            var current = openSet.OrderBy(x => fScore[x]).First();

            if (current == to)
            {
                return ReconstructPath(cameFrom, current);
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbour in m_Tilemap.GetNeighbours(current))
            {
                if (closedSet.Contains(neighbour))
                {
                    continue;
                }

                var tentativeGScore = gScore[current] + 1;

                if (!openSet.Contains(neighbour))
                {
                    openSet.Add(neighbour);
                }
                else if (tentativeGScore >= gScore[neighbour])
                {
                    continue;
                }

                cameFrom[neighbour] = current;
                gScore[neighbour] = tentativeGScore;
                fScore[neighbour] = gScore[neighbour] + HeuristicCostEstimate(neighbour, to);
            }
        }

        return new Vector2Int[0];
    }

    private static float HeuristicCostEstimate(Vector2Int from, Vector2Int to)
    {
        return Mathf.Abs(from.x - to.x) + Mathf.Abs(from.y - to.y);
    }

    private static Vector2Int[] ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        var totalPath = new List<Vector2Int> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);
        }

        totalPath.Reverse();

        return totalPath.ToArray();
    }
}
