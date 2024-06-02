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

        SoundManger.i.PlaySound(6);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        //화살 생성
        GameObject arrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        arrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        arrow.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //플레이어 이동 방향으로 발사
        if (lvMax)
        {
            //추가 화살 각도 및 벡터 계산
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, angle+ 30);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, angle - 30);
            Vector2 leftDir = RotateVector2(Vector2.right, angle + 30);
            Vector2 rightDir = RotateVector2(Vector2.right, angle - 30);

            //왼쪽 화살 && 오른쪽 화살 생성
            GameObject leftArrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, leftRotation, transform);
            GameObject rightArrow = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rightRotation, transform);
            leftArrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            leftArrow.GetComponentInChildren<BulletComponent>().Move(leftDir); 
            rightArrow.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            rightArrow.GetComponentInChildren<BulletComponent>().Move(rightDir); 
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
