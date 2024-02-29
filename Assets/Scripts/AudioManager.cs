using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EAudio
{
    SFX_CONFIRM,
    SFX_TEXT,
    SFX_RUN_DIRTY,
    SFX_WALK_DIRTY
}
public class AudioManager
{
    private Dictionary<EAudio, AudioClip> m_AudioClips;
    private Dictionary<EAudio, AudioSource> m_AudioSourcePlaying;

    private static AudioManager m_Instance;

    private AudioPool m_AudioPool;

    public static AudioManager GetInstance() {
        if (m_Instance == null) m_Instance = new AudioManager();

        return m_Instance; 
    }

    public AudioManager()
    {
        m_AudioPool = new AudioPool();
        m_AudioSourcePlaying = new Dictionary<EAudio, AudioSource>();
        LoadAllAudioClips();
    }

    private void LoadAllAudioClips()
    {
        m_AudioClips = new Dictionary<EAudio, AudioClip>();
        m_AudioClips.Add(EAudio.SFX_CONFIRM, Resources.Load<AudioClip>("Sounds/SFXConfirm"));
        m_AudioClips.Add(EAudio.SFX_RUN_DIRTY, Resources.Load<AudioClip>("Sounds/SFXRunDirty"));
        m_AudioClips.Add(EAudio.SFX_WALK_DIRTY, Resources.Load<AudioClip>("Sounds/SFXWalkDirty"));
        m_AudioClips.Add(EAudio.SFX_TEXT, Resources.Load<AudioClip>("Sounds/SFXText"));
    }

    public void Play(EAudio audioClipId, Vector3 soundPosition, bool isLooping = false)
    {
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
        if (m_AudioSourcePlaying.ContainsKey(audioClipId)) m_AudioSourcePlaying[audioClipId].Stop();
    }
}
