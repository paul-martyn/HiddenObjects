using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Services.RemoteData
{
    public interface IRemoteResourceLoader : IService
    { 
        public Task LoadData (string url, Action<LoadingStatus, string> callback);
        public Task LoadImage (string url, Action<LoadingStatus, Sprite> callback);
    }
}