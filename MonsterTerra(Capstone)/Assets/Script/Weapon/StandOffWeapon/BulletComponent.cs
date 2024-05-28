using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    //���ݷ�, ũ��Ƽ�� ������, ũ��Ƽ�� Ȯ��, ȿ�� �ο�
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    float dur = 0;
    float amount = 0;
    Rigidbody2D rb;

    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;
    float bulletTime = 0f; //�Ҹ��� ����ִ� �ð�

    [SerializeField] SOTYPE Type;     //������ �Ѿ����� ����
    [SerializeField] bool isdirdmg = false;  //�Ÿ� ��� ������
    [SerializeField] bool isguided = false;  //�����Ǵ��� ����

    Transform[] Enemy;
    Transform target;

    private void Start()
    {
        Invoke("DestroyBullet", dTime); //���� �ð� ���� ����
        FindClosestEnemy();
    }

    void Update()
    {
        if (isdirdmg) // ���󰡴� ���� ���ִ� �ð�
        {
            bulletTime += Time.deltaTime;
        }
        if (isguided)
        {
            GuidedBullet();
        }
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
    }

    void DestroyBullet()
    {
        Destroy(parent); //�����ð� ���� ����
    }

    private void OnTriggerEnter2D(Collider2D collision) //���Ͷ� �浹�ϸ� ����
    {
        if (collision.CompareTag("RushMonster1") || collision.CompareTag("RushMonster2") || collision.CompareTag("RushMonster3") || collision.CompareTag("RushMonster4") ||
           collision.CompareTag("ShootMonster1") || collision.CompareTag("ShootMonster2") || collision.CompareTag("ShootMonster3") || collision.CompareTag("ShootMonster4") ||
           collision.CompareTag("BossMonster1") || collision.CompareTag("BossMonster2"))
        {
            collision.GetComponent<Enemy>().TakeDamage(atk, cridmg, cri, etype); // ���Ϳ��� ������ �ִ� �κ�
            if (isdirdmg)
            {
                collision.GetComponent<Enemy>().TakeTrueDamage(bulletTime * speed * 0.5f, cridmg, cri); //�Ÿ� ��� ������ 
            }
            CancelInvoke("DestroyBullet");
            DestroyBullet();
        }
    }

    public void SetMax()
    {
        if(Type == SOTYPE.SR)
        {
            isdirdmg = true;
        }
        if(Type == SOTYPE.AR || Type == SOTYPE.CROSSBOW)
        {
            isguided = true;
        }
    }
    private void GuidedBullet() //�����Ǵ� �κ�
    {
        FindClosestEnemy();
        //���͵� �� �Ѿ˰� ���� ����� �κп� �ִ� ���Ϳ��� ����
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle ));

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
