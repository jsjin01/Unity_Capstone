using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : WeaponComponent
{
    public static Spear i;
    [SerializeField] GameObject[] weapon;
    int index = 0; //���� ��� 

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
        SoundManger.i.PlaySound(4);
        GamePlayerMoveControl.i.anit.SetTrigger("CloseRange");
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject spear = Instantiate(weapon[index], GamePlayerMoveControl.i.playerPos, rotation, transform);
        spear.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri);
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            index++;                 //���� ���� 1.5�� ����
        }
        else if (lv == 2)
        {
            weaponmulatkspd -= 0.5f; //���� ���� ��� 50% ����
        }
        else if (lv == 3)
        {
           index++;                  //���� ���� 2�� ����
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 0.5f; //���� ���� ��� 50% ����
        }
        else if (lv == 5)
        {
            index++;                 //�ʿ� ���� ��ȯ
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
