using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWallController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
    }

    private void OnDoorwayOpen(int id)
    {
        if(id == this.id)
        {
            gameObject.SetActive(false);
        }
    }
}
