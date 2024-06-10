namespace CodeBase.UI.Buttons.LevelButton.States
{
    public abstract class LevelButtonState
    {
        protected readonly ButtonAddon ButtonAddon;

        protected LevelButtonState(ButtonAddon buttonAddon)
        {
            ButtonAddon = buttonAddon;
        }

        protected abstract void Init();
        public abstract void Enter();
        public abstract  void Exit();

        public virtual void OnClick()
        {
        }
    }
}