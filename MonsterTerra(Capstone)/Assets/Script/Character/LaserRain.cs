using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRain : CharacterComponent
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
        GamePlayerManager.i.atk += 1; //레벨업시에 데미지 1증가
    }

}
