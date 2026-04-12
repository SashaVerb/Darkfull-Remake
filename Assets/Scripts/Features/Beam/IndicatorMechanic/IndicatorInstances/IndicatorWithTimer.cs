using System;
using UnityEngine;
using UnityEngine.Events;

namespace Features.Beam.IndicatorMechanic.IndicatorInstances
{
    public class IndicatorWithTimer : MonoBehaviour, IIndicator
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _rotSpeed = 1f;

        [field: SerializeField] public UnityEvent OnActivate { get; private set; }
        [field: SerializeField] public UnityEvent OnDeactivate { get; private set; }

        private float _timer;
        private bool _activating = false;
        private bool _wasActive = false;

        private void Update()
        {
            if (_activating)
            {
                if (!_wasActive)
                {
                    _timer += Time.deltaTime;

                    if (_timer >= _duration)
                    {
                        _timer = _duration;
                        _wasActive = true;
                        OnActivate?.Invoke();
                    }
                    else
                    {
                        transform.Rotate(Vector3.up * Time.deltaTime * _rotSpeed);
                    }
                }
            }
            else
            {
                _timer -= Time.deltaTime;

                if (_timer <= 0f)
                {
                    _timer = 0f;
                }
                else
                {
                    transform.Rotate(Vector3.down * Time.deltaTime * _rotSpeed);
                }
            }
        }

        public void Activate()
        {
            _activating = true;
        }

        public void Deactivate()
        {
            _activating = false;

            if (_wasActive)
            {
                OnDeactivate?.Invoke();
                _wasActive = false;
            }
        }
    }
}