using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    float normMoveSpeed;
    protected Transform target;
    public Transform lookTarget;
    Rigidbody2D rb;
    public int health;
    SpawnManager spawnManager;
    public bool hit;
    float hitTimer;
    public bool inBounds;
    float boundsTimer;
    public Transform particleSpawn;
    public GameObject deathParticle;
    public bool spawning;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lookTarget = target = PlayerController.instance.gameObject.transform;
        normMoveSpeed = moveSpeed;
        spawnManager = SpawnManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inBounds)
        {
            boundsTimer += Time.deltaTime;
            if(boundsTimer >= 0.1f)
            {
                Destroy(gameObject);
                spawnManager.enemyCount--;
                spawnManager.Spawn();
            }
        }

        if (spawning) return;

        if (hit)
        {
            moveSpeed = 0;
            hitTimer += Time.deltaTime;
            if(hitTimer >= 0.5f)
            {
                hit = false;
            }
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance >= 8)
        {
            moveSpeed = normMoveSpeed + 3;
        }

        else
        {
            moveSpeed = normMoveSpeed;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        //transform.LookAt(target);
        Vector3 direction = lookTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg - 90);
    }

    public void Die()
    {
        spawnManager.RampSpawn();
        Instantiate(deathParticle, particleSpawn.position, transform.rotation);
        AudioManager.instance.Play("Grass Cut");
        Destroy(gameObject);
    }
}
