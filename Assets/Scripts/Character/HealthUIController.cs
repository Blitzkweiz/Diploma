using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject player;
    public GameObject heartContainer;
    private float fillValue;

    void Update()
    {
        fillValue = (float)player.GetComponent<PlayerController>().currentHealth / (float)player.GetComponent<PlayerController>().maxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
