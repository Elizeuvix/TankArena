using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float destroyTime = 1.0f;
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.Play();
        Destroy(this.gameObject, destroyTime);
    }
}
