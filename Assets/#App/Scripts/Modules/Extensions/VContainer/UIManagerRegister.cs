using UIManagement;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Modules.Extensions.VContainer
{
    public static class UIManagerRegister
    {
        public static void RegisterUI<T>(this IContainerBuilder builder, T uiPanel) where T : UIPanel
        {
            builder.Register(resolver =>
            {
                var instance = UIManager.Instantiate(uiPanel);
                resolver.InjectGameObject(instance.gameObject);
                return instance;
            }, Lifetime.Singleton);
        }
    }
}