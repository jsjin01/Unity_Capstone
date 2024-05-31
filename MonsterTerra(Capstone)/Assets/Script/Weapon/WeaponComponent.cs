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
} //���� �����
public enum SOTYPE
{
    BOW,
    SHOTGUN,
    AR,
    SR,
    CROSSBOW
} //���Ÿ� �����
public enum MWTYPE
{
    ICE,
    FIRE,
    THUNDER,
    POISON,
    RANDOM
} //���� �����
public enum SPTYPE
{
    SHIELD,
    BRUNCH,
    MAGICTOOL,
    COFFEE,
    FEATHER
} //���� �����
public abstract class WeaponComponent : MonoBehaviour
{
    public bool canAttack = true;             //���� ����
    [SerializeField]protected WEAPONTYPE weapontype;                   //���� ����
    protected float weaponmulatk = 0;            //���� ���ݷ� ���
    protected float weaponmulatkspd = 0;         //���� ���� �ӵ� ���
    protected float addCridmg = 0;               //�߰� ũ��Ƽ�� ������ 
    protected float addCri = 0;                  //�߰� ũ��Ƽ�� Ȯ��
    protected int debuffType = 0;                //ȿ�� �ο� => default�� 0�� �ο�
    protected int lv = 0;                        //���� ����
    //1->2 /2->3 /3->4 /4->5 / 5 -> �ʿ�

    protected float dmg;                     //���� ������
    protected float endAtkSpd;               //���� ���ݼӵ�
    protected float endCriDmg;               //���� ũ��Ƽ�� ������
    protected float endCri;                  //���� ũ��Ƽ�� Ȯ��


    //ĳ���� Ư���������� ������ ���
    protected float Ccr = 1f;
    protected float Cso = 1f;
    protected float Cmw= 1f;
    protected float Csp = 1f;
    protected float endC;

    protected float angle;                   //�÷��̾ ���� �ִ� ���� ���

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

        //���⿡ ���� ������ ����
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

        //�������ݷ� , �������ݼӵ� , ũ��Ƽ�� ������ �� Ȯ�� ���
        dmg = weaponmulatk * GamePlayerManager.i.atk;
        endAtkSpd = weaponmulatkspd * GamePlayerManager.i.atkSpd;
        endCriDmg = GamePlayerManager.i.criDmg + addCridmg;
        endCri = GamePlayerManager.i.cri + addCri;
    }
    abstract public void Attack();        //���� �����ϴ� �κ�

    abstract public void WeaponLevelUp(); // ���� ��ȭ�ϴ� �ý���

    public void SetCdamage(CharacterComponent cc)
    {
        Ccr = cc.CRdmg;
        Cso = cc.SOdmg;
        Cmw = cc.MWdmg;
        Csp = cc.SWdmg;
    }



}
