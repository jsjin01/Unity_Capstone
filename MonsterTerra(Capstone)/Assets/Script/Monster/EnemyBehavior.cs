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
        EnemyBullet bullet = bulletObject.GetComponent<EnemyBullet>(); // Bullet 스크립트 가져오기


        // Bullet 스크립트의 ShootHoming 함수 호출하여 총알을 플레이어를 따라가게 만듦
        bullet.ShootHoming(direction, 3f, player);
    }

    public IEnumerator Charging(Vector2 targetPosition, float chargeSpeed, float chargeDistance)
    {
        // Wait for 2 seconds
        isCharging = true;

        yield return new WaitForSeconds(2f);

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        float distanceTraveled = 0f; // 이동한 거리를 추적하기 위한 변수 추가
        while (distanceTraveled < chargeDistance) // 이동할 거리가 남아 있고 적이 살아있는 동안에만 이동
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