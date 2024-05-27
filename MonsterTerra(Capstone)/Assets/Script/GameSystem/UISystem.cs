using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    public InfoType type;
    public Text nameText;
    public Image characterImage; // 이미지를 표시할 Image 컴포넌트
    public Sprite[] characterSprites; // 캐릭터 이미지들을 저장할 배열
    int character = 0;
    int[] weapon = { 0, 0, 0, 0 };

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
        // 시작할 때 첫 번째 캐릭터 이미지를 표시
        ShowCharacterImage();
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
        character += dir;
        ShowCharacterImage();
    }

    void ShowCharacterImage()
    {
        if (character >= characterSprites.Length)
            character = 0;
        else if (character < 0)
            character = characterSprites.Length - 1;

        // nameText 변수를 사용하는 부분
        if (nameText != null)
        {
            characterImage.sprite = characterSprites[character];
            nameText.text = characterSprites[character].name;
        }
    }
}
