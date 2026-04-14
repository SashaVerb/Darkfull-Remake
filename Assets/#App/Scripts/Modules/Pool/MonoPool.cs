using UnityEngine;
using UnityEngine.Pool;

public class MonoPool<T> : IPool<T> where T : Component
{
    private readonly Transform _parent;
    private readonly T _prefab;
    private readonly ObjectPool<T> _pool;

    public MonoPool(T prefab, Transform parent = null, int defaultCapacity = 10)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroy,
            defaultCapacity: defaultCapacity
        );
    }

    public T Get() => _pool.Get();

    public void Release(T behaviour) => _pool.Release(behaviour);

    private T Create()
    {
        return Object.Instantiate(_prefab, _parent);
    }

    private void OnGet(T behaviour)
    {
        behaviour.gameObject.SetActive(true);   
    }

    private void OnRelease(T behaviour)
    {
        behaviour.gameObject.SetActive(false);   
    }
    
    private void OnDestroy(T behaviour)
    {
        Object.Destroy(behaviour.gameObject);
    }
}
