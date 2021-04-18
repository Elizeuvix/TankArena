using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [HideInInspector] public Transform targetPlayer;
    public Vector3 offset = Vector3.up;
    public float smoothCamera = 5.0f;
    private void Update()
    {
        if (!targetPlayer) return;

        transform.LookAt(targetPlayer.position, Vector3.up);
        transform.position = Vector3.Lerp(transform.position, 
        targetPlayer.position + offset, 
        smoothCamera * Time.deltaTime);
    }
}
