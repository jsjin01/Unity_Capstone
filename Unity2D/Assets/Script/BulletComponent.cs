using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 20f;     //총알 속도
    [SerializeField] int dmg = 1;           //데미지
    Rigidbody2D rb;

    public void Move()
    {
        if (rb != null) 
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = transform.position.normalized * speed;
    }

    public void DestroyBullet() //총알이 사라짐
    {
        BulletPoolManager.i.ReturnBullet(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //적이 데미지 입는 부분 
        }
    }
}
