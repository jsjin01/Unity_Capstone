using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager i;
    [SerializeField] GameObject bulletPrefab;       
    [SerializeField] int initBulletCount = 20;      


    private void Awake()
    {
        i = this;
    }

    void Start()
    {
        CreateBullet(initBulletCount);
    }

    void CreateBullet(int cnt = 1)      //총알 만드는 메서드
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(bulletPrefab, transform);   
        }
    }

    public void UseBullet(Vector3 p, Quaternion rot) 
    {
        if (transform.childCount == 0)
        {
            CreateBullet();
        }

        BulletComponent b = transform.GetChild(0).GetComponent<BulletComponent>();  

        b.transform.position = p;              
        b.transform.rotation = rot;            
        b.gameObject.SetActive(true);           
        b.transform.parent = null;              
        b.Move();                             
    }

    public void ReturnBullet(GameObject b)
    {
        b.SetActive(false);                     
        b.transform.SetParent(transform);       
    }
}
