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
    protected bool canAttack = true;         //���� ����
    protected float weaponmulatk;            //���� ���ݷ� ���
    protected float weaponmulatkspd;         //���� ���� �ӵ� ���

    protected float dmg;                     //���� ������
    protected float endAtkSpd;               //���� ���ݼӵ�

    protected float angle;

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
            //�������ݷ� , �������ݼӵ� ���
            angle = Mathf.Atan2(GamePlayerMoveControl.i.playerDir.y, GamePlayerMoveControl.i.playerDir.x) * Mathf.Rad2Deg;
        dmg = weaponmulatk * GamePlayerManager.i.atk;
        endAtkSpd = weaponmulatkspd * GamePlayerManager.i.atkSpd;
    }
    abstract public void Attack();        //���� �����ϴ� �κ�

    abstract public void WeaponLevelUp(); // ���� ��ȭ�ϴ� �ý���




}
