using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.health = 2;
            player.sr.sprite = PlayerController.instance.full;
            Instantiate(effect, collision.transform);
            Destroy(gameObject);
        }
    }
}
