using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponet : MonoBehaviour
{
    [SerializeField] float speed = 10f;     //�Ѿ� �ӵ�
    [SerializeField] int dmg = 2;           //���ݷ�
    Rigidbody2D rb;                         //������ �ٵ� ��Ƶ� ����

    public void Move(Vector3 p)             //���͸� �޾Ƽ� ���
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = p.normalized * speed; //�ش� ��ġ�� �ӵ� �ο�
        Invoke("DestroyBullet", 5);         //5�� �� DestroyBullet ȣ��!!
    }

    private void DestroyBullet()                //�Ѿ� ���ִ� �Լ�
    {
        BulletPoolManager.i.ReturnBullet(gameObject);   //Ǯ�� �ְ� ����
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
