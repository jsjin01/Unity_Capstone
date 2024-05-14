using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleLvMaxVFX : MonoBehaviour
{
    float atk = 5;
    float cridmg = 0;
    float cri = 0;
    int etype = 0; // 특수 효과

    [SerializeField] GameObject parent;
    [SerializeField] Vector2 target;
    [SerializeField] float dTime = 1.0f;

    private void Start()
    {
        Destroy(parent, dTime);
    }

    private void Update()
    {
        target = GamePlayerMoveControl.i.playerPos;
        parent.transform.position = target;
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Enemy>().TakeDamage(0,0,0,7); // 데미지 없이 출혈 효과만 적용
        }
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }
    
}
