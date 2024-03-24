using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum GUN //?? ????
{
    HANDGUN,    //????
    SHOTGUN,    //????
    RIFFLE      //????
}

public enum DIRECTION // ???? => ?��?????? ????
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}
public class GunWeaponComponent : MonoBehaviour
{
    public static GunWeaponComponent i;

    Vector3 Pos = Vector3.zero;                 //???? ???
    Vector3 aimPos = Vector3.zero;              //????? ?????? ???
    Quaternion rotation = Quaternion.identity;  //??? ????=> sprite ??????��? ???

    public int atk { get; private set; } = 10;              //????? //??????? ??????? ?? ?????? ???? ?????
    float atkSpd = 0.1f;                                    //??????
    public GUN Type { get; private set; } = GUN.HANDGUN;     //???????
    public bool isShot = true;                                      //??? ???? ??? ????

    [SerializeField] Sprite[] gunSprite;    //????????? ????
    [SerializeField] SpriteRenderer sr;     //??????????????? ????

    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        Pos = transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            if (isShot)
            {
                Shoot(DIRECTION.RIGHT);
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            sr.sprite = gunSprite[2];
            Type = GUN.RIFFLE;
        }
    }

    public void Shoot(DIRECTION dir)
    {
        StartCoroutine(ShootCol());
        Aim(dir);
        BulletPoolManager.i.UseBullet(Pos, aimPos, rotation);
    }

    void Aim(DIRECTION dir) // ?????? ????? ???? ????
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
