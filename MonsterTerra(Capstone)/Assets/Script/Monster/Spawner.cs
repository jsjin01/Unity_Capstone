using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public int maxEnemies = 10; // 최대 소환할 몹의 수
    private int currentEnemyCount = 0; // 현재 소환된 몹의 수
    private int currentBossCount = 0;
    public int enemyKillCount = 0;
    public int BossKillCount = 0;
    public int level = 0;
    int prelevel = 0;
    float timer;

    enum StageType
    {
        eStage_1,
        eStage_2,
        eStage_3,
        eStage_4,
        eBossStage_1,
        eStage_6, 
        eStage_7,
        eStage_8,
        eStage_9,
        eBossStage_2,
    }

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // point array( me = 0, components = 1~N )
    }
    
    void Update()
    {
        timer += Time.deltaTime;    //timer
        levelCheck();
        if (timer > spawnData[level].spawnTime) // Setting Value
        {
            timer = 0;
            Spawn(level);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            IncreaseLevel();
        }
    }

    void IncreaseLevel()
    {
        if (level < spawnData.Length - 1)
        {
            level++;
            Debug.Log("Level increased to: " + level);
        }
        else
        {
            Debug.Log("Maximum level reached.");
        }
    }

    void levelCheck()
    {
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.i.gameTime / 10f), spawnData.Length - 1); // 0.xx delete ( 10s, 20s )
        switch (level)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                if (enemyKillCount == 10)
                    level++;
                break;
            case 4:
                if (BossKillCount == 1)
                {
                    level++;
                    Enemy[] allEnemies = FindObjectsOfType<Enemy>();
                    foreach (Enemy enemy in allEnemies)
                    {
                        if (enemy.isLive)
                        {
                            StartCoroutine(enemy.Die(0f)); // 사망 애니메이션 없이 즉시 사망
                        }
                    }
                }
                break;
            case 5:
            case 6:
            case 7:
            case 8:
                if (enemyKillCount == 10)
                    level++;
                break;
            case 9:
                if (BossKillCount == 1)
                {
                    level++;
                    Enemy[] allEnemies = FindObjectsOfType<Enemy>();
                    foreach (Enemy enemy in allEnemies)
                    {
                        if (enemy.isLive)
                        {
                            StartCoroutine(enemy.Die(0f)); // 사망 애니메이션 없이 즉시 사망
                        }
                    }
                }
                break;
            default:
                break;
        }

        if (level != prelevel)
        {
            currentEnemyCount = 0;
            currentBossCount = 0;
            enemyKillCount = 0;
            BossKillCount = 0;
        }
        prelevel = level;
    }

    void Spawn(int level)
    {
        StageType eStage = (StageType)level;

        if (currentEnemyCount < maxEnemies) // 현재 소환된 몹의 수가 최대치보다 적을 때만 소환
        {
            switch (eStage)
            {
                case StageType.eStage_1:
                case StageType.eStage_2:
                    SpawnMonster(0);
                    SpawnMonster(4);
                    break;
                case StageType.eStage_3:
                case StageType.eStage_4:
                    SpawnMonster(1);
                    SpawnMonster(5);
                    break;
                case StageType.eBossStage_1:
                    if (currentBossCount == 1) return;
                    currentBossCount++;
                    SpawnMonster(8);
                    break;
                case StageType.eStage_6:
                case StageType.eStage_7:
                    SpawnMonster(2);
                    SpawnMonster(6);
                    break;
                case StageType.eStage_8:
                case StageType.eStage_9:
                    SpawnMonster(3);
                    SpawnMonster(7);
                    break;
                case StageType.eBossStage_2:
                    if (currentBossCount == 1) return;
                    currentBossCount++;
                    SpawnMonster(9);
                    break;
            }
        }
    }

    void SpawnMonster(int enemyNo)
    {
        //GameObject enemy = GameManager.i.Epool.Get(Random.Range(0,1)); // 0,10 random spawn
        GameObject enemy = GameManager.i.Epool.Get(enemyNo);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[enemyNo]);
        currentEnemyCount++; // 소환된 몹의 수 증가
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
            if (currentBossCount == 0) yield break;
            SpawnMonster(e1);
            SpawnMonster(e2);
            yield return new WaitForSeconds(1f); // Delay between spawns
        }
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
