using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }
    }
}