using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : WeaponComponent
{
    public static CrossBow i;
    [SerializeField] GameObject bullet; // 투사체
    bool lvMax = false;
    float dur = 0f;
    float amount = 0f;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 0.5f;
        weaponmulatkspd = 3.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        SoundManger.i.PlaySound(6);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //CrossBow Arrow 생성
        GameObject cbArrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        cbArrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri, dur, amount, debuffType);
        cbArrow.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        if(lv >= 2)
        {
            GameObject cbArrow1 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
            cbArrow1.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri, dur, amount, 6);
            cbArrow1.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        }
        if(lv >= 4)
        {
            GameObject cbArrow2 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
            cbArrow2.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri, dur, amount, 5);
            cbArrow2.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        }
        if (lvMax)
        {
            cbArrow.GetComponentInChildren<BulletComponent>().SetMax();                           //총알 Max 레벨
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            debuffType = 7;
        }
        else if (lv == 2)
        {
           //화살 추가
        }
        else if (lv == 3)
        {
            weaponmulatkspd -= 0.5f; // 공격 속도 계수 0.5 감소
        }
        else if (lv == 4)
        {
           //화살 추가
        }
        else if (lv == 5)
        {
            dur = 3000f;//지속 무한히 지속
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
