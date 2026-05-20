using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Modules.Extensions.VContainer
{
    public static class PrefabInstallerRegister
    {
        public static T RegisterPrefabInstaller<T>(this IContainerBuilder builder, T prefab, Vector3 position, Quaternion rotation)
            where T : MonoBehaviour, IInstaller
        {
            if (builder == null) throw new System.ArgumentNullException(nameof(builder));
            if (prefab == null) throw new System.ArgumentNullException(nameof(prefab));

            var wasActive = prefab.gameObject.activeSelf;
            if (wasActive)
            {
                prefab.gameObject.SetActive(false);
            }

            var instance = Object.Instantiate(prefab, position, rotation);
            instance.Install(builder);

            if (wasActive)
            {
                prefab.gameObject.SetActive(true);
            }
            
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.InjectGameObject(instance.gameObject);
                if (wasActive)
                {
                    instance.gameObject.SetActive(true);
                }
            });

            return instance;
        }
    }
}
