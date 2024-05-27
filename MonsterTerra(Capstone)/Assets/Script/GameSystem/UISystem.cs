using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public InfoType type;
    public Image characterImage; // 이미지를 표시할 Image 컴포넌트
    public Sprite[] characterSprites; // 캐릭터 이미지들을 저장할 배열
    int[] character;

    Text myText;
    Slider mySlider;
    Spawner spawner;
    WeaponManager weaponManager;


    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Hp,
        Mp,
        Stage,
        Weapon,
        Weapon_Text,
        Character,
        Character_CR,
        Character_SO,
        Character_MG,
        Character_SP,
        None
    }

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
        spawner = FindObjectOfType<Spawner>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Start()
    {
        character = new int[5];
        // 시작할 때 첫 번째 캐릭터 이미지를 표시
        ShowCharacterImage(0);
    }

    void LateUpdate()
    {
        switch (type)
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
            case InfoType.Weapon:
                //int i = weaponManager.idx;
                break;
        }
    }

    public void ChangeCharacter(int dir)
    {
         character[0] += dir;
         ShowCharacterImage(0);
    }

    void ShowCharacterImage(int type)
    {
        switch (type)
        {
            case 0:
                if (character[type] >= characterSprites.Length)
                    character[type] = 0;
                else if(character[type] <= 0)
                    character[type] = characterSprites.Length - 1;

                characterImage.sprite = characterSprites[character[type]];
                break;
            default:
                break;
        }
    }
}
