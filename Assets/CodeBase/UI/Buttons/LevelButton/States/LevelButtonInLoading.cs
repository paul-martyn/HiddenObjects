namespace CodeBase.UI.Buttons.LevelButton.States
{
    public class LevelButtonInLoading : LevelButtonState
    {
        private readonly  LoadingProcessAnimation _loadingProcessAnimation;

        public LevelButtonInLoading(ButtonAddon buttonAddon, LoadingProcessAnimation loadingProcessAnimation) : base(buttonAddon)
        {
            _loadingProcessAnimation = loadingProcessAnimation;
        }

        protected override void Init() => 
            Reset();

        public override void Enter()
        {
            ButtonAddon.SetActive(false);
            _loadingProcessAnimation.Play();
        }

        public override void Exit() => 
            Reset();

        private void Reset() => 
            _loadingProcessAnimation.Stop();
    }
}