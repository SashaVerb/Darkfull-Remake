using System;
using UnityEngine;

namespace _App.Scripts.Features.Death
{
    public class PlayerHealth : MonoBehaviour
    {
        [field: SerializeField] public float MaxHp  { get; private set; } = 1f;
        [field: SerializeField] public float RegenDelay { get; private set; } = 1f;
        [field: SerializeField] public float RegenPerSecond { get; private set; } = 1f;
        public float CurrentHp { get; private set; }
        public bool IsDead { get; private set; } = false;

        public event Action OnDeath;
        
        private float _timeSinceLastDamage;

        private void Awake()
        {
            CurrentHp = MaxHp;
        }

        private void Update()
        {
            if(IsDead)
                return;
            
            _timeSinceLastDamage += Time.deltaTime;

            if (_timeSinceLastDamage >= RegenDelay && CurrentHp > 0f && CurrentHp < MaxHp)
                CurrentHp = Mathf.Min(CurrentHp + RegenPerSecond * Time.deltaTime, MaxHp);
        }

        public void TakeDamage(float amount)
        {
            if(IsDead)
                return;
            
            CurrentHp -= amount;
            _timeSinceLastDamage = 0f;

            if (CurrentHp > 0f)
                return;

            InstantKill();
        }

        public void InstantKill()
        {
            if(IsDead)
                return;
            
            IsDead = true;
            CurrentHp = 0f;
            _timeSinceLastDamage = 0f;
            OnDeath?.Invoke();
            Debug.Log("Death");
        }
    }
}