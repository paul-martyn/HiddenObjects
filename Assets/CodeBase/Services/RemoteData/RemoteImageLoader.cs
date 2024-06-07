using System;
using System.Threading.Tasks;
using CodeBase.UI.UiElements;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Services.RemoteData
{
    public class RemoteImageLoader : IRemoteImageLoader
    {
        public event Action<LoadingState, Sprite> LoadStateChanged;
        
        public async Task LoadImage(string url)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                LoadStateChanged?.Invoke(LoadingState.InLoading, null);
                await Task.Yield();
            }

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                LoadStateChanged?.Invoke(LoadingState.Failed, null);
            }
            else
            { 
                try
                {
                    Texture2D texture2D = DownloadHandlerTexture.GetContent(webRequest);
                    Sprite sprite = ConvertTextureToSprite(texture2D);
                    LoadStateChanged?.Invoke(LoadingState.Loaded, sprite);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed when trying to load a texture: " + ex.Message);
                    LoadStateChanged?.Invoke(LoadingState.Failed, null);
                }
            }
        }
        
        private Sprite ConvertTextureToSprite(Texture2D texture) => 
            Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}