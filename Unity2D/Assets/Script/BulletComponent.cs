using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 20f;     //�Ѿ� �ӵ�
    [SerializeField] int dmg = 1;           //������
    Rigidbody2D rb;

    public void Move()
    {
        if (rb != null) 
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = transform.position.normalized * speed;
    }

    public void DestroyBullet() //�Ѿ��� �����
    {
        BulletPoolManager.i.ReturnBullet(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //���� ������ �Դ� �κ� 
        }
    }
}
