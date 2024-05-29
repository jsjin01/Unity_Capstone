using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagic : WeaponComponent
{
    public static IceMagic i;
    [SerializeField] GameObject bullet; // 투사체
    [SerializeField] GameObject LvMax; // 만렙 스킬
    bool lvMax = false;
    public int idxlv = 0;

    float dur = 3f;
    float amount = 0.2f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 1f;
        weaponmulatkspd = 15.0f;
        debuffType = 4;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //ICE Bullet 생성
        GameObject iceBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        iceBullet.GetComponentInChildren<MagicBullet>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        iceBullet.GetComponentInChildren<MagicBullet>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사

        if (lvMax)
        {
            GameObject iceWorld = Instantiate(LvMax, GamePlayerMoveControl.i.playerPos, Quaternion.Euler(0,0,0), transform);
            iceWorld.transform.GetChild(0).GetComponentInChildren<MagicVFX>().SetAttack(dmg / 10, endCriDmg, endCri, dur, amount, debuffType);
        }

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            idxlv++;//범위 증가
        }
        else if (lv == 2)
        {
            amount *= 2f;//둔화율 2배 증가
        }
        else if (lv == 3)
        {
            dur = 5; //둔화 시간 5초로 증가
        }
        else if (lv == 4)
        {
           weaponmulatkspd  -= 5f;
        }
        else if (lv == 5)
        {
            lvMax = true;
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
