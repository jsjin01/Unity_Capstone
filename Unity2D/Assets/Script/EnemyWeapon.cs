using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
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

    //void Update()
    //{
    //    // check
    //    if (Time.time > nextFireTime && Vector2.Distance(transform.position, player.position) <= fireRange)
    //    {
    //      //Debug.Log("Fire condition met at time: " + Time.time);
    //        Debug.Log("Next fire time: " + nextFireTime);
    //        Fire(player.position); // player pos
    //        nextFireTime = Time.time + 1f / fireRate;//1f / fireRate; // 1 per sec
    //    }
    //}

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
        float elapsedTime = 0f;

        yield return new WaitForSeconds(2f);

        isCharging = false; // Charging completed
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        while (Vector2.Distance(transform.position, targetPosition) > 0.7f) //target Position Stop
        {
            // 목표 지점으로 이동
            Vector2 newPosition = (Vector2)transform.position + direction * chargeSpeed * Time.deltaTime;
            transform.position = newPosition;

            yield return null;
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, fireRange);
    //}
}