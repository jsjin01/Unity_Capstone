using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARACTERTYPE //ĳ���� ����
{
    BLOCKMAKER,
    TITAN,
    LASERAIN,
    PROGRESS,
    CHAOSCRAD,
    ITEMMAVEN,
    RUNEMAGICIAN,
    BLOODLUST
}
public abstract class CharacterComponent : MonoBehaviour
{
    //ĳ���� Ÿ��
    [SerializeField] protected CHARACTERTYPE ctype;

    //�⺻ �нú� ���� ������ ��� 
    protected float CRdmg = 1;
    protected float SOdmg = 1;
    protected float MWdmg = 1;
    protected float SWdmg = 1;

    public abstract void LevelUp();//���� �нú꿡�� ���� �� �ÿ� ����Ǵ� �κ�
    public abstract void CharaterSkill(); //���� ��ų
}
