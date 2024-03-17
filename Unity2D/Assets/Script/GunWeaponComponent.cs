using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum GUN //총 종류
{
    HANDGUN,    //권총
    SHOTGUN,    //샷건
    RIFFLE      //소총
}

public enum DIRECTION // 방향 => 플레이어에서 받음
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class GunWeapon : MonoBehaviour
{

    Vector3 Pos = Vector3.zero;                 //현재 위치
    Vector3 aimPos = Vector3.zero;              //총알이 날라가는 위치
    Quaternion rotation = Quaternion.identity;  //총알 각도=> sprite 조정하는데 사용

    int atk = 10;               //공격력
    float atkSpd = 0.1f;        //공격속도
    GUN Type = GUN.HANDGUN;     //무기타입
    bool isShot = true;         //발사 가능 판단 변수

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

    void Aim(DIRECTION dir) // 나가는 방향과 각도 수정
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
