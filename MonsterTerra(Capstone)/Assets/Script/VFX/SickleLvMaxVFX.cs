using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleLvMaxVFX : MonoBehaviour
{
    float atk = 5;
    float cridmg = 0;
    float cri = 0;

    int etype = 0; // 특수 효과
    float dur = 0;
    float amount = 0;

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

    public void SetAttack(float _atk, float _cridmg, float _cri, float _dur = 0, float _amount = 0, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;

        dur = _dur;
        amount = _amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
           collision.CompareTag("ShootMonster1") || collision.CompareTag("ShootMonster2") || collision.CompareTag("ShootMonster3") || collision.CompareTag("ShootMonster4") ||
           collision.CompareTag("BossMonster1") || collision.CompareTag("BossMonster2"))
        {
            collision.GetComponent<Enemy>().TakeDamage(0,0,0,7,dur ,amount); // 데미지 없이 출혈 효과만 적용
        }
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }
    
}
