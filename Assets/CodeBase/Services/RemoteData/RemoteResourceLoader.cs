using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Services.RemoteData
{
    public class RemoteResourceLoader : IRemoteResourceLoader
    {
        public async Task LoadData (string url, Action<LoadingStatus, string> callback)
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(RemoteDataPath.RemoteDataURL);
            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                callback.Invoke(LoadingStatus.InLoading, null);
                await Task.Yield();
            }

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                callback.Invoke(LoadingStatus.Failed, null);
            }
            else
            { 
                try
                {
                    string response = webRequest.downloadHandler.text;
                    Debug.Log("Received: " + response);
                    callback.Invoke(LoadingStatus.Successfully, response);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed when trying to load a image: " + ex.Message);
                    callback.Invoke(LoadingStatus.Failed, null);
                }
            }
        }

        public async Task LoadImage(string url, Action<LoadingStatus, Sprite> callback)
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                callback.Invoke(LoadingStatus.InLoading, null);
                await Task.Yield();
            }

            if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                callback.Invoke(LoadingStatus.Failed, null);
            }
            else
            { 
                try
                {
                    Texture2D texture2D = DownloadHandlerTexture.GetContent(webRequest);
                    Sprite sprite = ConvertTextureToSprite(texture2D);
                    callback.Invoke(LoadingStatus.Successfully, sprite);
                }
                catch (Exception ex)
                {
                    Debug.LogError("Failed when trying to load a image: " + ex.Message);
                    callback.Invoke(LoadingStatus.Failed, null);
                }
            }
        }

        private Sprite ConvertTextureToSprite(Texture2D texture) => 
            Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}