using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class GameController : MonoBehaviourPunCallbacks
{
    [Header("Game Data")]
    public byte maxPlayersRoom = 4;
    public string appVersion = "0.1";
    public string[] regions;

    private UIController uIController;

    void Start()
    {
        uIController = GetComponent<UIController>();
        uIController.ChangeRegionList(regions);
    }

    public void StartConnection(string nickname, int re = 0)
    {
        PhotonNetwork.GameVersion = appVersion;

        if (regions[re] != "NOTHING"){
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regions[re];
        }

        PhotonNetwork.ConnectUsingSettings();
        
        PhotonNetwork.NickName = nickname;
        uIController.ShowLog("Conectando...");
    }

    #region "UI Events"

    public void CreateRoom(string roomName, bool create = true){
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = maxPlayersRoom;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);

        uIController.ShowLobbyPanel(false);
        if(create){
            uIController.ShowMessage("Creating...");
        }else{
            uIController.ShowMessage("Joining...");
        }
    }

    public void OnClick_ExitGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    #endregion

    #region "PUN Callbacks"

    public override void OnConnectedToMaster()
    {
        uIController.ShowServerData(string.Format("Connected: <b>{0}</b> | App Version: <b>{1}</b> | Max Players: <b>{2}</b>", PhotonNetwork.CloudRegion, appVersion, maxPlayersRoom));
        uIController.ShowLog("Conectado");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        uIController.ShowLobbyPanel();
        uIController.ShowLog("Joined in Lobby");
    }    

    public override void OnJoinedRoom()
    {
        uIController.ShowMessage("Entrando...");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnRoomListUpdate(List<RoomInfo> rl)
    {
        uIController.UpdateRoomList(rl);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        uIController.ShowLobbyPanel(false);
        uIController.ShowLog(cause.ToString());
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        uIController.ShowLobbyPanel(false);
        uIController.ShowLog(message);
        uIController.ShowLog(returnCode.ToString());
    }

    #endregion
}
