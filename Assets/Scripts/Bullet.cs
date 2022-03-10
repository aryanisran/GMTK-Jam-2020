using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int enemiesKilled;

    private void Start()
    {
        enemiesKilled = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.health--;
            if (enemy.health <= 0) enemy.Die();
            enemiesKilled++;
            ScoreCounter.instance.score += 100 * enemiesKilled;
            UIScript.instance.UpdateScore();
            UIScript.instance.UpdateScoreGain(100 * enemiesKilled, enemiesKilled);
            DropManager.instance.TryDropItem(enemiesKilled, transform.position);
        }
    }
}
