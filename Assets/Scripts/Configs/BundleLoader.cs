using UnityEngine;
using System.IO;
using System;

public class BundleLoader
{

    public static void LoadAll<T>(string bundleName, Action<T[]> LoadAssets) where T : UnityEngine.Object
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));

        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return; // return empty
        }

        T[] assets = localAssetBundle.LoadAllAssets<T>();
        LoadAssets?.Invoke(assets);
        localAssetBundle.Unload(false);

    }
}
