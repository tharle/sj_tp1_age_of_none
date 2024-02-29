using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuEventAnimation : MonoBehaviour
{
    public void PlayRunSound()
    {
        AudioManager.GetInstance().Play(EAudio.SFX_RUN_DIRTY, transform.position, true);
    }

    public void PlayWalkSound()
    {
        AudioManager.GetInstance().Play(EAudio.SFX_WALK_DIRTY, transform.position, true);
    }

    public void StopAllSounds()
    {
        AudioManager.GetInstance().Stop(EAudio.SFX_RUN_DIRTY);
        AudioManager.GetInstance().Stop(EAudio.SFX_WALK_DIRTY);
    }
}
