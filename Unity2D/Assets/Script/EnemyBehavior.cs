using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public Transform chargePoint;
    public float fireRate; // bullet time
    public float fireRange = 5f; // distance
    private float nextFireTime;
    public bool isCharging = false;

    private Transform player; // player

    void Start()
    {
        player = GameManager.instance.player.transform;
        nextFireTime = Time.time; // init
        fireRate = 0.5f;
        firePoint = transform; // current pos
        chargePoint = transform; // current pos
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

    public IEnumerator Charging(Vector2 targetPosition, float chargeSpeed)
    {
        // Wait for 2 seconds
        isCharging = true;
        float chargeDelay = 2f;

        yield return new WaitForSeconds(2f);

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        float elapsedTime = 0f;
        while (Vector2.Distance(transform.position, targetPosition) > 0.1f) //target Position Stop
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 3f)
            {
                isCharging = false; // Charging completed
                yield break;
            }
            // 목표 지점으로 이동
            Vector2 newPosition = (Vector2)transform.position + direction * chargeSpeed * Time.deltaTime;
            transform.position = newPosition;

            yield return null;
        }
        isCharging = false; // Charging completed
    }
}