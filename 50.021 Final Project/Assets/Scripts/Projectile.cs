using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float speed;


    private Transform player;

    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().PlayerTakeDamage(20);
            DestroyProjectile();
            Debug.Log("Hit");
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Platform"))
        {
            DestroyProjectile();

            Debug.Log("Hit Non-Player");
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
