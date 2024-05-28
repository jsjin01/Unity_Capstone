using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    //���ݷ�, ũ��Ƽ�� ������, ũ��Ƽ�� Ȯ��, ȿ�� �ο�
    float atk;
    float cridmg;
    float cri;
    int etype = 0;
    Rigidbody2D rb;

    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;
    float bulletTime = 0f; //�Ҹ��� ����ִ� �ð�

    [SerializeField] SOTYPE Type;     //������ �Ѿ����� ����
    [SerializeField] bool isdirdmg = false;  //�Ÿ� ��� ������
    [SerializeField] bool isguided = false;  //�����Ǵ��� ����

    void Update()
    {
        if (isdirdmg) // ���󰡴� ���� ���ִ� �ð�
        {
            bulletTime += Time.deltaTime;
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

    public void SetAttack(float _atk, float _cridmg, float _cri, int _etype = 0, GameObject obj = null) //������ ����
    {
        atk = _atk;
        cridmg = _cridmg;
        cri = _cri;
        etype = _etype;
    }
    private void Start()
    {
        Invoke("DestroyBullet", dTime); //���� �ð� ���� ����
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
        if(Type == SOTYPE.AR)
        {
            isguided = true;
        }
    }
    private void GuidedBullet() //�����Ǵ� �κ�
    {
        //���͵� �� �Ѿ˰� ���� ����� �κп� �ִ� ���Ϳ��� ����
    }

}
