using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level;
    float timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // point array( me = 0, components = 1~N )
    }
    void Update()
    {
        timer += Time.deltaTime;    //timer
        level = Mathf.Min(Mathf.FloorToInt(GameManager.i.gameTime / 10f), spawnData.Length - 1); // 0.xx delete ( 10s, 20s )

        if(timer > spawnData[level].spawnTime) // Setting Value
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.i.Epool.Get(Random.Range(0,10)); // 0,10 random spawn
        //GameObject enemy = GameManager.i.Epool.Get(1);   // enemy spawn
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

    public void BossSpawn_1()
    {
        StartCoroutine(SpawnBossEnemies(0,4));
    }

    public void BossSpawn_2()
    {
        StartCoroutine(SpawnBossEnemies(3,7));
    }

    IEnumerator SpawnBossEnemies(int e1, int e2)
    {
        // Loop through and spawn RushMonster1 and ShootMonster1 enemies
        for (int i = 0; i < 5; i++)
        {
            SpawnMonster(e1);
            SpawnMonster(e2);
            yield return new WaitForSeconds(1f); // Delay between spawns
        }
    }

    void SpawnMonster(int enemyNo)
    {
        GameObject enemy = GameManager.i.Epool.Get(enemyNo); // Assuming the index for RushMonster1 is 0
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // Initialize enemy
    }
}

[System.Serializable]   //Serailization
public class SpawnData
{
    public int spriteType; // Type
    public float spawnTime; // SpawnTime
    public int hp; // health
    public float speed; // speed
    public float damage;
    public float defense;
}
