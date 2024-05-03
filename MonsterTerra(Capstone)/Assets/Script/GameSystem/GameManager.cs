using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [Header("# Game Control")]
    public bool isLive = true; // 게임이 시작됬는지 아닌지 판단하는 변수
    public float gameTime;

    [Header("# Game Object")]
    public EnemyPoolManager Epool;

    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }
}
