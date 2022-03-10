using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{

    public static DropManager instance;
    PlayerController player;

    bool healthCooldown;
    public GameObject healthItem;

    bool shieldCooldown;
    public GameObject shieldItem;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryDropItem(int multiplier, Vector3 position)
    {
        if(player.health == 1 && !healthCooldown) //Health is top prio
        {
            float chance = Random.Range(0f, 1f);
            Debug.Log("Chance: " + chance);
            Debug.Log((float)multiplier / 10);
            if(chance < (float)multiplier / 10)
            {
                Instantiate(healthItem, position, Quaternion.identity);
                Debug.Log("Spawning health");
                StartCoroutine(HealthCd());
            }
        }
        
        else
        {
            if (!shieldCooldown)
            {
                float chance = Random.Range(0f, 1f);
                if (chance < ((float)multiplier / 10 - 0.3f))
                {
                    Instantiate(shieldItem, position, Quaternion.identity);
                    Debug.Log("Spawning shield");
                    StartCoroutine(ShieldCd());
                }
            }
        }
    }

    IEnumerator HealthCd()
    {
        healthCooldown = true;
        yield return new WaitForSeconds(60f);
        healthCooldown = false;
    }

    IEnumerator ShieldCd()
    {
        shieldCooldown = true;
        yield return new WaitForSeconds(60f);
        shieldCooldown = false;
    }
}
