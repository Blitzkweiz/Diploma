using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private ExitGames.Client.Photon.Hashtable myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private void SetCustomNumber()
    {
        System.Random random = new System.Random();
        int result = random.Next(0, 99);

        text.text = result.ToString();

        myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(myCustomProperties);
    }

    public void OnClick_Button()
    {
        SetCustomNumber();
    }
}
