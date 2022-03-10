using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventOutOfBoundsSpawn : MonoBehaviour
{
    SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = SpawnManager.instance;
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(SpawnManager.SpawnPoint sp in spawnManager.spawnPoints)
        {
            if(collision.gameObject == sp.gameObject)
            {
                sp.isEnabled = true;
            }
        }
    }*/


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            //Debug.Log(collision.name);
            collision.GetComponent<Enemy>().inBounds = true;
        }
    }
}
