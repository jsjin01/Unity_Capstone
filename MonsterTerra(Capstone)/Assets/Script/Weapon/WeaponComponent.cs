using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CRTYPE
{
    LONGSWORD,
    AXE,
    SPEAR,
    DAGGER,
    SICKLE
} //근접 무기류
public enum SOTYPE
{
    BOW,
    SHOTGUN,
    AR,
    SR,
    CROSSBOW
} //원거리 무기류
public enum MWTYPE
{
    ICE,
    FIRE,
    THUNDER,
    POISON,
    RANDOM
} //마법 무기류
public enum SPTYPE
{
    SHIELD,
    BRUNCH,
    MAGICTOOL,
    COFFEE,
    FEATHER
} //지원 무기류
public abstract class WeaponComponent : MonoBehaviour
{
    protected float weaponmulatk;            //무기 공격력 계수
    protected float weaponmulatkspd;         //무기 공격 속도 계수

    protected float dmg;                     //최종 데미지
    protected float endAtkSpd;               //최종 공격속도

    protected float angle;

    private void Start()
    {
    }  
    private void Update()
    {
            //최종공격력 , 최종공격속도 계산
            angle = Mathf.Atan2(GamePlayerMoveControl.i.playerDir.y, GamePlayerMoveControl.i.playerDir.x) * Mathf.Rad2Deg;
        dmg = weaponmulatk * GamePlayerManager.i.atk;
        endAtkSpd = weaponmulatkspd * GamePlayerManager.i.atkSpd;
    }
    abstract public void Attack();        //무기 공격하는 부분

    abstract public void WeaponLevelUp(); // 무기 강화하는 시스템




}
