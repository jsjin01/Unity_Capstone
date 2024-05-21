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
            weaponmulatkspd -= 0.2f;// ���ݼӵ� 20% ����
        }
        else if (lv == 2)
        {
            weaponmulatk += 0.1f; //���� �߰� ���ݷ� 10% ����
        }
        else if (lv == 3)
        {
            addCridmg += 0.15f; // ġ��Ÿ ������ 15% ����
        }
        else if (lv == 4)
        {
            addCri += 0.1f; //ġ��Ÿ Ȯ�� 10�� ����
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
