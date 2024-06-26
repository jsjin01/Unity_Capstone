using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponComponent
{
    public static Axe i;
    [SerializeField] GameObject[] weapon;
    int index = 0; //공격 모션 
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
        SoundManger.i.PlaySound(5);
        GamePlayerMoveControl.i.anit.SetTrigger("CloseRange");
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject axe = Instantiate(weapon[index], GamePlayerMoveControl.i.playerPos, rotation, transform);
        axe.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg,endCriDmg,endCri, dur, amount, debuffType);
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk += 0.2f;  //추가 공격력 계수 20% 증가
        }
        else if (lv == 2)
        {
            addCridmg += 0.15f;    //크리티컬 데미지 15% 증가
        }
        else if (lv == 3)
        {
            debuffType = 6;        //파열효과를 지니는 부분
            dur = 3f;
            amount = 0.3f;
        }
        else if (lv == 4)
        {
            weaponmulatk *= 1.3f;  //추가 공격력의 30%만큼 강화
        }
        else if (lv == 5)
        {
            index++;               //초월 공격 전환
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
