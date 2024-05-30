using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : CharacterComponent
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
        GamePlayerManager.i.MaxHp += 20; //레벨업시에 최대 체력 증가
        GamePlayerManager.i.hp += 20;  //최대 체력 증가한 만큼 증가
    }

}
