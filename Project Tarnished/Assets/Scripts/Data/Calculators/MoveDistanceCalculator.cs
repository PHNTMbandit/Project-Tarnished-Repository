using System.Collections.Generic;
using UnityEngine;

namespace ProjectTarnished.Data.Calculators
{
    public static class MoveDistanceCalculator
    {
        public static float CalculatePathDistance(List<Vector3> path)
        {
            float pathDistance = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                pathDistance += Vector3.Distance(path[i], path[i + 1]);
            }

            return pathDistance;
        }
    }
}