using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
     public static BulletPoolManager i;
    [SerializeField] GameObject bulletPrefab;      //�Ѿ� prefebs�� ��Ƶδ� ����
    [SerializeField] int initBulletCount = 20;      //���� �� ������ �� �Ѿ� ����


    private void Awake()
    {
        i = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBullet(initBulletCount);
    }

    void CreateBullet(int cnt = 1)//�Ѿ� ���� �Լ�
    {
        for(int i = 0; i < cnt; i++ )
        {
            Instantiate(bulletPrefab,transform);
        }
    }

    public void UseBullet(Vector3 p, Vector3 m, Quaternion rot) //(���� ��ġ, �߻� ����, �Ѿ� ����) �Ѿ� ����
    {
        if (transform.childCount == 0) CreateBullet();      //Ǯ�ȿ� �Ѿ��� ���ٸ� ���� ����

        BulletComponet b = transform.GetChild(0).GetComponent<BulletComponet>(); 

        b.transform.position = p;               //���� ��ġ
        b.transform.rotation = rot;             //����
        b.gameObject.SetActive(true);           //������Ʈ Ȱ��ȭ
        b.transform.parent = null;              //�θ� ���� ����
        b.Move(m);                              //�Ѿ� �߻�
    }

    public void ReturnBullet(GameObject b)      //����...������Ʈ�� ��Ȱ��ȭ���Ѽ� �Ⱥ��̰� ����
    {
        b.SetActive(false);                     
        b.transform.SetParent(transform);       
    }
}
