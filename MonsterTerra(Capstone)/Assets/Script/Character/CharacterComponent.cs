using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CHARACTERTYPE //캐릭터 종류
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
    //캐릭터 타입
    [SerializeField] protected CHARACTERTYPE ctype;

    //기본 패시브 무기 데미지 계수 
    protected float CRdmg = 1;
    protected float SOdmg = 1;
    protected float MWdmg = 1;
    protected float SWdmg = 1;

    public abstract void LevelUp();//고유 패시브에서 레벨 업 시에 적용되는 부분
    public abstract void CharaterSkill(); //고유 스킬
}
