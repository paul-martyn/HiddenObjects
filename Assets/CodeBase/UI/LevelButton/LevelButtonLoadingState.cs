namespace CodeBase.UI.LevelButton
{
    public class LevelButtonLoadingState : LevelButtonState
    {
        private readonly  LoadingProcessAnimation _loadingProcessAnimation;

        public LevelButtonLoadingState(LoadingProcessAnimation loadingProcessAnimation)
        {
            _loadingProcessAnimation = loadingProcessAnimation;
        }

        public override void Enter()
        {
            _loadingProcessAnimation.Play();
        }

        public override void Exit()
        {
            _loadingProcessAnimation.Stop();
        }
    }
}