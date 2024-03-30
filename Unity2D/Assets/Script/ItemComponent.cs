using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Magnet,
    Potion,
    Annihilation,
    Turret,
    Mine,
    WeaponEnhancement,
}

public abstract class ItemComponent : MonoBehaviour
{
    [SerializeField] int hp = 3;
    [SerializeField] int dTime = 7;
    [SerializeField] protected ItemType t;

    private void Start()
    {
        Destroy(gameObject, dTime);
    }

    public void TakeDamage(int dmg)
    {
        if (hp <= 0) return;

        hp -= dmg;

        if (hp <= 0)
        {
            DestroyItem();
        }
    }

    void DestroyItem()
    {
        GetItem();
        Destroy(gameObject);
     }

    protected abstract void GetItem();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("아이템을 획득했습니다 ");
            GetItem();
            Destroy(gameObject);

        }
    }
}




