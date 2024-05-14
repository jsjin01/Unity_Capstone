using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : WeaponComponent
{
    public static Dagger i;
    [SerializeField] GameObject[] weapon;
    int index = 0; //���� ��� 
    int maxidx = 1;//�ʿ� ���


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //�⺻�� ����
        weaponmulatk = 1.2f;
        weaponmulatkspd = 1f;
    }

    public override void Attack()
    {
        if (!canAttack)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject dagger = Instantiate(weapon[index], GamePlayerMoveControl.i.playerPos, rotation, transform);
        dagger.GetComponentInChildren<DaggerVFX>().SetAttack(dmg, endCriDmg, endCri, lv);
        if(lv  == 5) //�ʿ� ����
        {
            GameObject daggerParticle  = Instantiate(weapon[maxidx],GamePlayerMoveControl.i.playerPos, Quaternion.Euler(0,0,0), transform);
            daggerParticle.GetComponentInChildren<DaggerLvMaxVFX>().SetAttack(dmg, endCriDmg, endCri);
        }
        StartCoroutine(AttackRate());
    }

    public override void WeaponLevelUp()
    {
        lv++; //DaggerVFX���� ����
    }
}
