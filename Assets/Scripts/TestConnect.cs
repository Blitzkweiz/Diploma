using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class TestConnect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text connectionState;
    [SerializeField]
    private Button playButton;
    void Start()
    {
        Debug.Log("Connecting to Photon...", this);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();

        playButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon.", this);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName, this);
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
        connectionState.color = new Color(0, 255, 0);
        connectionState.text = "Connected";

        playButton.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason " + cause.ToString(), this);
        if (connectionState != null)
        {
            connectionState.color = new Color(255, 0, 0);
            connectionState.text = "Connecting...";
        }

        playButton.interactable = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.", this);
    }
}
