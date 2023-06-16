using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Components
{
    public class RenderersBoundsComposer
    {
        private readonly Dictionary<GameObject, Renderer[]> _renderers = new();

        public void IncludeRenderersOf(GameObject rootGameObject)
        {
            if (_renderers.ContainsKey(rootGameObject)) return;

            _renderers[rootGameObject] = rootGameObject.GetComponentsInChildren<Renderer>();
        }

        public void ExcludeRenderersOf(GameObject rootGameObject)
        {
            if (!_renderers.ContainsKey(rootGameObject)) return;

            _renderers.Remove(rootGameObject);
        }

        public Bounds? Compose()
        {
            Bounds? result = null;

            foreach (var renderers in _renderers.Values)
            {
                if (renderers.Length > 0)
                {
                    if (!result.HasValue)
                    {
                        result = renderers[0].bounds;
                    }

                    for (int i = 0, n = renderers.Length; i < n; i++)
                    {
                        var bounds = result.Value;
                        bounds.Encapsulate(renderers[i].bounds);
                        result = bounds;
                    }
                }
            }

            return result;
        }
    }
}