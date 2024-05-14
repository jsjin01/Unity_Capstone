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
    protected bool canAttack = true;             //공격 여부
    protected float weaponmulatk = 0;            //무기 공격력 계수
    protected float weaponmulatkspd = 0;         //무기 공격 속도 계수
    protected float addCridmg = 0;               //추가 크리티컬 데미지 
    protected float addCri = 0;                  //추가 크리티컬 확률
    protected int debuffType = 0;                //효과 부여 => default로 0값 부여
    protected int lv = 0;                        //무기 레벨
    //1->2 /2->3 /3->4 /4->5 / 5 -> 초월

    protected float dmg;                     //최종 데미지
    protected float endAtkSpd;               //최종 공격속도
    protected float endCriDmg;               //최종 크리티컬 데미지
    protected float endCri;                  //최종 크리티컬 확률


    protected float angle;                   //플레이어가 보고 있는 방향 계산

    private void Start()
    {
    }  

    protected IEnumerator AttackRate()
    {
        canAttack = false;
        yield return new WaitForSeconds(endAtkSpd);
        canAttack = true;
    }
    private void Update()
    {
        angle = Mathf.Atan2(GamePlayerMoveControl.i.playerDir.y, GamePlayerMoveControl.i.playerDir.x) * Mathf.Rad2Deg;
        //최종공격력 , 최종공격속도 , 크리티컬 데미지 및 확률 계산
        dmg = weaponmulatk * GamePlayerManager.i.atk;
        endAtkSpd = weaponmulatkspd * GamePlayerManager.i.atkSpd;
        endCriDmg = GamePlayerManager.i.criDmg + addCridmg;
        endCri = GamePlayerManager.i.cri + addCri;
    }
    abstract public void Attack();        //무기 공격하는 부분

    abstract public void WeaponLevelUp(); // 무기 강화하는 시스템




}
