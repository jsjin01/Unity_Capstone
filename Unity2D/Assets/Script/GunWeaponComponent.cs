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
public class GunWeaponComponent : MonoBehaviour
{
    public static GunWeaponComponent i;

    Vector3 Pos = Vector3.zero;                 //���� ��ġ
    Vector3 aimPos = Vector3.zero;              //�Ѿ��� ���󰡴� ��ġ
    Quaternion rotation = Quaternion.identity;  //�Ѿ� ����=> sprite �����ϴµ� ���

    public int atk { get; private set; } = 10;              //���ݷ� //�ۿ����� �����پ� �� ������ ���� �Ұ���
    float atkSpd = 0.1f;                                    //���ݼӵ�
    public GUN Type { get; private set; } = GUN.HANDGUN;     //����Ÿ��
    bool isShot = true;                                      //�߻� ���� �Ǵ� ����

    [SerializeField] Sprite[] gunSprite;    //��������Ʈ ����
    [SerializeField] SpriteRenderer sr;     //��������Ʈ������ ����

    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        Pos = transform.position;
        sr = GetComponent<SpriteRenderer>();
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
        if (Input.GetKey(KeyCode.Q))
        {
            sr.sprite = gunSprite[2];
            Type = GUN.RIFFLE;
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
