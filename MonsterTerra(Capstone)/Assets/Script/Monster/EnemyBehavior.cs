using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public Transform chargePoint;
    public float fireRate;          // bullet time
    public float fireRange = 5f;    // distance
    private float nextFireTime;
    public bool isCharging = false;

    private Transform player; // player

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        nextFireTime = Time.time;        // init
        fireRate = 0.5f;
        firePoint = transform;       // current pos
        chargePoint = transform;     // current pos
    }

    /// <summary>
    /// Fight Motion
    /// </summary>
    /// <param name="Motion"></param>
    /// 

    public void Shoot(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * 3f; // bullet speed
    }

    public void ShootHoming(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject bulletObject = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));
        EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>(); // Bullet ��ũ��Ʈ ��������


        // Bullet ��ũ��Ʈ�� ShootHoming �Լ� ȣ���Ͽ� �Ѿ��� �÷��̾ ���󰡰� ����
        bullet.ShootHoming(direction, 3f, player);
    }

    public IEnumerator Charging(Vector2 targetPosition, float chargeSpeed, float chargeDistance)
    {
        // Wait for 2 seconds
        isCharging = true;

        yield return new WaitForSeconds(2f);

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        float distanceTraveled = 0f; // �̵��� �Ÿ��� �����ϱ� ���� ���� �߰�
        while (distanceTraveled < chargeDistance) // �̵��� �Ÿ��� ���� �ְ� ���� ����ִ� ���ȿ��� �̵�
        {
            // ��ǥ �������� �̵�
            Vector2 newPosition = (Vector2)transform.position + direction * chargeSpeed * Time.deltaTime;
            transform.position = newPosition;

            // �̵��� �Ÿ� ����
            distanceTraveled += chargeSpeed * Time.deltaTime;

            yield return null;
        }
        isCharging = false; // Charging completed
    }
}