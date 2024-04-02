using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive = true;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]   
    public float maxHp = 100;
    public float hp;
    public float previousHp;
    public int atk = 20;
    public float atkSpeed = 1f;
    public int lv;
    public int exp;
    public int[] nextExp = { 100, 140, 196 };
    [Header("# Game Object")]
    public EnemyPoolManager Epool;
    public Player player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hp = maxHp;
    }

    void FixedUpdate()
    {
        previousHp = hp;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

    }

    public void GetExp()
    {
        IncreaseExperience(10);

        if (exp >= nextExp[lv])
        {
            lv++;
            exp = 0;
        }
    }

    public void IncreaseExperience(int expAmount)
    {
        exp += expAmount;
        Debug.Log("경험치 증가! 현재 경험치: " + exp);
    }

}
