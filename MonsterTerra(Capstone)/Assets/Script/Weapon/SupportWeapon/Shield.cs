using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : WeaponComponent
{
    public static Shield i;
    [SerializeField] GameObject particle; //���� ��ƼŬ

    public IEnumerator ShieldCor;
    float dur = 3f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 2f;
        weaponmulatkspd = 5.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        SoundManger.i.PlaySound(10);
        if (endAtkSpd < dur) //���� ���ݼӵ��� �������ӽð����� ª�� �� ����
        {
            endAtkSpd = dur; 
        }
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

        //ICE Bullet ����
        GameObject iceBullet = Instantiate(particle, GamePlayerMoveControl.i.playerPos, rotation, transform);
        ShieldCor = GamePlayerManager.i.Shield(dmg, dur);
        StartCoroutine(ShieldCor);

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            dur = 5f; //���ӽð� 5�ʷ� ����
        }
        else if (lv == 2)
        {
            weaponmulatk += 1f; //��ȣ�� 100% ��� ����
        }
        else if (lv == 3)
        {
            weaponmulatk -= 2f; //��� ����
        }
        else
        {
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
        }

        if (lv == 4)
        {
            lv--;
        }
    }
}

