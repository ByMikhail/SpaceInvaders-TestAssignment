using SpaceInvaders.Tools.Extensions;
using TMPro;
using UnityEngine;

namespace SpaceInvaders.GUI.Code.Scripts.Views.Base
{
    public abstract class PropertyField<TValue> : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _labelText;

        public string Label
        {
            get => _label;
            set => value.AssignTo(ref _label, HandleOnLabelChanged);
        }

        public TValue Value
        {
            get => _value;
            set => value.AssignTo(ref _value, HandleOnValueChanged);
        }

        private string _label;
        private TValue _value;

        protected void Awake()
        {
            HandleOnValueChanged();
        }

        protected virtual void HandleOnLabelChanged()
        {
            _labelText.text = _label;
        }

        protected abstract void HandleOnValueChanged();
    }
}