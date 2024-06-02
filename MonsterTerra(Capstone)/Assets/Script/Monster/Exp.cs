using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public float destroyDelay = 10f;
    public int amount = 10;
    // ExpPrefab이 생성될 때 호출되는 함수
    void Start()
    {
        Debug.Log("Exp 스크립트가 실행되었습니다.");
        // 생성 후 일정 시간 후에 삭제
        Destroy(gameObject, destroyDelay);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가진 플레이어인지 확인합니다.
        if (collision.CompareTag("Player"))
        {
            GamePlayerManager.i.GetExp(amount);
            Destroy(gameObject);
        }
    }
}
