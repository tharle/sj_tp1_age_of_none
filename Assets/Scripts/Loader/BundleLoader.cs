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
            GameObject go = new GameObject("BundleLoader");
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
        Dictionary<EAudio, AudioClip> audioClipsBundle = new();

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

    public Dictionary<ERank, Sprite> LoadAllRankStamps()
    {
        Dictionary<ERank, Sprite> rankStampBundle = new();

        string[] spriteNames = { nameof(ERank.S), nameof(ERank.A), nameof(ERank.B), nameof(ERank.C), nameof(ERank.NONE) };
        List<Sprite> sprites = LoadAll<Sprite>(GameParameters.BundleNames.SPRITE_STAMP, true, spriteNames);
        foreach (Sprite sprite in sprites)
        {
            ERank rankId = ERank.NONE;
            switch (sprite.name)
            {
                case nameof(ERank.S):
                    rankId = ERank.S;
                    break;
                case nameof(ERank.A):
                    rankId = ERank.A;
                    break;
                case nameof(ERank.B):
                    rankId = ERank.B;
                    break;
                case nameof(ERank.C):
                    rankId = ERank.C;
                    break;
                default:
                    rankId = ERank.NONE;
                    break;
            }

            Sprite newSprite = Instantiate(sprite);
            newSprite.name = sprite.name;
            rankStampBundle.Add(rankId, newSprite);
        }

        return rankStampBundle;
    }
}
