using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ChatController : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject panelChat; //Acessa o painel do chat
    public InputField inputMessage; //Acessa o formulário de menssagen
    public Text txtMessage; //Acessa o componente que vai exibir as mensagens
    [Tooltip("Caracteres mínimos para enviar a mensagem")]
    public int minLength = 2;//Caracteres mínimos para enviar a mensagem
    [HideInInspector] public PlayerController playerController;//Acessa o player controller
    public AudioSource audioSource; //Alarme chat

    private bool isOpen = false;
    private PhotonView photonView;

    private void Start()
    {
        panelChat.SetActive(false);
        photonView = GetComponent<PhotonView>();
        ResetChat();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            ChangeVisible();
            
            if (isOpen)
                InputFocus();
        }

        ///KeyCode.Return = ENTER DO MEIO
        if (isOpen && inputMessage.text.Length >= minLength &&
        Input.GetKeyDown(KeyCode.Return))
        {
            photonView.RPC("SendChatMessage", RpcTarget.All, GetMessage());
            inputMessage.text = string.Empty;
            InputFocus();
        }
    }

    private string GetMessage()
    {
        string c = "#ffa500ff";
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        return string.Format("<b><color={0}>{1}</color></b> - {2}", c, nickName, inputMessage.text);
    }

    [PunRPC]
    private void SendChatMessage(string msg)
    {
        txtMessage.text += msg + "\n";
        audioSource.Play();
    }

    private void InputFocus()
    {
        EventSystem.current.SetSelectedGameObject(inputMessage.gameObject, null);
        inputMessage.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void ChangeVisible()
    {
        panelChat.SetActive(isOpen);
        playerController.control = !isOpen;
    }

    private void ResetChat()
    {
        txtMessage.text = string.Empty;
        inputMessage.text = string.Empty;
    }
}