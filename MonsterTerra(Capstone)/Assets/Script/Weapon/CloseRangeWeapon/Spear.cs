using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : WeaponComponent
{
    public static Spear i;
    [SerializeField] GameObject[] weapon;
    int index = 0; //공격 모션 

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
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
            index++;                 //무기 길이 1.5배 증가
        }
        else if (lv == 2)
        {
            weaponmulatkspd -= 0.5f; //무기 공속 계수 50% 증가
        }
        else if (lv == 3)
        {
           index++;                  //무기 길이 2배 증가
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 0.5f; //무기 공속 계수 50% 증가
        }
        else if (lv == 5)
        {
            index++;                 //초월 공격 전환
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
