using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Effects")]
    public AudioSource audioSource;
    public AudioClip[] audiosClips;
    public ParticleSystem particleShot;
    public Animator animator;

    [Header("Controllers")]
    public GameObject prefabBullet;
    public Transform shotPoint;
    public float shotTime = 0.4f ;
    public float reloadTime = 0.2f ;

    private PlayerController playerController;
    private PhotonView photonView;
    private bool isReloading = false;
    private float currentTime =0f;    

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(!photonView.IsMine || isReloading || !playerController.control) return;

        if(Input.GetMouseButton(0) && currentTime >= shotTime)
        {
            isReloading = true;
            currentTime = 0f;
            PhotonNetwork.Instantiate(prefabBullet.name, shotPoint.position, shotPoint.rotation);
            
            StartCoroutine(Reloading());
            photonView.RPC("PlayEffect", RpcTarget.All);
        }
        
        currentTime += 1 * Time.deltaTime;
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
    }

    [PunRPC]
    private void PlayEffect() 
    {
        audioSource.clip = audiosClips[UnityEngine.Random.Range(0, audiosClips.Length - 1)];
        audioSource.Play();
        particleShot.Play();
        animator.SetTrigger("Fire");
    }
}
