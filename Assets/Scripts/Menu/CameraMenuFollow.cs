using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuFollow : MonoBehaviour
{
    private Vector3 m_offset;

    private void Start()
    {
        Vector3 playerPosition = PlayerMenuController.GetInstance().transform.position;
        m_offset = transform.localPosition - playerPosition;
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector3 playerPosition = PlayerMenuController.GetInstance().transform.position;
        transform.position = playerPosition + m_offset;
    }
}
