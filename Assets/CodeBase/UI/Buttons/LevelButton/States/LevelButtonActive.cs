using CodeBase.Infrastructure.States;
using CodeBase.Services;

namespace CodeBase.UI.Buttons.LevelButton.States
{
    public class LevelButtonActive : LevelButtonState
    {
        private ILevelButtonStateSwitcher _switcher;
        private readonly int _levelID;
        
        public LevelButtonActive(ButtonAddon buttonAddon, int levelID, ILevelButtonStateSwitcher switcher) : base(buttonAddon)
        {
            _levelID = levelID;
            _switcher = switcher;
        }

        protected override void Init()
        {
        }

        public override void Enter()
        {
            ButtonAddon.SetActive(true);
        }

        public override void Exit()
        {
        }

        public override void OnClick() => 
            AllServices.Container.Single<IGameStateMachine>().Enter<LoadLevelState, LevelPayload>(new LevelPayload(_levelID, ref _switcher));
    }
}