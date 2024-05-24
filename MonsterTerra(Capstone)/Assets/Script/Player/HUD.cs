using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public InfoType type;
    Text myText;
    Slider mySlider;
    Spawner spawner;

    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Hp,
        Mp,
        Stage,
    }

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        spawner = FindObjectOfType<Spawner>();
    }

    void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GamePlayerManager.i.exp;
                float maxExp = GamePlayerManager.i.maxExp[GamePlayerManager.i.lv];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Hp:
                float curHp = GamePlayerManager.i.hp;
                float maxHp = GamePlayerManager.i.MaxHp;
                mySlider.value = curHp / maxHp;
                break;
            case InfoType.Mp:
                float curMp = GamePlayerManager.i.mp;
                float maxMp = GamePlayerManager.i.MaxMp;
                mySlider.value = curMp / maxMp;
                break;
            case InfoType.Stage:
                myText.text = string.Format("Stage {0}", spawner.level + 1);
                break;
        }
    }
}
