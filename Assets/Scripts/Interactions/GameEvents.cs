using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public bool theBattleBegins = false;
    public static List<PlayerController> Players { get; set; }

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        onDoorwayTriggerEnter?.Invoke(id);
    }
}
