using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : WeaponComponent
{
    public static Sickle i;
    [SerializeField] GameObject[] weapon;
    int index = 0; //공격 모션
    int endindex = 2;
    float dur = 0f;
    float amount = 0f;

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
            debuffType = 7; // 출혈효과 1초간 
            dur = 1f;
            amount = 0.1f;
        }
        else if (lv == 2)
        {
            index++;       //무기 길이 증가
        }
        else if (lv == 3)
        {
            weaponmulatk += 0.1f; //10% 만큼 데미지 증가
        }
        else if (lv == 4)
        {
            dur = 300f;
            amount = 0.1f;
        }
        else if (lv == 5)
        {
            Debug.Log("초월");
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
