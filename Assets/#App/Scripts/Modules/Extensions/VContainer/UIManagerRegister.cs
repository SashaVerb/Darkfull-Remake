using UIManagement;
using VContainer;
using VContainer.Unity;

namespace _App.Scripts.Modules.Extensions.VContainer
{
    public static class UIManagerRegister
    {
        public class UIRegistrationBuilder<T> where T : UIPanel
        {
            private readonly IContainerBuilder _builder;

            public UIRegistrationBuilder(IContainerBuilder builder)
            {
                _builder = builder;
            }

            public void NonLazy()
            {
                _builder.RegisterBuildCallback(resolver => resolver.Resolve<T>());
            }
        }

        public static UIRegistrationBuilder<T> RegisterUI<T>(this IContainerBuilder builder, T uiPanelPrefab) where T : UIPanel
        {
            builder.Register(resolver =>
            {
                var wasActive = uiPanelPrefab.gameObject.activeSelf;
                if (wasActive)
                {
                    uiPanelPrefab.gameObject.SetActive(false);
                }

                var instance = UIManager.Instantiate(uiPanelPrefab);
                
                resolver.InjectGameObject(instance.gameObject);
                
                if (wasActive)
                {
                    uiPanelPrefab.gameObject.SetActive(true);
                    instance.gameObject.SetActive(true);
                }
                
                return instance;
            }, Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            
            builder.RegisterBuildCallback(ForceCreateUI<T>);
            
            return new UIRegistrationBuilder<T>(builder);
        }

        private static void ForceCreateUI<T>(IObjectResolver obj) where T : UIPanel
        {
            obj.Resolve<T>();
        }
    }
}