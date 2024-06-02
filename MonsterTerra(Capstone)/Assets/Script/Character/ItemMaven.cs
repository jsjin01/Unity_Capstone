using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaven : CharacterComponent
{
    private void OnEnable()
    {
        GamePlayerManager.i.Cbuff = 1.5f;
        Debug.Log("캐릭터가 할당되었습니다.");
    }
    public override void CharaterSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelUp()
    {
        Debug.Log("패시브 작동 중");
        GamePlayerManager.i.speed *= 1.05f; //레벨업시에 스피드 증가
    }

}