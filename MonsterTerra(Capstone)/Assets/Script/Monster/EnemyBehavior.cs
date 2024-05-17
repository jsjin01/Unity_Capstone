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
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        rb.velocity = direction * 3f; // bullet speed
    }

    public void ShootHoming(Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        rb.velocity = direction * 3f; // bullet speed
    }

    public IEnumerator Charging(Vector2 targetPosition, float chargeSpeed, float chargeDistance)
    {
        // Wait for 2 seconds
        isCharging = true;

        yield return new WaitForSeconds(2f);

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        float distanceTraveled = 0f; // 이동한 거리를 추적하기 위한 변수 추가
        while (distanceTraveled < chargeDistance) // 이동한 거리가 chargeDistance보다 작을 때까지 반복
        {
            // 목표 지점으로 이동
            Vector2 newPosition = (Vector2)transform.position + direction * chargeSpeed * Time.deltaTime;
            transform.position = newPosition;

            // 이동한 거리 갱신
            distanceTraveled += chargeSpeed * Time.deltaTime;

            yield return null;
        }
        isCharging = false; // Charging completed
    }
}