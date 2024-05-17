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
        GameObject enemy = GameManager.i.Epool.Get(Random.Range(0,8)); // 0,8 random spawn
        //GameObject enemy = GameManager.i.Epool.Get(1);   // enemy spawn
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
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
