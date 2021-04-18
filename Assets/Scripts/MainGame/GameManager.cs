using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("CANVAS UI")]
    public GameObject canvas;
    public GameObject panelTeam;
    public GameObject menuGame;

    [Header("SPAWN POINTS")]
    public Transform[] spawnPointBlue;
    public Transform[] spawnPointRed;

    [Header("PLAYERS")]
    public GameObject playerBlue;
    public GameObject playerRed;

    private PhotonView photoView;
    private TeamController teamController;
    private ChatController chatController;
    private bool paused = false;

    private void Start()
    {
        canvas.SetActive(true);
        photoView = GetComponent<PhotonView>();
        teamController = GetComponent<TeamController>();
        chatController = GetComponent<ChatController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            ShowGameMenu();
        }
    }

    private void ShowGameMenu()
    {
        menuGame.SetActive(paused);
    }

    public void OnClick_BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClick_HideGameMenu()
    {
        paused = false;
        ShowGameMenu();
    }

    public void OnClick_CreateBlueTeam()
    {
        int index = UnityEngine.Random.Range(0, (spawnPointBlue.Length - 1));
        Transform t = spawnPointBlue[index];

        GameObject go = PhotonNetwork.Instantiate(playerBlue.name, t.position, t.rotation);
        go.GetComponent<PlayerController>().teamController = teamController;
        chatController.playerController = go.GetComponent<PlayerController>();
        panelTeam.SetActive(false);
        SetCustomProperties(1);
    }

    public void OnClick_CreateRedTeam()
    {
        int index = UnityEngine.Random.Range(0, (spawnPointRed.Length - 1));
        Transform t = spawnPointRed[index];

        GameObject go = PhotonNetwork.Instantiate(playerRed.name, t.position, t.rotation);
        go.GetComponent<PlayerController>().teamController = teamController;
        chatController.playerController = go.GetComponent<PlayerController>();
        panelTeam.SetActive(false);
        SetCustomProperties(2);
    }

    private void SetCustomProperties(int t)
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        hash.Add("TEAM", t);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }
}
