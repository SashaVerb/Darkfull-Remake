namespace Modules.Interactable
{
    public class InstantInteractable : Interactable
    {
        public override void Activate()
        {
            if(IsActive)
                return;
            
            IsActive = true;
            OnActivate?.Invoke();
        }

        public override void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;
            OnDeactivate?.Invoke();
        }
    }
}
