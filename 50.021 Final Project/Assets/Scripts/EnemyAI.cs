using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public GameObject projectile;
    public GameObject player; 
    private float speed = 600f;
    private float nextWayPointDistance = 5f;

    private Path path;
    private int currentWayPoint = 0;
    private bool reachedEndofPath = false;

    public Transform enemySprite;

    private float timeBtwShots;

    public float startTimeBtwShots;

    Seeker seeker;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        timeBtwShots = startTimeBtwShots;
        InvokeRepeating("UpdatePath", 0f, 0.5f);

       
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, player.transform.position, OnPathComplete);
    }

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndofPath = true;
            return;
        }
        else
        {
            reachedEndofPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);
        // Reached the waypoint
        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        turnSprite(force, enemySprite);

    }

    void turnSprite(Vector2 force, Transform enemyGFX)
    {
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(5f, 5f, 5f);
        }

        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-5f, 5f, 5f);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

}
