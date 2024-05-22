using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR : WeaponComponent
{
    public static SR i;
    [SerializeField] GameObject bullet; // 투사체
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 2.0f;
        weaponmulatkspd = 3.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //화살 생성
        GameObject srBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        srBullet.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        srBullet.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk += 0.2f; //무기 추가 공격력 20% 증가
        }
        else if (lv == 2)
        {
            addCridmg += 0.30f; // 치명타 데미지 30% 증가
        }
        else if (lv == 3)
        {
            addCri += 0.1f; // 치명타 확률 10% 증가
        }
        else if (lv == 4)
        {
            weaponmulatk += 0.3f; //무기 추가 공격력 30% 증가
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
