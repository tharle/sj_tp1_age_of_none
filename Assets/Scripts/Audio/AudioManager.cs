using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public enum EAudio
{
    SFXConfirm,
    SFXText,
    SFXRunDirty,
    SFXWalkDirty
}
public class AudioManager
{
    private Dictionary<EAudio, AudioClip> m_AudioClips;
    private Dictionary<EAudio, AudioSource> m_AudioSourcePlaying;
    private AudioPool m_AudioPool;

    private BundleLoader m_Loader;

    private static AudioManager m_Instance;
    public static AudioManager GetInstance() {
        if (m_Instance == null) m_Instance = new AudioManager();

        return m_Instance; 
    }

    public AudioManager()
    {
        m_AudioPool = new AudioPool();
        m_AudioSourcePlaying = new Dictionary<EAudio, AudioSource>();
        m_Loader = BundleLoader.Instance;
        m_AudioClips = m_Loader.LoadSFX();
    }

    public void CleanAllDestroyed()
    {
        Dictionary<EAudio, AudioSource> newAudioSourcePlaying = new();
        foreach (KeyValuePair<EAudio, AudioSource> entry in m_AudioSourcePlaying)
        {
            if(!entry.Value.IsDestroyed()) newAudioSourcePlaying.Add(entry.Key, entry.Value);
        }

        m_AudioSourcePlaying = newAudioSourcePlaying;
    }

    public void Play(EAudio audioClipId, Vector3 soundPosition, bool isLooping = false)
    {
        CleanAllDestroyed();
        AudioSource audioSource;
        if (m_AudioSourcePlaying.ContainsKey(audioClipId))
        {
            audioSource = m_AudioSourcePlaying[audioClipId];
        }
        else
        {
            audioSource = m_AudioPool.GetAvailable();
            audioSource.clip = m_AudioClips[audioClipId];
            audioSource.transform.position = soundPosition;
            m_AudioSourcePlaying.Add(audioClipId, audioSource);
        }

        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
            audioSource.loop = isLooping;
        }
    }


    public void Stop(EAudio audioClipId)
    {
        CleanAllDestroyed();
        if (m_AudioSourcePlaying.ContainsKey(audioClipId)) m_AudioSourcePlaying[audioClipId].Stop();
    }
}
