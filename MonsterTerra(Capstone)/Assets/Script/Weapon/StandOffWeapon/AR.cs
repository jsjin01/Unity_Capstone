using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : WeaponComponent
{
    public static AR i;
    [SerializeField] GameObject bullet; // ����ü
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
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

        //SR bullet ����
        GameObject srBullet = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        srBullet.GetComponentInChildren<BulletComponent>().SetAttack(dmg, endCriDmg, endCri);
        srBullet.GetComponentInChildren<BulletComponent>().Move(GamePlayerMoveControl.i.playerDir); //�÷��̾� �̵� �������� �߻�
        if (lvMax)
        {
            srBullet.GetComponentInChildren<BulletComponent>().SetMax();                           //�Ѿ� Max ����
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++;
        if (lv == 1)
        {
            weaponmulatkspd -= 0.1f; //���� �ӵ� 10�� ����
        }
        else if (lv == 2)
        {
            debuffType = 2; //�˹� �߰�
        }
        else if (lv == 3)
        {
            weaponmulatkspd -= 0.2f; //���� �ӵ� 20�� ����
        }
        else if (lv == 4)
        {
            weaponmulatkspd -= 0.3f; //���� �ӵ� 30�� ����
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
