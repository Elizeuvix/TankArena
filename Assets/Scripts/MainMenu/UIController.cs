using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class UIController : MonoBehaviour
{
    public Text txtMessage;

    [Header ("Nickname")]
    public GameObject panelNickname;
    public InputField txtNickname;
    public int minLengthNick = 3;
    public Dropdown listRegion; //
    
    [Header ("Lobby")]
    public GameObject panelLobby;
    public InputField txtRoomName;
    public Text txtServerData;
    public GameObject prefabRoomItem;
    public Transform parentRoomItem;

    private UILog uILog;
    private GameController gameController;

    void Start()
    {
        txtMessage.text = string.Empty;
        uILog = GetComponent<UILog>();
        gameController = GetComponent<GameController>();
        panelNickname.SetActive(true);
        GetNickname();
    }
    
    #region "UI Events"
    //Este método é executado quando clicamos no botão Nickname para entrar no Lobby
    public void OnClick_NickName()
    {
        if(txtNickname.text.Length < minLengthNick)
        {
            uILog.SetText("Nickname inválido, min " + minLengthNick);
            return;
        }
        SetNickname();
        panelNickname.SetActive(false);
        txtMessage.text = "LOADING...";
        uILog.SetText(string.Empty);

        gameController.StartConnection(txtNickname.text, listRegion.value);
    }

    public void ChangeRegionList(string[] regions)
    {
        for(int i = 0; i < regions.Length; i++)
        {
            listRegion.options.Add(new Dropdown.OptionData() {text = regions[i]});
        }
        listRegion.value = regions.Length-1;
    }

    #endregion

    private void GetNickname()
    {
        if(PlayerPrefs.HasKey("NICK"))
        {
            txtNickname.text = PlayerPrefs.GetString("NICK");
        }
    }

    private void SetNickname()
    {
        PlayerPrefs.SetString("NICK", txtNickname.text);
    }

    public void ShowLobbyPanel(bool show = true)
    {
        if(show)
        {
            panelLobby.SetActive(true);
            txtMessage.text = string.Empty;
        }
        else
        {
            panelLobby.SetActive(false);
        }
        
    }

    public void ShowLog(string s)
    {
        uILog.SetText(s);
    }

    public void ShowMessage(string s)
    {
        txtMessage.text = s;
    }

    public void ShowServerData(string d) 
    {
        txtServerData.text = d;
    }

    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        //prefabRoomItem
        foreach (RoomInfo ri in roomList)
        {
            RoomItem roomItem = Instantiate(prefabRoomItem, parentRoomItem.position, parentRoomItem.rotation, parentRoomItem).GetComponent<RoomItem>();
            roomItem.UpdateRoom(ri.Name, ri.MaxPlayers, ri.PlayerCount, this);
        }
    }

    public void OnClick_CreateRoom()
    {
        if (txtRoomName.text.Length < 3)
            return;

        gameController.CreateRoom(txtRoomName.text, true);
    }

    public void OnClick_JoinRoom(string roomName)
    {
        if (roomName.Length < 3)
            return;

        gameController.CreateRoom(roomName, false);
    }
}
