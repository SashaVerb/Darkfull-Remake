using UnityEngine;
using Modules.Interactable;
using EnemyMovement = KinematicCharacterController.Examples.EnemyMovement;

public class EnemyInteractable : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;

    private Interactable _interactable;
    
    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
    }
    
    private void OnEnable()
    {
        _interactable.OnActivate.AddListener(Stop);
        _interactable.OnDeactivate.AddListener(MoveOn);
    }

    private void OnDisable()
    {
        _interactable.OnActivate.RemoveListener(Stop);
        _interactable.OnDeactivate.RemoveListener(MoveOn);
    }
    
    public void Stop()
    {
        _enemyMovement.Stop();
        _enemyMovement.enabled = false;
    }

    public void MoveOn()
    {
        _enemyMovement.enabled = true;
    }

    private void OnValidate()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
    }
}
