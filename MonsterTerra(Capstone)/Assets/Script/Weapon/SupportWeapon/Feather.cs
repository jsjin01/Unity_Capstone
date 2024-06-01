using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : WeaponComponent
{
    public static Feather i;
    [SerializeField] GameObject particle; //���� ��ƼŬ

    float dur = 3f;
    bool max = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1f;
        weaponmulatkspd = 7.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        if (endAtkSpd < dur) //���� ���ݼӵ��� �������ӽð����� ª�� �� ����
        {
            endAtkSpd = dur;
        }
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

        //ICE Bullet ����
        GameObject iceBullet = Instantiate(particle, GamePlayerMoveControl.i.playerPos, rotation, transform);
        StartCoroutine(GamePlayerManager.i.Feather(dmg, dur));

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
            weaponmulatk += 1f; //ȿ�� ���� 2�� ����
        }
        else if (lv == 3)
        {
            weaponmulatk -= 3f; //��� ����
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
