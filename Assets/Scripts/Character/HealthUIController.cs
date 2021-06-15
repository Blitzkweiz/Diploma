using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    private GameObject player;
    public GameObject heartContainer;
    private float fillValue;

    void Start()
    {
        player = FindObjectsOfType<PlayerController>().First(x => x.photonView.IsMine).gameObject;
    }

    void Update()
    {
        fillValue = (float)player.GetComponent<PlayerController>().currentHealth / (float)player.GetComponent<PlayerController>().maxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
