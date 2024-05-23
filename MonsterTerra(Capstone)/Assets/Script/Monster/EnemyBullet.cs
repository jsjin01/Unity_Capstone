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
        rb.velocity = initialDirection * 3f; // �Ѿ� �ӵ�
        isHoming = true;
    }

    void Update()
    {
        if (isHoming)
        {
            // �÷��̾ ���ϴ� ���� ���͸� ���
            Vector2 targetDirection = ((Vector2)target.position - rb.position).normalized;

            // �ʱ� ����� �÷��̾� ���� ���̸� �����Ͽ� �Ѿ��� ������ ����
            Vector2 newDirection = Vector2.Lerp(rb.velocity.normalized, targetDirection, homingSpeed * Time.deltaTime);
            rb.velocity = newDirection * rb.velocity.magnitude; // �ӵ� ����
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
