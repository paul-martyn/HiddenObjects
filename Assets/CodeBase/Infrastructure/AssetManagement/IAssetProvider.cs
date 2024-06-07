using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Transform parent);
    }
}