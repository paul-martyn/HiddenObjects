using UnityEngine;

namespace CodeBase.Services.RemoteData.Handlers
{
    public interface IRemoteImageHandler
    {
        public void OnLoadStateChanged (LoadingStatus status, Sprite remoteResource);
    }
}
 