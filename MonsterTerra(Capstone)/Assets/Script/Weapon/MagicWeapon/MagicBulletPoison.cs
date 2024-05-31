using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletPoison : MonoBehaviour
{
    [SerializeField] MWTYPE magicT;
    int idx = 0;
    [SerializeField] float speed = 10f;
    //공격력, 크리티컬 데미지, 크리티컬 확률, 효과 부여
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    float dur = 0;
    float amount = 0;

    Rigidbody2D rb;

    [SerializeField] GameObject[] magicVFX;
    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;

    //유도 기능에 사용
    Transform[] Enemy;
    Transform target;

    private void Start()
    {
        idx = PoisonMagic.i.idxlv;
        Invoke("DestroyBullet", dTime); //일정 시간 이후 삭제
        FindClosestEnemy();
    }

    void Update()
    {
        GuidedBullet();
    }
    public void Move(Vector3 p)      //쏜 방향으로 쭉 날라감      
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (p == new Vector3(0, 0, 0))
        {
            Vector3 q = new Vector3(1, 0, 0);
            rb.velocity = q.normalized * speed;
        }
        else
        {
            rb.velocity = p.normalized * speed; //일직선으로 나가는 부분
        }
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

    void DestroyBullet()
    {
        GameObject iceParticle = Instantiate(magicVFX[idx], transform.position, Quaternion.Euler(0, 0, 0), GameObject.Find("IceMagic").transform);
        iceParticle.GetComponentInChildren<MagicVFX>().SetAttack(atk / 10, cridmg, cri, dur, amount, etype); //장판 데미지 설정
        Destroy(parent); //삭제
    }

    private void OnTriggerEnter2D(Collider2D collision) //몬스터랑 충돌하면 삭제
    {
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
           collision.CompareTag("ShootMonster1") || collision.CompareTag("ShootMonster2") || collision.CompareTag("ShootMonster3") || collision.CompareTag("ShootMonster4") ||
           collision.CompareTag("BossMonster1") || collision.CompareTag("BossMonster2"))
        {
            collision.GetComponent<Enemy>().TakeDamage(atk, cridmg, cri, etype, dur, amount); // 몬스터에게 데미지 주는 부분
            CancelInvoke("DestroyBullet");
            DestroyBullet();
        }
    }

    private void GuidedBullet() //유도되는 부분
    {
        FindClosestEnemy();
        //몬스터들 중 총알과 가장 가까운 부분에 있는 몬스터에게 날라감
        if (target == null)
        {
            // target이 null이면 아무 작업도 수행하지 않고 메서드를 종료합니다.
            return;
        }
        Vector2 targetDirection = ((Vector2)target.position - rb.position).normalized;

        // 새로운 방향 계산
        Vector2 newDirection = Vector2.Lerp(rb.velocity.normalized, targetDirection, speed * Time.deltaTime);

        // 만약 새로운 방향이 목표 방향에 충분히 가깝지 않으면
        if (Vector2.Dot(newDirection, targetDirection) < 0.99f)
        {
            // 새로운 방향을 이용해 속도를 조정
            rb.velocity = newDirection * speed;
        }

        float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    void FindClosestEnemy()
    {
        Enemy = GameObject.Find("EnemyPool").GetComponentsInChildren<Transform>();
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Transform enemy in Enemy)
        {
            float distance = Vector2.Distance(transform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;
        }
    }
}


