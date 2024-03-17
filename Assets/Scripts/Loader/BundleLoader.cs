using UnityEngine;
using System.IO;
using System;
using Unity.VisualScripting;
using System.Collections.Generic;

public class BundleLoader: MonoBehaviour
{

    private static BundleLoader m_Instance;
    public static BundleLoader GetInstance()
    {
        if (m_Instance == null) 
        {
            GameObject go = new GameObject();
            m_Instance = go.AddComponent<BundleLoader>();
        } 

        return m_Instance;
    }

    public T Load<T>(string bundleName, string assetName) where T : UnityEngine.Object
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));

        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return default(T);
        }

        T asset = Instantiate(localAssetBundle.LoadAsset<T>(assetName));
        asset.name = assetName;

        localAssetBundle.Unload(false);

        return asset;
    }

    private List<T> LoadAll<T>(string bundleName, bool IsCallUnload, params string[] assetNames) where T : UnityEngine.Object
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));
        List<T> assets = new List<T>();

        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return assets;
        }

        foreach(string assetName in assetNames)
        {
            T asset = localAssetBundle.LoadAsset<T>(assetName);
            assets.Add(asset);
        }

        if(IsCallUnload) localAssetBundle.Unload(false);

        return assets;
    }

    public Dictionary<EAudio, AudioClip> LoadSFX()
    {
        Dictionary<EAudio, AudioClip> audioClipsBundle = new Dictionary<EAudio, AudioClip>();

        string[] assetNames = { nameof(EAudio.SFXConfirm), nameof(EAudio.SFXRunDirty), nameof(EAudio.SFXWalkDirty), nameof(EAudio.SFXText) };
        List<AudioClip> audioClips = LoadAll<AudioClip>(GameParameters.BundleNames.SFX, false, assetNames);
        foreach (AudioClip clip in audioClips)
        {
            EAudio audioId = EAudio.SFXConfirm;
            switch (clip.name)
            {
                case nameof(EAudio.SFXConfirm):
                    audioId = EAudio.SFXConfirm;
                    break;
                case nameof(EAudio.SFXRunDirty):
                    audioId = EAudio.SFXRunDirty;
                    break;
                case nameof(EAudio.SFXWalkDirty):
                    audioId = EAudio.SFXWalkDirty;
                    break;
                case nameof(EAudio.SFXText):
                    audioId = EAudio.SFXText;
                    break;
            }

            AudioClip newClip = Instantiate(clip);
            newClip.name = clip.name;
            audioClipsBundle.Add(audioId, newClip);
        }

        return audioClipsBundle;
    }
}
