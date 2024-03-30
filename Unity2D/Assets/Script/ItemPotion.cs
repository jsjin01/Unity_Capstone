using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotion : ItemComponent
{
    [SerializeField] int hpVal = 15;

    protected override void GetItem()
    {
        PlayerComponent.instance.RecoveryHp(hpVal);
    }

    
}
