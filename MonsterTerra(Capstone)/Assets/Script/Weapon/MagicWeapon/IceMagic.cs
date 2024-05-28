using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagic : WeaponComponent
{
    public static IceMagic i;
    [SerializeField] GameObject bullet; // ����ü
    bool lvMax = false;
    float dur = 0f;
    float amount = 0f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 0.5f;
        weaponmulatkspd = 3.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //CrossBow Arrow ����
        GameObject iceBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        iceBullet.GetComponentInChildren<MagicBullet>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        iceBullet.GetComponentInChildren<MagicBullet>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            debuffType = 7;
        }
        else if (lv == 2)
        {
            //ȭ�� �߰�
        }
        else if (lv == 3)
        {
            weaponmulatkspd -= 0.5f; // ���� �ӵ� ��� 0.5 ����
        }
        else if (lv == 4)
        {
            //ȭ�� �߰�
        }
        else if (lv == 5)
        {
            dur = 3000f;//���� ������ ����
            lvMax = true;
        }
        else
        {
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
        }

        if (lv == 6)
        {
            lv--;
        }
    }
}