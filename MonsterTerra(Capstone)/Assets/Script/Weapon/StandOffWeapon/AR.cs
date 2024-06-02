using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : WeaponComponent
{
    public static AR i;
    [SerializeField] GameObject bullet; // 투사체
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 0.5f;
        weaponmulatkspd = 0.7f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        SoundManger.i.PlaySound(7);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //SR bullet 생성
        GameObject srBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        srBullet.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        srBullet.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        if (lvMax)
        {
            srBullet.GetComponentInChildren<BulletComponent>().SetMax();                           //총알 Max 레벨
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatkspd -= 0.1f; //공격 속도 10퍼 증가
        }
        else if (lv == 2)
        {
            debuffType = 2; //넉백 추가
        }
        else if (lv == 3)
        {
            weaponmulatkspd -= 0.2f; //공격 속도 20퍼 증가
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 0.3f; //공격 속도 30퍼 증가
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
