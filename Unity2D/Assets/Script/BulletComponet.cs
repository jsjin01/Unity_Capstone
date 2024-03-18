using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletComponet : MonoBehaviour
{
    [SerializeField] float speed = 10f;     //�Ѿ� �ӵ�
    [SerializeField] int dmg;               //������
    Rigidbody2D rb;

    GUN Type;     //�Ѿ� ����
    [SerializeField] Sprite[] bulletSprites; //��������Ʈ ����
    SpriteRenderer sr;                       //��������Ʈ �������� ������


    public void Move(Vector3 p)             //���͸� �޾Ƽ� ���
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        dmg = GunWeaponComponent.i.atk;
        Type = GunWeaponComponent.i.Type;
        StateChange(Type);
        rb.velocity = p.normalized * speed; //�ش� ��ġ�� �ӵ� �ο�
        Invoke("DestroyBullet", 5);         //5�� �� DestroyBullet ȣ��!!
    }

    private void DestroyBullet()                //�Ѿ� ���ִ� �Լ�
    {
        BulletPoolManager.i.ReturnBullet(gameObject);   //Ǯ�� �ְ� ����
    }

    private void StateChange(GUN t) //���� ��ȭ
    {
     
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }


        if (t == GUN.HANDGUN)
        {
            sr.sprite = bulletSprites[0];
        }
        else if(t == GUN.SHOTGUN)
        {
            sr.sprite = bulletSprites[1];
        }
        else if(t == GUN.RIFFLE)
        {
            sr.sprite = bulletSprites[2];
            Debug.Log("d");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
        //���Ͱ� ���ظ� �Դ� �κ�       
        }
        CancelInvoke("DestroyBullet");      //�� �ʵڿ� �����Ѵٴ� ����� ����
        DestroyBullet();                    //�ٷ� ����
    }
}
