using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemspawner : MonoBehaviour
{
    bool isStart = false;
    [SerializeField]GameObject[] Item; //스폰할 수 있는 아이템 목록
    
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.i.isLive && !isStart) //처음 스폰하는 부분
        {
            InvokeRepeating("CreatItem", 0f, 10f*(2 -GamePlayerManager.i.Cbuff));
            isStart = true;
        }
    }
    
    void CreatItem()
    {
        Quaternion rotation = Quaternion.identity;
        float x = Random.Range(-9, 9);
        float y = Random.Range(-5, 5);
        int idx = Random.Range(0, 5);
        Vector2 randomPos = GamePlayerMoveControl.i.playerPos + new Vector2(x, y);

        GameObject thunderMax = Instantiate(Item[idx], randomPos, rotation, transform);
    }
}
