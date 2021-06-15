using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private Transform[] positions;

    private void Awake()
    {
        int i = 0;

        if (PhotonNetwork.IsMasterClient)
        {
            i = 1;
        }

        var player = MasterManager.NetworkInstantiate(prefab, positions[i].position, Quaternion.identity);
        GameEvents.Players.Add(player.GetComponent<PlayerController>());
    }
}
