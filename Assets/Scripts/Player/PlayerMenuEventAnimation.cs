using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuEventAnimation : MonoBehaviour
{
    public void PlayRunSound()
    {
        AudioManager.GetInstance().Play(EAudio.SFXRunDirty, transform.position, true);
    }

    public void PlayWalkSound()
    {
        AudioManager.GetInstance().Play(EAudio.SFXWalkDirty, transform.position, true);
    }

    public void StopAllSounds()
    {
        AudioManager.GetInstance().Stop(EAudio.SFXRunDirty);
        AudioManager.GetInstance().Stop(EAudio.SFXWalkDirty);
    }
}
