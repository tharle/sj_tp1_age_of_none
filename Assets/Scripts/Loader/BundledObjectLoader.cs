using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BundledObjectLoader : MonoBehaviour
{

    [SerializeField] private string m_AssetName  = "BundledSpriteObject";
    [SerializeField] private string m_BundleName = "testbundle";

    void Start()
    {
        LoadBundle();
    }

    private void LoadBundle()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, m_BundleName));

        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return;
        }

        GameObject asset = localAssetBundle.LoadAsset<GameObject>(m_AssetName);
        Instantiate(asset);
        localAssetBundle.Unload(false);
    }
}
