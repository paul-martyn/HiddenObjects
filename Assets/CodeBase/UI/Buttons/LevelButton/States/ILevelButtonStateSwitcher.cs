namespace CodeBase.UI.Buttons.LevelButton.States
{
    public interface ILevelButtonStateSwitcher
    {
        public void SwitchState<T>() where T : LevelButtonState;
    }
}