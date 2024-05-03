using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword: WeaponComponent
{
    public static LongSword i;
    int lv = 1;
    //1->2 /2->3 /3->4 /4->5 / 5 -> 초월
    [SerializeField]GameObject[] weapon;

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        //기본값 적용
        weaponmulatk = 1.2f;
        weaponmulatkspd = 1.2f;
    }

    public override void Attack()
    {
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        GameObject longsward = Instantiate(weapon[lv -1 ],GamePlayerMoveControl.i.playerPos , rotation , transform);
        longsward.GetComponentInChildren<CloseRangeWeaponVFX>().SetAttack(dmg);
    }

    public override void WeaponLevelUp()
    {
        throw new System.NotImplementedException();
    }
}
