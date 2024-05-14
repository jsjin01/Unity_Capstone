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
    protected bool canAttack = true;             //���� ����
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
        //�������ݷ� , �������ݼӵ� , ũ��Ƽ�� ������ �� Ȯ�� ���
        dmg = weaponmulatk * GamePlayerManager.i.atk;
        endAtkSpd = weaponmulatkspd * GamePlayerManager.i.atkSpd;
        endCriDmg = GamePlayerManager.i.criDmg + addCridmg;
        endCri = GamePlayerManager.i.cri + addCri;
    }
    abstract public void Attack();        //���� �����ϴ� �κ�

    abstract public void WeaponLevelUp(); // ���� ��ȭ�ϴ� �ý���




}
