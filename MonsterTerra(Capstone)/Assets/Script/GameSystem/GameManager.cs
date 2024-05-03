using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [Header("# Game Control")]
    public bool isLive = true; // ������ ���ۉ���� �ƴ��� �Ǵ��ϴ� ����
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
