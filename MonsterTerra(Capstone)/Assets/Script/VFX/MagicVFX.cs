using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicVFX : MonoBehaviour
{
    float atk = 5;
    float cridmg = 0;
    float cri = 0;
    //효과창
    int etype = 0;
    float dur = 0f;
    float amount = 0f;

    float DmgRate = 0.5f;
    bool candmg = true;
    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;

    private void Start()
    {
        Destroy(parent, dTime);
    }


    public void SetAttack(float _atk, float _cridmg, float _cri, float _dur = 0, float _amount = 0, int _etype = 0, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;

        etype = _etype;
        dur = _dur;
        amount = _amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!candmg)
        {
            return;
        }
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
           collision.CompareTag("ShootMonster1") || collision.CompareTag("ShootMonster2") || collision.CompareTag("ShootMonster3") || collision.CompareTag("ShootMonster4") ||
           collision.CompareTag("BossMonster1") || collision.CompareTag("BossMonster2"))
        {
            //몬스터에게 데미지 주는 부분
            collision.GetComponent<Enemy>().TakeDamage(atk, cridmg, cri, etype);
            Debug.Log("takeDamage");
        }
        StartCoroutine(Rate());
        
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }

    IEnumerator Rate() //딜 들어가는 대기시간
    {
        candmg = false;
        yield return new WaitForSeconds(DmgRate);
        candmg = true;
    }
}


