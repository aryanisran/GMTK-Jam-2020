using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public GameObject gameObject;
        public Transform transform;
        public bool isEnabled;
    }
    public GameObject[] enemyPrefabs;
    public SpawnPoint[] spawnPoints;
    float timeElapsed;
    public float spawnRate;
    public int maxEnemies;
    public int enemyCount;
    public int killCount;
    public static SpawnManager instance;
    SpawnPoint spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (SpawnPoint sp in spawnPoints)
        {
            sp.transform = sp.gameObject.transform;
            sp.isEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount >= maxEnemies) return;
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= spawnRate)
        {
            Spawn();
        }
    }

    public void RampSpawn()
    {
        enemyCount--;
        killCount++;
        spawnRate = Mathf.Clamp(spawnRate - 0.1f, 0.5f, 10);
        if(maxEnemies < 10)
        {
            maxEnemies++;
        }
        else if (killCount % 5 == 0)
        {
            maxEnemies++;
        }
    }

    public void Spawn()
    {
        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //Pick a random spawn point
        if (!spawnPoint.isEnabled)
        {
            for (int i = 0; spawnPoints[i].isEnabled == false; i = Random.Range(0, spawnPoints.Length)) //If picked spawn point isn't enabled, pick random spawn points unitl find one that is
            {
                spawnPoint = spawnPoints[i];
            }
        }

        if(killCount > 50)
        {
            foreach(GameObject enemy in enemyPrefabs)
            {
                Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            }
            enemyCount += enemyPrefabs.Length;
        }

        else if (killCount > 10)
        {
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.transform.position, Quaternion.identity);
            enemyCount++;
        }

        else
        {
            Instantiate(enemyPrefabs[0], spawnPoint.transform.position, Quaternion.identity);
            enemyCount++;
        }
        timeElapsed = 0;
    }
}
