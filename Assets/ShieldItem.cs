using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.health = 3;
            player.shield.SetActive(true);
            player.sr.sprite = player.full;
            Destroy(gameObject);
        }
    }
}
