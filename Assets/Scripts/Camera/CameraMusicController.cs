using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMusicController : MonoBehaviour
{

    void Update()
    {
        if(!PlayerController.Instance.IsPlaying) GetComponent<AudioSource>().Stop();
    }
}
