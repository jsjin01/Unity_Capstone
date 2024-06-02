using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brunch : WeaponComponent
{
    public static Brunch i;
    [SerializeField] GameObject particle; //버프 파티클

    float dur = 3f;
    bool max = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 2f;
        weaponmulatkspd = 5.0f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        SoundManger.i.PlaySound(10);
        if (endAtkSpd < dur) //최종 공격속도가 버프지속시간보다 짧을 수 없음
        {
            endAtkSpd = dur;
        }
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

        //ICE Bullet 생성
        GameObject iceBullet = Instantiate(particle, GamePlayerMoveControl.i.playerPos, rotation, transform);
        StartCoroutine(GamePlayerManager.i.Brunch(dmg, dur, max));

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk += 1f; //회복량 계수 증가
        }
        else if (lv == 2)
        {
            weaponmulatkspd -= 2f; //공속 계수 증가
        }
        else if (lv == 3)
        {
            max = true;
        }
        else
        {
            Debug.Log("더이상 강화할 수 없습니다.");
        }

        if (lv == 4)
        {
            lv--;
        }
    }
}
