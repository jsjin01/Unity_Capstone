using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMagic : WeaponComponent
{
    public static RandomMagic i;

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1f;
        weaponmulatkspd = 15.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        if(lv < 5)
        {
            int randomatk = Random.Range(0, 4);
            if (randomatk == 0)
            {
                IceMagic.i.Attack();
            }
            else if (randomatk == 1)
            {
                FireMagic.i.Attack();
            }
            else if (randomatk == 2)
            {
                PoisonMagic.i.Attack();
            }
            else if (randomatk == 3)
            {
                ThunderMagic.i.Attack();
            }
        }
        else
        {
            IceMagic.i.Attack();
            FireMagic.i.Attack();
            PoisonMagic.i.Attack();
            ThunderMagic.i.Attack();
        }
        //��� ������ ���� ��ƾ ���� 
        IceMagic.i.StopAllCoroutines();
        FireMagic.i.StopAllCoroutines();
        PoisonMagic.i.StopAllCoroutines();
        ThunderMagic.i.StopAllCoroutines();
        IceMagic.i.canAttack = true;
        FireMagic.i.canAttack = true;
        PoisonMagic.i.canAttack = true;
        ThunderMagic.i.canAttack= true;
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            //��� ���� ������, ���� ����
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 2)
        {
            //��� ���� ������, ���� ����
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 3)
        {
            //��� ���� ������, ���� ����
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 4)
        {
            //��� ���� ������, ���� ����
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 5)
        {
            //��� ���� �ʿ�
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
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
