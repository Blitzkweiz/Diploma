using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        StartCoroutine(StartLoadMenu());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartLoadMenu()
    {
        PhotonNetwork.LeaveRoom(true);
        while (PhotonNetwork.InRoom)
            yield return null;
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
