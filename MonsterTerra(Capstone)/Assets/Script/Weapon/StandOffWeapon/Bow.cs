using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponComponent
{
    public static Bow i;
    [SerializeField] GameObject bullet; // 투사체
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 0.8f;
        weaponmulatkspd = 1f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject arrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        arrow.GetComponent<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        arrow.GetComponent<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        if (lvMax)
        {
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, angle + 30);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, angle - 30);
            GameObject arrow1 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, leftRotation, transform);
            GameObject arrow2 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rightRotation, transform);
            arrow1.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            arrow1.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
            arrow2.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            arrow2.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatkspd -= 0.2f;// 공격속도 20% 증가
        }
        else if (lv == 2)
        {
            weaponmulatk += 0.1f; //무기 추가 공격력 10% 증가
        }
        else if (lv == 3)
        {
            addCridmg += 0.15f; // 치명타 데미지 15% 증가
        }
        else if (lv == 4)
        {
            addCri += 0.1f; //치명타 확률 10퍼 증가
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
