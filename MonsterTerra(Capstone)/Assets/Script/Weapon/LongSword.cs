using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword: WeaponComponent
{
    public static LongSword i;
    [SerializeField]GameObject[] weapon;
    int index = 0; //공격 모션 

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
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
            index++;               //공격범위가 1.5배 증가
        }
        else if(lv == 2)
        {
            addCridmg += 0.1f;     //크리티컬 데미지 10% 증가
        }
        else if(lv == 3)
        {
            weaponmulatk += 0.1f;  //추가 공격력 계수 10% 증가
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 0.1f;//추가 공격 속도 계수 10% 감소
        }
        else if(lv == 5)
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
