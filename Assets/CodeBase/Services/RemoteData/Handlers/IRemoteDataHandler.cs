namespace CodeBase.Services.RemoteData.Handlers
{
    public interface IRemoteDataHandler
    {
        public void OnLoadStateChanged (LoadingStatus status, string response);
    }
}