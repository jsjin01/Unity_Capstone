using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] float speed = 10f;    
    [SerializeField] public int dmg;             
    Rigidbody2D rb;

    SOTYPE Type;    
    [SerializeField] Sprite[] bulletSprites; 
    SpriteRenderer sr;                      
    public void Move(Vector3 p)      //�� �������� �� ����      
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        dmg = 1;
        //Type = GunWeaponComponent.i.type;
        rb.velocity = p.normalized * speed; 
        Invoke("DestroyBullet", 5);        
    }

    private void DestroyBullet()                
    {
        //BulletPoolManager.i.ReturnBullet(gameObject);  
    }

    private void OnTriggerEnter2D(Collider2D collision) //���Ͷ� �浹�ϸ� ����
    {
        if (collision.CompareTag("Enemy"))
        {
            CancelInvoke("DestroyBullet");
            DestroyBullet();
        }
    }
}
