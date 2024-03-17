using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponet : MonoBehaviour
{
    [SerializeField] float speed = 10f;     //총알 속도
    [SerializeField] int dmg = 2;           //공격력
    Rigidbody2D rb;                         //리지드 바디 담아둘 변수

    public void Move(Vector3 p)             //벡터를 받아서 사용
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.velocity = p.normalized * speed; //해당 위치로 속도 부여
        Invoke("DestroyBullet", 5);         //5초 뒤 DestroyBullet 호출!!
    }

    private void DestroyBullet()                //총알 없애는 함수
    {
        BulletPoolManager.i.ReturnBullet(gameObject);   //풀에 넣고 재사용
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
        //몬스터가 피해를 입는 부분       
        }
        CancelInvoke("DestroyBullet");      //몇 초뒤에 삭제한다는 명령을 없앰
        DestroyBullet();                    //바로 삭제
    }
}
