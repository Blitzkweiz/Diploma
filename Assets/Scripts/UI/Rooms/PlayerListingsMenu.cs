﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private PlayerListing playerListing;
    [SerializeField]
    private Button readyUpButton;
    [SerializeField]
    private Button startGameButton;

    private TextMeshProUGUI readyUpText;

    private List<PlayerListing> listings = new List<PlayerListing>();
    private RoomsCanvases roomsCanvases;
    private bool ready = false;


    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
        if (PhotonNetwork.IsMasterClient)
        {
            readyUpButton.gameObject.SetActive(false);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
            readyUpText = readyUpButton.GetComponentInChildren<TextMeshProUGUI>();
            SetReadyUp(false);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for(int i = 0; i < listings.Count; i++)
        {
            Destroy(listings[i].gameObject);
        }

        listings.Clear();
    }

    private void SetReadyUp(bool state)
    {
        if(readyUpText != null)
        {
            ready = state;

            if (ready)
                readyUpText.text = "R";
            else
                readyUpText.text = "N";
        }
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                listings.Add(listing);
            }
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        roomsCanvases.CurrentRoomCanvas.LeaveRoomMenu.OnClick_LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
    
    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < listings.Count; i++)
            {
                Debug.Log($"player {i} readyup set to {listings[i].Ready}");
                if (listings[i].Player != PhotonNetwork.LocalPlayer && !listings[i].Ready)
                    return;
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, ready);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            listings[index].Ready = ready;
        }
    }
}
