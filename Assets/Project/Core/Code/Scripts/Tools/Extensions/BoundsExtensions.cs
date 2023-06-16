using UnityEngine;

namespace SpaceInvaders.Tools.Extensions
{
    public static class BoundsExtensions
    {
        public static Bounds ToLocalSpace(this Bounds worldSpaceBounds, Transform transform)
        {
            return new Bounds(transform.transform.InverseTransformPoint(worldSpaceBounds.center), worldSpaceBounds.size);
        }
    }
}