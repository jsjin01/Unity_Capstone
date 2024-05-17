using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponComponent
{
    public static ShotGun i;
    [SerializeField] GameObject bullet; // ����ü
    bool lvMax = false;


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
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

        //���� ����Ʈ ����
        GameObject shotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rotation, transform);
        shotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
        if (lvMax)
        {
            //�߰� ���� ����Ʈ ���� �� ���� ���
            Quaternion leftRotation = Quaternion.Euler(0f, 0f, angle + 90);
            Quaternion rightRotation = Quaternion.Euler(0f, 0f, angle - 90);
            Quaternion backRotation = Quaternion.Euler(0f, 0f, angle - 180);
            Vector2 leftDir = RotateVector2(Vector2.right, angle + 90);
            Vector2 rightDir = RotateVector2(Vector2.right, angle - 90);
            Vector2 backDir = RotateVector2(Vector2.right, angle - 180);

            //���� ���� ����Ʈ && ������ ���� ����Ʈ && ���� ���� ����Ʈ ����
            GameObject leftShotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, leftRotation, transform);
            GameObject rightShotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, rightRotation, transform);
            GameObject backShotgunB = Instantiate(bullet, GamePlayerMoveControl.i.playerPos, backRotation, transform);
            leftShotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
            rightShotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
            backShotgunB.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg, endCriDmg, endCri, debuffType);
        }
        StartCoroutine(AttackRate());
    }
    Vector2 RotateVector2(Vector2 dir, float angle) //������ ���� playDir ��ȭ ���
    {
        // ������ �������� ��ȯ
        float rad = angle * Mathf.Deg2Rad;

        // ȸ�� ��ȯ ����
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
            weaponmulatk += 0.1f; //���� �߰� ���ݷ� 10% ����
        }
        else if (lv == 3)
        {
            addCridmg += 0.15f; // ġ��Ÿ ������ 15% ����
        }
        else if (lv == 4)
        {
            weaponmulatk += 0.2f; //���� �߰� ���ݷ� 20% ����
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
