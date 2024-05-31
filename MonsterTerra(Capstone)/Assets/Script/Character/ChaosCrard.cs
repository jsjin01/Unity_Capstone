using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosCrard : CharacterComponent
{
    private void OnEnable()
    {
        Debug.Log("캐릭터가 할당되었습니다.");
    }
    public override void CharaterSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelUp()
    {
        Debug.Log("패시브 작동 중");
        int upstate = Random.Range(0, 6);
        int downstate;
        while (true)
        {
            downstate = Random.Range(0, 6);
            if (downstate == upstate)
            {
                downstate = Random.Range(0, 6);
            }
            else
            {
                break;
            }
        }
        RandomState(upstate, true);
        RandomState(downstate, false);
    }

    void RandomState(int i, bool up) //랜덤 스탯 증가 and 감소
    {
        if(i == 0) //MHP
        {
            if (up)
            {
                GamePlayerManager.i.MaxHp += 20;
                GamePlayerManager.i.hp += 20;
            }
            else
            {
                GamePlayerManager.i.MaxHp -= 10;
                GamePlayerManager.i.hp -= 10;
            }
        }
        else if(i ==1) //spd
        {
            if (up)
            {
                GamePlayerManager.i.speed *= 1.1f;
            }
            else
            {
                GamePlayerManager.i.speed *= 0.95f;
            }
        }
        else if(i == 2)//Atk
        {
            if (up)
            {
                GamePlayerManager.i.atk += 2;
            }
            else
            {
                GamePlayerManager.i.atk -= 1;
            }
        }
        else if(i == 3)//AtkSpd
        {
            if (up)
            {
                GamePlayerManager.i.atkSpd *= 1.1f;
            }
            else
            {
                GamePlayerManager.i.atkSpd *= 0.95f;
            }
        }
        else if(i == 4)//Cri
        {
            if (up)
            {
                GamePlayerManager.i.cri += 0.05f;
            }
            else
            {
                GamePlayerManager.i.cri -= 0.02f;
            }
        }
        else if(i == 5)//CriAtk
        {
            if (up)
            {
                GamePlayerManager.i.criDmg += 0.1f;
            }
            else
            {
                GamePlayerManager.i.criDmg -= 0.05f;
            }
        }

    }

}