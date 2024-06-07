namespace CodeBase.Services.Progress
{
    public interface IProgressService : IService
    {
        public Data.Progress.PersistentProgress Progress { get; set; }
    }
}
