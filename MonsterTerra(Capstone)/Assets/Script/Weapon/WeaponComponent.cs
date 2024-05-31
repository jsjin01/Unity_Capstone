using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPONTYPE
{
    CLOSERANGE,
    STANDOFF,
    MAGICWEAPON,
    SUPPORTWEAPON
}
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
    public bool canAttack = true;             //공격 여부
    [SerializeField]protected WEAPONTYPE weapontype;                   //무기 유형
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


    //캐릭터 특성으로인한 데미지 계산
    protected float Ccr = 1f;
    protected float Cso = 1f;
    protected float Cmw= 1f;
    protected float Csp = 1f;
    protected float endC;

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

        //무기에 따른 데미지 구별
        if (weapontype == WEAPONTYPE.CLOSERANGE)
        {
            endC = GamePlayerManager.i.atk * Ccr;
        }
        else if (weapontype == WEAPONTYPE.STANDOFF)
        {
            endC = GamePlayerManager.i.atk * Cso;
        }
        else if(weapontype == WEAPONTYPE.MAGICWEAPON)
        {
            endC = GamePlayerManager.i.atk * Cmw;
        }
        else if(weapontype ==WEAPONTYPE.SUPPORTWEAPON)
        {
            endC = GamePlayerManager.i.atk * Csp;
        }

        //최종공격력 , 최종공격속도 , 크리티컬 데미지 및 확률 계산
        dmg = weaponmulatk * GamePlayerManager.i.atk;
        endAtkSpd = weaponmulatkspd * GamePlayerManager.i.atkSpd;
        endCriDmg = GamePlayerManager.i.criDmg + addCridmg;
        endCri = GamePlayerManager.i.cri + addCri;
    }
    abstract public void Attack();        //무기 공격하는 부분

    abstract public void WeaponLevelUp(); // 무기 강화하는 시스템

    public void SetCdamage(CharacterComponent cc)
    {
        Ccr = cc.CRdmg;
        Cso = cc.SOdmg;
        Cmw = cc.MWdmg;
        Csp = cc.SWdmg;
    }



}
