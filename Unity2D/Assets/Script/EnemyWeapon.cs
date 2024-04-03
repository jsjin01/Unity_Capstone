using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate; // bullet time
    public float fireRange = 5f; // distance
    private float nextFireTime;

    private Transform player; // player

    void Start()
    {
        player = GameManager.instance.player.transform;
        nextFireTime = Time.time; // init
        fireRate = 0.5f;
        firePoint = transform; // current pos
    }

    void Update()
    {
        // check
        if (Time.time > nextFireTime && Vector2.Distance(transform.position, player.position) <= fireRange)
        {
            Debug.Log("Fire condition met at time: " + Time.time);
            Debug.Log("Next fire time: " + nextFireTime);
            ShootAtPlayer(player.position); // player pos
            nextFireTime = Time.time + 1f / fireRate;//1f / fireRate; // 1 per sec
        }
    }

    void ShootAtPlayer(Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (targetPosition - (Vector2)firePoint.position).normalized;
        rb.velocity = direction * 3f; // bullet speed
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}