using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;
    public float bulletLifetime = 5f;
    Rigidbody2D rb;
    Vector2 initialDirection;
    float homingSpeed;
    Transform target;
    bool isHoming = false;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    public void ShootHoming(Vector2 initialDirection, float homingSpeed, Transform target)
    {
        this.initialDirection = initialDirection;
        this.homingSpeed = homingSpeed;
        this.target = target;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialDirection * 3f; // 총알 속도
        isHoming = true;
    }

    void Update()
    {
        if (isHoming)
        {
            // 플레이어를 향하는 방향 벡터를 계산
            Vector2 targetDirection = ((Vector2)target.position - rb.position).normalized;

            // 초기 방향과 플레이어 방향 사이를 보간하여 총알의 방향을 조정
            Vector2 newDirection = Vector2.Lerp(rb.velocity.normalized, targetDirection, homingSpeed * Time.deltaTime);
            rb.velocity = newDirection * rb.velocity.magnitude; // 속도 유지
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GamePlayerMoveControl.i.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
