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
        //기본값 적용
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
        //모든 무기의 공격 루틴 무시 
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
            //모든 무기 레벨업, 공속 증가
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 2)
        {
            //모든 무기 레벨업, 공속 증가
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 3)
        {
            //모든 무기 레벨업, 공속 증가
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 4)
        {
            //모든 무기 레벨업, 공속 증가
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else if (lv == 5)
        {
            //모든 무기 초월
            IceMagic.i.WeaponLevelUp();
            FireMagic.i.WeaponLevelUp();
            PoisonMagic.i.WeaponLevelUp();
            ThunderMagic.i.WeaponLevelUp();
            weaponmulatkspd -= 1f;
        }
        else
        {
            Debug.Log("더이상 강화할 수 없습니다.");
        }

        if (lv == 6)
        {
            lv--;
        }
    }
}
