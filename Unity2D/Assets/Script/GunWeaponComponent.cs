using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum GUN //�� ����
{
    HANDGUN,    //����
    SHOTGUN,    //����
    RIFFLE      //����
}

public enum DIRECTION // ���� => �÷��̾�� ����
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class GunWeapon : MonoBehaviour
{

    Vector3 Pos = Vector3.zero;                 //���� ��ġ
    Vector3 aimPos = Vector3.zero;              //�Ѿ��� ���󰡴� ��ġ
    Quaternion rotation = Quaternion.identity;  //�Ѿ� ����=> sprite �����ϴµ� ���

    int atk = 10;               //���ݷ�
    float atkSpd = 0.1f;        //���ݼӵ�
    GUN Type = GUN.HANDGUN;     //����Ÿ��
    bool isShot = true;         //�߻� ���� �Ǵ� ����

    void Start()
    {
        Pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (isShot)
            {
                Shoot(DIRECTION.RIGHT);
            }
        }
    }

    void Shoot(DIRECTION dir)
    {
        StartCoroutine(ShootCol());
        Aim(dir);
        BulletPoolManager.i.UseBullet(Pos, aimPos, rotation);
    }

    void Aim(DIRECTION dir) // ������ ����� ���� ����
    {
        if(dir == DIRECTION.UP)
        {
            rotation = Quaternion.Euler(0,0,0);
            aimPos = new Vector3(0,1,0);
        }
        else if (dir == DIRECTION.DOWN)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            aimPos = new Vector3(0, -1, 0);
        }
        else if (dir == DIRECTION.LEFT)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            aimPos = new Vector3(-1, 0, 0);
        }
        else if (dir == DIRECTION.RIGHT)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            aimPos = new Vector3(1, 0, 0);
        }
    }

    IEnumerator ShootCol()
    {
        isShot = false;
        yield return new WaitForSeconds(atkSpd);
        isShot = true;
    }

}
