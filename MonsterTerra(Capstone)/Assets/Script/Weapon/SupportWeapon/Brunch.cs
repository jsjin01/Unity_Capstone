using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brunch : WeaponComponent
{
    public static Brunch i;
    [SerializeField] GameObject particle; //���� ��ƼŬ

    float dur = 3f;
    bool max = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
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
        if (endAtkSpd < dur) //���� ���ݼӵ��� �������ӽð����� ª�� �� ����
        {
            endAtkSpd = dur;
        }
        Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

        //ICE Bullet ����
        GameObject iceBullet = Instantiate(particle, GamePlayerMoveControl.i.playerPos, rotation, transform);
        StartCoroutine(GamePlayerManager.i.Brunch(dmg, dur, max));

        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk += 1f; //ȸ���� ��� ����
        }
        else if (lv == 2)
        {
            weaponmulatkspd -= 2f; //���� ��� ����
        }
        else if (lv == 3)
        {
            max = true;
        }
        else
        {
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
        }

        if (lv == 4)
        {
            lv--;
        }
    }
}
