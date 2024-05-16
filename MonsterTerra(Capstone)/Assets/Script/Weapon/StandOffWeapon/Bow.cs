using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponComponent
{
    public static Bow i;
    [SerializeField] GameObject bullet; // ����ü
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
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
        arrow.GetComponent<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        if (lvMax)
        {
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, angle + 30);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, angle - 30);
            GameObject arrow1 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, leftRotation, transform);
            GameObject arrow2 = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rightRotation, transform);
            arrow1.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            arrow1.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
            arrow2.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
            arrow2.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        }
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
