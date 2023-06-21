using SpaceInvaders.GUI.Code.Scripts.Views.Base;
using TMPro;
using UnityEngine;

namespace SpaceInvaders.GUI.Code.Scripts.Views
{
    public class PropertyFieldInt : PropertyField<int>
    {
        [SerializeField] private TextMeshProUGUI _valueText;

        protected override void HandleOnValueChanged()
        {
            _valueText.text = Value.ToString();
        }
    }
}