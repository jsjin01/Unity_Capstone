using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagic : WeaponComponent
{
    public static IceMagic i;
    [SerializeField] GameObject bullet; // ����ü
    [SerializeField] GameObject LvMax; // ���� ��ų
    bool lvMax = false;
    public int idxlv = 0;

    float dur = 3f;
    float amount = 0.2f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1f;
        weaponmulatkspd = 15.0f;
        debuffType = 4;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //ICE Bullet ����
        GameObject iceBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        iceBullet.GetComponentInChildren<MagicBullet>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        iceBullet.GetComponentInChildren<MagicBullet>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�

        if (lvMax)
        {
            GameObject iceWorld = Instantiate(LvMax, GamePlayerMoveControl.i.playerPos, Quaternion.Euler(0,0,0), transform);
            iceWorld.transform.GetChild(0).GetComponentInChildren<MagicVFX>().SetAttack(dmg / 10, endCriDmg, endCri, dur, amount, debuffType);
        }

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            idxlv++;//���� ����
        }
        else if (lv == 2)
        {
            amount *= 2f;//��ȭ�� 2�� ����
        }
        else if (lv == 3)
        {
            dur = 5; //��ȭ �ð� 5�ʷ� ����
        }
        else if (lv == 4)
        {
           weaponmulatkspd  -= 5f;
        }
        else if (lv == 5)
        {
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
