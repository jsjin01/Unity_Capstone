using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : CharacterComponent
{
    private void OnEnable()
    {
        //모든 능력치 다운
        GamePlayerManager.i.atk = (int)((float)GamePlayerManager.i.atk * 0.7f);
        GamePlayerManager.i.atkSpd *= 1.3f;
        GamePlayerManager.i.speed *= 0.7f;
        GamePlayerManager.i.MaxHp *= 0.7f;
        GamePlayerManager.i.hp = GamePlayerManager.i.MaxHp;

        Debug.Log("캐릭터가 할당되었습니다.");
    }
    public override void CharaterSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelUp()
    {
        Debug.Log("패시브 작동 중");
    }

}
