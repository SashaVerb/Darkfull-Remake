using VContainer;

namespace Modules.Interactable
{
    public interface IInteractionContext
    {
        void Provide(IObjectResolver resolver);
        void Revoke();
    }
}
