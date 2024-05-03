using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeWeaponVFX : MonoBehaviour
{
    float atk = 5;
    [SerializeField] GameObject parent;
    [SerializeField] float dTime = 1.0f;

    private void Start()
    {
        Destroy(parent, dTime);
    }

    public void SetAttack(float _atk, GameObject obj = null) //데미지 설정
    {
        atk = _atk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Enemy>().TakeDamage(atk);
            Debug.Log("ss");
        }
    }

    public void DestroyWeapon(float dt = 0)
    {
        Destroy(parent, dt);
    }
}
