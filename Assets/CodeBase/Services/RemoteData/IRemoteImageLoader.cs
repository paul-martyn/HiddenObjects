using System;
using System.Threading.Tasks;
using CodeBase.UI.UiElements;
using UnityEngine;

namespace CodeBase.Services.RemoteData
{
    public interface IRemoteImageLoader
    { 
        public event Action<LoadingState, Sprite> LoadStateChanged;
        public Task LoadImage (string url);
    }
}