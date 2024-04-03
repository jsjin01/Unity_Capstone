using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float bulletLifetime = 5f;

    void Start()
    {
        Destroy(gameObject, bulletLifetime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
