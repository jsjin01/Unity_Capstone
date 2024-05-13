using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponComponent
{
    public static Axe i;
    [SerializeField] GameObject[] weapon;
    int index = 1; //���� ��� 

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1.2f;
        weaponmulatkspd = 3.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject axe = Instantiate(weapon[index], GamePlayerMoveControl.i.playerPos, rotation, transform);
        axe.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg,endCriDmg,endCri);
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk += 0.2f;  //�߰� ���ݷ� ��� 20% ����
        }
        else if (lv == 2)
        {
            addCridmg += 0.15f;    //ũ��Ƽ�� ������ 15% ����
        }
        else if (lv == 3)
        {
            debuffTpye = 8;        //�Ŀ�ȿ���� ���ϴ� �κ�
        }
        else if (lv == 4)
        {
            weaponmulatk *= 1.3f;  //�߰� ���ݷ��� 30%��ŭ ��ȭ
        }
        else if (lv == 5)
        {
            index++;               //�ʿ� ���� ��ȯ
        }
        else
        {
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
        }
    }
}
