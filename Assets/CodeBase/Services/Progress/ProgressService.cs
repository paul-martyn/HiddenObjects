namespace CodeBase.Services.Progress
{
    public class ProgressService : IProgressService
    {
        public Data.Progress.PersistentProgress Progress { get; set; }
    }
}