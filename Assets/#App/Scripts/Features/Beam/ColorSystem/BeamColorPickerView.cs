using System;
using UIManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Features.Beam.ColorSystem
{
    public class BeamColorPickerView : UIPanel, IDisposable
    {
        public event Action<BeamColor> OnColorPicked;

        [SerializeField] private ButtonColorBinding[] bindings;

        protected override void Awake()
        {
            base.Awake();
            
            foreach (var binding in bindings)
            {
                BeamColor capturedColor = binding.Color;
                binding.Action = () => OnColorPicked?.Invoke(capturedColor);
            }

            OnColorPicked += _ => Hide();
        }

        private void OnEnable()
        {
            foreach (var binding in bindings)
            {
                binding.Button.onClick.AddListener(binding.Action);
            }
        }

        private void OnDisable()
        {
            foreach (var binding in bindings)
            {
                binding.Button.onClick.RemoveListener(binding.Action);
            }
        }

        private void OnValidate()
        {
            if (bindings == null || bindings.Length == 0)
            {
                var beamColors = Enum.GetValues(typeof(BeamColor));
                bindings = new ButtonColorBinding[beamColors.Length];

                for (int i = 0; i < beamColors.Length; i++)
                {
                    bindings[i] = new ButtonColorBinding
                    {
                        Color = (BeamColor)beamColors.GetValue(i)
                    };
                }
            }
        }

        [Serializable]
        private sealed class ButtonColorBinding
        {
            public Button Button;
            public BeamColor Color;

            [NonSerialized] public UnityAction Action;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}