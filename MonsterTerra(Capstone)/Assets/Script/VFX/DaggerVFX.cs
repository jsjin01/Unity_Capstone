using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerVFX : MonoBehaviour
{
    float atk = 5;
    float cridmg = 0;
    float cri = 0;
    int etype = 0; // 특수 효과
    int lv = 0;    //레벨도 받음

    float addatk = 0;//추가타 데미지
    float addcri = 0;//추가타 크리티컬 확률

    Enemy enemy; //몬스터 받는 변수 =>  Invoke 안에서 사용하기 위해서 생성
    
    bool addATk1 = false;
    bool addATk2 = false;

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

    public void SetAttack(float _atk, float _cridmg, float _cri, int lv, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
            collision.CompareTag("ShootMonster1")|| collision.CompareTag("ShootMonster2")|| collision.CompareTag("ShootMonster3")|| collision.CompareTag("ShootMonster4")||
            collision.CompareTag("BossMonster1")|| collision.CompareTag("BossMonster2"))
        {
            //몬스터에게 데미지 주는 부분
            collision.GetComponent<Enemy>().TakeDamage(atk,cridmg, cri, etype);
            enemy = collision.GetComponent<Enemy>();
            if(lv == 1)
            {
                addatk = atk * 0.3f; //추가 데미지 30% 적용
            }
            else if(lv > 1)
            {
                addatk = atk * 0.6f; //추가 데미지 60% 적용
            }
            
            if(lv >= 4)
            {
                addcri = cri;       //추가타에도 치명타 적용
            }

            if(lv > 1)
            {
                Invoke("AddAtk1", 0.1f); //추가타 적용
            }
            Debug.Log("takeDamage");
        }
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }

    void AddAtk1() //추가 1타
    {
        enemy.TakeDamage(addatk, cridmg, addcri, etype);
        if (lv == 3)
        {
            Invoke("AddAtk2", 0.1f);
        }
    }

    void AddAtk2() //추가 2타
    {
        enemy.TakeDamage(addatk, cridmg, addcri, etype);
    }

}
