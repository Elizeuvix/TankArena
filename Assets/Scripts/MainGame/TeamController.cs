using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TeamController : MonoBehaviour
{
    public Text txtBlueTeam, txtRedTeam;

    private int bluePoints = 0, redPoints = 0;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        ChangeValues();
    }

    private void ChangeValues()
    {
        txtBlueTeam.text = bluePoints.ToString();
        txtRedTeam.text = redPoints.ToString();
    }

    public void AddPoint(bool blueTeam)
    {
        if (blueTeam)
            this.bluePoints++;
        else
            this.redPoints++;

        photonView.RPC("Change", RpcTarget.AllBuffered, this.bluePoints, this.redPoints);
    }

    [PunRPC]
    private void Change(int bp, int rp)
    {
        this.bluePoints = bp;
        this.redPoints = rp;
        ChangeValues();
    }
}