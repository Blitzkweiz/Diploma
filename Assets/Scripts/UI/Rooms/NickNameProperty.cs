using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameProperty : MonoBehaviour
{
    [SerializeField]
    private InputField input;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    private void SetCustomNickName()
    {
        customProperties["NickName"] = input.text;
        PhotonNetwork.SetPlayerCustomProperties(customProperties);
    }

    public void OnClick_Button()
    {
        SetCustomNickName();
    }
}
