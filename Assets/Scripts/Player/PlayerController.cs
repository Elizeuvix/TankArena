using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PhotonView))]

public class PlayerController : MonoBehaviour
{
    public bool control = true;
    public Text txtNickname;
    [HideInInspector] public TeamController teamController;
    [HideInInspector] public int team;

    private PlayerHealth playerHealth;
    private PlayerWeapon playerWeapon;
    private PlayerMotor playerMotor;  
    private PhotonView photonView;    
    
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerMotor = GetComponent<PlayerMotor>();
        photonView = GetComponent<PhotonView>();

        //if(!PhotonNetwork.IsConnected) return;

        if(photonView.IsMine)
        {
            GameObject.FindGameObjectWithTag("MainCamera").
            GetComponent<PlayerCamera>().targetPlayer = this.transform;
            txtNickname.text = PhotonNetwork.LocalPlayer.NickName;
            team = (int)PhotonNetwork.LocalPlayer.CustomProperties["TEAM"];
        }
        else
        {
            txtNickname.text = photonView.Owner.NickName;
            team = (int)photonView.Owner.CustomProperties["TEAM"];
        }
    }

    void Update()
    {
        if(!control || !photonView.IsMine)
            return;
        playerMotor.Move();
    }
}
