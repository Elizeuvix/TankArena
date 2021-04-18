using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text txtRoomName, txtPlayeCount;
    public Button btnJoin;

    public void UpdateRoom(string roomName, int maxPlayer, int currentPlayer, UIController ui)
    {
        txtRoomName.text = roomName;
        txtPlayeCount.text = string.Format("{0}/{1}", currentPlayer, maxPlayer);

        if(currentPlayer == maxPlayer){
            btnJoin.interactable = false;
            return;
        }

        btnJoin.onClick.AddListener(delegate { ui.OnClick_JoinRoom(roomName); });
    }
}