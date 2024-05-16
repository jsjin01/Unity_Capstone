using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    //공격력, 크리티컬 데미지, 크리티컬 확률, 효과 부여
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    Rigidbody2D rb;

    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;

    [SerializeField]SOTYPE Type;     //누구의 총알인지 구별
    [SerializeField] bool isguided = false;  //유도되는지 여부

    public void Move(Vector3 p)      //쏜 방향으로 쭉 날라감      
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = p.normalized * speed; //일직선으로 나가는 부분
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, int _etype = 0, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
        etype = _etype;
    }
    private void Start()
    {
        Invoke("DestroyBullet", dTime); //일정 시간 이후 삭제
    }

    void DestroyBullet()
    {
        Destroy(parent); //일정시간 이후 삭제
    }

    private void OnTriggerEnter2D(Collider2D collision) //몬스터랑 충돌하면 삭제
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Enemy>().TakeDamage(atk, cridmg, cri, etype); // 몬스터에게 데미지 주는 부분
            CancelInvoke("DestroyBullet");
            DestroyBullet();
        }
    }

    private void GuidedBullet() //유도되는 부분
    {
        //몬스터들 중 총알과 가장 가까운 부분에 있는 몬스터에게 날라감
    }
}
