using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : WeaponComponent
{
    public static CrossBow i;
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

        SoundManger.i.PlaySound(6);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //CrossBow Arrow ����
        GameObject cbArrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        cbArrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        cbArrow.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        if(lv >= 2)
        {
            GameObject cbArrow1 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
            cbArrow1.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri, dur, amount, 6);
            cbArrow1.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        }
        if(lv >= 4)
        {
            GameObject cbArrow2 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
            cbArrow2.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri, dur, amount, 5);
            cbArrow2.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        }
        if (lvMax)
        {
            cbArrow.GetComponentInChildren<BulletComponent>().SetMax();                           //�Ѿ� Max ����
        }
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
