using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBulletPoison : MonoBehaviour
{
    [SerializeField] MWTYPE magicT;
    int idx = 0;
    [SerializeField] float speed = 10f;
    //���ݷ�, ũ��Ƽ�� ������, ũ��Ƽ�� Ȯ��, ȿ�� �ο�
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

    //���� ��ɿ� ���
    Transform[] Enemy;
    Transform target;

    private void Start()
    {
        idx = PoisonMagic.i.idxlv;
        Invoke("DestroyBullet", dTime); //���� �ð� ���� ����
        FindClosestEnemy();
    }

    void Update()
    {
        GuidedBullet();
    }
    public void Move(Vector3 p)      //�� �������� �� ����      
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
            rb.velocity = p.normalized * speed; //���������� ������ �κ�
        }
    }

    public void SetAttack(float _atk, float _cridmg, float _cri, float _dur = 0, float _amount = 0, int _etype = 0, GameObject obj = null) //������ ����
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
        iceParticle.GetComponentInChildren<MagicVFX>().SetAttack(atk / 10, cridmg, cri, dur, amount, etype); //���� ������ ����
        Destroy(parent); //����
    }

    private void OnTriggerEnter2D(Collider2D collision) //���Ͷ� �浹�ϸ� ����
    {
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
           collision.CompareTag("ShootMonster1") || collision.CompareTag("ShootMonster2") || collision.CompareTag("ShootMonster3") || collision.CompareTag("ShootMonster4") ||
           collision.CompareTag("BossMonster1") || collision.CompareTag("BossMonster2"))
        {
            collision.GetComponent<Enemy>().TakeDamage(atk, cridmg, cri, etype, dur, amount); // ���Ϳ��� ������ �ִ� �κ�
            CancelInvoke("DestroyBullet");
            DestroyBullet();
        }
    }

    private void GuidedBullet() //�����Ǵ� �κ�
    {
        FindClosestEnemy();
        //���͵� �� �Ѿ˰� ���� ����� �κп� �ִ� ���Ϳ��� ����
        if (target == null)
        {
            // target�� null�̸� �ƹ� �۾��� �������� �ʰ� �޼��带 �����մϴ�.
            return;
        }
        Vector2 targetDirection = ((Vector2)target.position - rb.position).normalized;

        // ���ο� ���� ���
        Vector2 newDirection = Vector2.Lerp(rb.velocity.normalized, targetDirection, speed * Time.deltaTime);

        // ���� ���ο� ������ ��ǥ ���⿡ ����� ������ ������
        if (Vector2.Dot(newDirection, targetDirection) < 0.99f)
        {
            // ���ο� ������ �̿��� �ӵ��� ����
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

