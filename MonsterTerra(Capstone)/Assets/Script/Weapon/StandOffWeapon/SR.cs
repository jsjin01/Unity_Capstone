using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR : WeaponComponent
{
    public static SR i;
    [SerializeField] GameObject bullet; // ����ü
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
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

        //ȭ�� ����
        GameObject srBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        srBullet.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        srBullet.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatk += 0.2f; //���� �߰� ���ݷ� 20% ����
        }
        else if (lv == 2)
        {
            addCridmg += 0.30f; // ġ��Ÿ ������ 30% ����
        }
        else if (lv == 3)
        {
            addCri += 0.1f; // ġ��Ÿ Ȯ�� 10% ����
        }
        else if (lv == 4)
        {
            weaponmulatk += 0.3f; //���� �߰� ���ݷ� 30% ����
        }
        else if (lv == 5)
        {
            lvMax = true;
        }
        else
        {
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
        }

        if (lv == 6)
        {
            lv--;
        }
    }
}
