using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerController : CharacterController
{
    public Animator topAnimator;
    public Animator bottomAnimator;

    private Rigidbody2D playerRigidbody;

    private PhotonView photonView;

    //public GameObject deathScreen;
    //public GameObject winScreen;

    //public Text killCounterText;
    //public int killCount = 0;

    public GameObject crossHair;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    private Vector3 movement;
    private Vector3 aim;
    private bool isAiming;
    private bool endOfAiming;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerRigidbody = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!photonView.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(playerRigidbody);
        }

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;

        ProcessInputs();
        Animate();
        AimAndShoot();
        Move();
    }

    [PunRPC]
    private void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<BulletController>().shooter = gameObject;
        bullet.GetComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x, y).normalized * bulletSpeed;
        bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(y, x) * Mathf.Rad2Deg);
        lastFire = Time.time;
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector3(movement.x * speed, movement.y * speed, 0.0f);
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);
        aim += mouseMovement;
        if (aim.magnitude > 1.0f)
        {
            aim.Normalize();
        }
        isAiming = Input.GetButton("Fire");
        endOfAiming = Input.GetButtonUp("Fire");

        if(movement.magnitude > 1.0f)
        {
            movement.Normalize();
        }

        //killCounterText.text = killCount + " x";
    }

    private void Animate()
    {
        bottomAnimator.SetFloat("Horizontal", movement.x);
        bottomAnimator.SetFloat("Vertical", movement.y);

        topAnimator.SetFloat("MoveHorizontal", movement.x);
        topAnimator.SetFloat("MoveVertical", movement.y);
        topAnimator.SetFloat("MoveMagnitude", movement.magnitude);

        topAnimator.SetFloat("AimHorizontal", aim.x);
        topAnimator.SetFloat("AimVertical", aim.y);
        topAnimator.SetFloat("AimMagnitude", aim.magnitude);
        topAnimator.SetBool("Aim", isAiming);
    }

    private void AimAndShoot()
    {
        if(aim.magnitude > 0.0f)
        {
            crossHair.transform.localPosition = aim * 0.4f;
            crossHair.SetActive(true);

            if (endOfAiming && Time.time > lastFire + fireDelay)
            {
                photonView.RPC("Shoot", RpcTarget.AllViaServer, aim.x, aim.y);
            }
        }
        else
        {
            crossHair.SetActive(false);
        }
    }

    protected override void Death()
    {
        //deathScreen.SetActive(true); 
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartCoroutine(EndScreen(2.0f));
    }

    private IEnumerator EndScreen(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene("Menu");
    }

    public void WinGame()
    {
        //winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartCoroutine(EndScreen(10.0f));
    }
}
