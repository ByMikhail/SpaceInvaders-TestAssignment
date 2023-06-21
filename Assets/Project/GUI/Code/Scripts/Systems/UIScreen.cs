using UnityEngine;

namespace SpaceInvaders.GUI.Code.Scripts.Systems
{
    [DisallowMultipleComponent]
    public class UIScreen : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}