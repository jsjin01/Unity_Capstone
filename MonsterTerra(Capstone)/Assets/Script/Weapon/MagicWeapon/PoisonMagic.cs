using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonMagic : WeaponComponent
{
    public static PoisonMagic i;
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
        debuffType = 5;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        SoundManger.i.PlaySound(9);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //Posion Bullet 생성
        GameObject PoisonBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        PoisonBullet.GetComponentInChildren<MagicBulletPoison>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        PoisonBullet.GetComponentInChildren<MagicBulletPoison>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사

        if (lvMax)
        {
            GameObject PoisonWorld = Instantiate(LvMax, GamePlayerMoveControl.i.playerPos, Quaternion.Euler(0, 0, 0), transform);
            PoisonWorld.transform.GetChild(0).GetComponentInChildren<MagicVFX>().SetAttack(dmg / 10, endCriDmg, endCri, dur, amount, debuffType);
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
            amount *= 2f;//독 속성 2배 증가
        }
        else if (lv == 3)
        {
            dur = 5; //독 지속시간 5초로 증가
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 5f;
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

