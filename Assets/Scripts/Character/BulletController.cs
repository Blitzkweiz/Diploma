using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;
    public GameObject shooter;

    private string shooterTag;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DeathDelay());
        shooterTag = shooter.tag;
        damage = shooter.GetComponent<CharacterController>().damage;
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currentPosition + GetComponent<Rigidbody2D>().velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            //if (other.CompareTag("Wall"))
            //{
            //    Destroy(gameObject);
            //    continue;
            //}

            //if ((shooterTag == "Player") && other.CompareTag("Enemy"))
            //{
            //    var enemy = other.GetComponent<CharacterController>();
            //    enemy.TakeDamage(damage);
            //    Destroy(gameObject);
            //    continue;
            //}

            //if ((shooterTag == "Enemy") && other.CompareTag("Player"))
            //{
            //    var player = other.GetComponent<CharacterController>();
            //    player.TakeDamage(damage);
            //    Destroy(gameObject);
            //    continue;
            //}
        }
    }
}
