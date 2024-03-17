using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
     public static BulletPoolManager i;
    [SerializeField] GameObject bulletPrefab;      //총알 prefebs을 담아두는 변수
    [SerializeField] int initBulletCount = 20;      //시작 시 생성해 둘 총알 갯수


    private void Awake()
    {
        i = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBullet(initBulletCount);
    }

    void CreateBullet(int cnt = 1)//총알 생성 함수
    {
        for(int i = 0; i < cnt; i++ )
        {
            Instantiate(bulletPrefab,transform);
        }
    }

    public void UseBullet(Vector3 p, Vector3 m, Quaternion rot) //(생성 위치, 발사 방향, 총알 방향) 총알 생성
    {
        if (transform.childCount == 0) CreateBullet();      //풀안에 총알이 없다면 새로 생성

        BulletComponet b = transform.GetChild(0).GetComponent<BulletComponet>(); 

        b.transform.position = p;               //생성 위치
        b.transform.rotation = rot;             //각도
        b.gameObject.SetActive(true);           //오브젝트 활성화
        b.transform.parent = null;              //부모 설정 해제
        b.Move(m);                              //총알 발사
    }

    public void ReturnBullet(GameObject b)      //삭제...오브젝트를 비활성화시켜서 안보이게 만듬
    {
        b.SetActive(false);                     
        b.transform.SetParent(transform);       
    }
}
