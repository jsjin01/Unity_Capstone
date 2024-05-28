using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : WeaponComponent
{
    public static Sickle i;
    [SerializeField] GameObject[] weapon;
    int index = 0; //���� ���
    int endindex = 2;
    float dur = 0f;
    float amount = 0f;

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
        GamePlayerMoveControl.i.anit.SetTrigger("CloseRange");
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject sickle = Instantiate(weapon[index], GamePlayerMoveControl.i.playerPos, rotation, transform);
        sickle.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
        if(lv == 5)
        {
            GameObject sickleVFX = Instantiate(weapon[endindex], GamePlayerMoveControl.i.playerPos, Quaternion.Euler(0,0,0), transform);
            sickleVFX.GetComponentInChildren<SickleLvMaxVFX>().SetAttack(0, 0, 0);
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            debuffType = 7; // ����ȿ�� 1�ʰ� 
            dur = 1f;
            amount = 0.1f;
        }
        else if (lv == 2)
        {
            index++;       //���� ���� ����
        }
        else if (lv == 3)
        {
            weaponmulatk += 0.1f; //10% ��ŭ ������ ����
        }
        else if (lv == 4)
        {
            dur = 300f;
            amount = 0.1f;
        }
        else if (lv == 5)
        {
            Debug.Log("�ʿ�");
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
