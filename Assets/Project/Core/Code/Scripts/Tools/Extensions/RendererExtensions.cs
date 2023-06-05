using UnityEngine;

namespace SpaceInvaders.Tools.Extensions
{
    public static class RendererExtensions
    {
        public static bool EveryoneIsInvisible(this Renderer[] renderers)
        {
            foreach (var renderer in renderers)
            {
                if (renderer.isVisible)
                {
                    return false;
                }
            }

            return true;
        }
    }
}