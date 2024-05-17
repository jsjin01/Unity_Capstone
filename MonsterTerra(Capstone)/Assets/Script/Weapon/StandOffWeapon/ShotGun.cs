using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponComponent
{
    public static ShotGun i;
    [SerializeField] GameObject bullet; // 투사체
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 1.2f;
        weaponmulatkspd = 1.5f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //샷건 이펙트 생성
        GameObject shotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        shotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
        if (lvMax)
        {
            //추가 샷건 이펙트 각도 및 벡터 계산
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, angle + 90);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, angle - 90);
            Quaternion backRotation = Quaternion.Euler(0f, 0f, angle - 180);
            Vector2 leftDir = RotateVector2(Vector2.right, angle + 90);
            Vector2 rightDir = RotateVector2(Vector2.right, angle - 90);
            Vector2 backDir = RotateVector2(Vector2.right, angle - 180);

            //왼쪽 샷건 이펙트 && 오른쪽 샷건 이펙트 && 뒷쪽 샷건 이펙트 생성
            GameObject leftShotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, leftRotation, transform);
            GameObject rightShotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rightRotation, transform);
            GameObject backShotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, backRotation, transform);
            leftShotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
            rightShotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
            backShotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
        }
        StartCoroutine(AttackRate());
    }
    Vector2 RotateVector2(Vector2 dir, float angle) //각도에 따른 playDir 변화 계산
    {
        // 각도를 라디안으로 변환
        float rad = angle * Mathf.Deg2Rad;

        // 회전 변환 적용
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        float newX = dir.x * cos - dir.y * sin;
        float newY = dir.x * sin + dir.y * cos;

        return new Vector2(newX, newY).normalized;
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            debuffType = 2;
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
            weaponmulatk += 0.2f; //무기 추가 공격력 20% 증가
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
