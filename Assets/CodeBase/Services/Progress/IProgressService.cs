using CodeBase.Data.Progress;

namespace CodeBase.Services.Progress
{
    public interface IProgressService : IService
    {
        public PersistentProgress Progress { get; set; }
    }
}
