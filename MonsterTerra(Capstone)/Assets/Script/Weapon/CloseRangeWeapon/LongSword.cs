using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword: WeaponComponent
{
    public static LongSword i;
    [SerializeField]GameObject[] weapon;
    int index = 0; //���� ��� 

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1.2f;
        weaponmulatkspd = 1.2f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject longsward = Instantiate(weapon[index],GamePlayerMoveControl.i.playerPos , rotation , transform);
        longsward.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri);
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if(lv == 1)
        {
            index++;               //���ݹ����� 1.5�� ����
        }
        else if(lv == 2)
        {
            addCridmg += 0.1f;     //ũ��Ƽ�� ������ 10% ����
        }
        else if(lv == 3)
        {
            weaponmulatk += 0.1f;  //�߰� ���ݷ� ��� 10% ����
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 0.1f;//�߰� ���� �ӵ� ��� 10% ����
        }
        else if(lv == 5)
        {
            index++;               //�ʿ� ���� ��ȯ
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
