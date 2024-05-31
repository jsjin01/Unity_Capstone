using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : WeaponComponent
{
    public static FireMagic i;
    [SerializeField] GameObject Circle; // 회전
    bool max =false;
    public int idxlv = 0;



    float dur = 3f;
    float amount = 5f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 1f;
        weaponmulatkspd = 15.0f;
        debuffType = 3;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //ICE Bullet 생성
        GameObject fireCircle = Instantiate(Circle , GamePlayerMoveControl.i.playerPos, rotation, transform);
        fireCircle.GetComponentInChildren<FireBallCircle>().SetAttack(dmg, endCriDmg, endCri, max, dur, amount, debuffType);
        

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk *= 2.0f; //데미지 2배 증가
        }
        else if (lv == 2)
        {
            amount *= 2f;//화상데미지 2배 증가
        }
        else if (lv == 3)
        {
            dur = 5; //둔화 시간 5초로 증가
        }
        else if (lv == 4)
        {
            idxlv++;//폭팔 범위 증가
        }
        else if (lv == 5)
        {
            max = true;
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
