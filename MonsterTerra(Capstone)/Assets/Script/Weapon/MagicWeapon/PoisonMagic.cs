using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMagic : WeaponComponent
{
    public static PoisonMagic i;
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
        debuffType = 5;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //Posion Bullet ����
        GameObject PoisonBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        PoisonBullet.GetComponentInChildren<MagicBullet>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        PoisonBullet.GetComponentInChildren<MagicBullet>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�

        if (lvMax)
        {
            GameObject PoisonWorld = Instantiate(LvMax, GamePlayerMoveControl.i.playerPos, Quaternion.Euler(0, 0, 0), transform);
            PoisonWorld.transform.GetChild(0).GetComponentInChildren<MagicVFX>().SetAttack(dmg / 10, endCriDmg, endCri, dur, amount, debuffType);
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
            amount *= 2f;//�� �Ӽ� 2�� ����
        }
        else if (lv == 3)
        {
            dur = 5; //�� ���ӽð� 5�ʷ� ����
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 5f;
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

