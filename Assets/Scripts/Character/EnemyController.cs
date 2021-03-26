using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Stay,
    Follow,
    Attack,
    Death
};

public enum EnemyType
{
    Ranged,
    Melee
}

public class EnemyController : CharacterController
{

    GameObject player;
    public EnemyState currentState = EnemyState.Stay;
    public Animator animator;
    private Vector3 playerDirection;

    public EnemyType enemyType;
    public float range;

    public float attackRange;
    public float coolDown;
    public bool coolDownAttack = false;

    public float deathTime;

    public GameObject bulletPrefab;

    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    void Update()
    {
        if (currentState != EnemyState.Death)
        {
            if (currentState == EnemyState.Follow)
            {
                Follow();
            }

            if (IsPlayerInRange())
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    currentState = EnemyState.Attack;
                    Attack();
                }
                else
                {
                    currentState = EnemyState.Follow;
                }
            }
        }

        if (!GameEvents.current.theBattleBegins)
        {
            currentHealth = -1;
            Death();
        }

        playerDirection = player.transform.position - transform.position;

        animator.SetFloat("AttackX", playerDirection.x);
        animator.SetInteger("Health", currentHealth);
        animator.SetBool("CoolDown", coolDownAttack);
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private void Follow()
    {
        if (!coolDownAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 
            animator.SetFloat("MoveX", playerDirection.x);
            animator.SetFloat("MoveMagnitude", playerDirection.magnitude);
        }
    }

    private void Attack()
    {
        if (!coolDownAttack)
        {
            animator.StopPlayback();
            animator.Play("Attack", 0, 0.0f);
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    player.GetComponent<PlayerController>().TakeDamage(damage);
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().shooter = gameObject;
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<Rigidbody2D>().velocity = playerDirection.normalized;
                    bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg);
                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    protected override void Death()
    {
        //player.GetComponent<PlayerController>().killCount++;
        StartCoroutine(DeathDelay());
        currentState = EnemyState.Death;
    }
    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}
