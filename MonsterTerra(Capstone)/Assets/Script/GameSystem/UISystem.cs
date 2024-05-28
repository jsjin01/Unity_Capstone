using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UISystem : MonoBehaviour
{
    public InfoType type;
    public Text nameText;
    public Text perText;
    public Image characterImage; // 이미지를 표시할 Image 컴포넌트
    public Sprite[] characterSprites; // 캐릭터 이미지들을 저장할 배열
    public GameObject[] imageWeaponCR; // 이미지 게임 오브젝트 배열
    public GameObject[] imageWeaponSO; // 이미지 게임 오브젝트 배열
    public GameObject[] imageWeaponMG; // 이미지 게임 오브젝트 배열
    public GameObject[] imageWeaponSP; // 이미지 게임 오브젝트 배열

    public GameObject explanation; // 설명
    string[] characterNames;
    string[] cperformances;
    string[] weaponNames;
    string[] wperformances;

    int character = 0;
    int weaponCR = 0;
    int weaponSO = 0;
    int weaponMG = 0;
    int weaponSP = 0;

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
        //ShowCharacterImage();
        //ShowWeaponCR();
        //LoadDataToArray(out characterNames, out cperformances);
        //LoadDataToArray2(out weaponNames, out wperformances);
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
        if (characterSprites.Length != 0)
        {
            characterImage.sprite = characterSprites[character];
            nameText.text = characterSprites[character].name;
        }
    }

    public void ChangeWeaponCR(int dir)
    {
        imageWeaponCR[weaponCR].SetActive(false);

        weaponCR += dir;
        ShowWeaponCR();
    }
    public void ChangeWeaponSO(int dir)
    {
        imageWeaponSO[weaponSO].SetActive(false);

        weaponSO += dir;
        ShowWeaponSO();
    }
    public void ChangeWeaponMG(int dir)
    {
        imageWeaponMG[weaponMG].SetActive(false);

        weaponMG += dir;
        ShowWeaponMG();
    }
    public void ChangeWeaponSP(int dir)
    {
        imageWeaponSP[weaponSP].SetActive(false);

        weaponSP += dir;
        ShowWeaponSP();
    }

    void ShowWeaponCR()
    {
        if (weaponCR >= imageWeaponCR.Length)
            weaponCR = 0;
        else if (weaponCR < 0)
            weaponCR = imageWeaponCR.Length - 1;

        if (imageWeaponCR.Length != 0)
        {
            imageWeaponCR[weaponCR].SetActive(true);
            nameText.text = imageWeaponCR[weaponCR].name;
        }
    }
    void ShowWeaponSO()
    {
        if (weaponSO >= imageWeaponSO.Length)
            weaponSO = 0;
        else if (weaponSO < 0)
            weaponSO = imageWeaponSO.Length - 1;

        if (imageWeaponSO.Length != 0)
        {
            imageWeaponSO[weaponSO].SetActive(true);
            nameText.text = imageWeaponSO[weaponSO].name;
        }
    }
    void ShowWeaponMG()
    {
        if (weaponMG >= imageWeaponMG.Length)
            weaponMG = 0;
        else if (weaponMG < 0)
            weaponMG = imageWeaponMG.Length - 1;

        if (imageWeaponMG.Length != 0)
        {
            imageWeaponMG[weaponMG].SetActive(true);
            nameText.text = imageWeaponMG[weaponMG].name;
        }
    }
    void ShowWeaponSP()
    {
        if (weaponSP >= imageWeaponSP.Length)
            weaponSP = 0;
        else if (weaponSP < 0)
            weaponSP = imageWeaponSP.Length - 1;

        if (imageWeaponSP.Length != 0)
        {
            imageWeaponSP[weaponSP].SetActive(true);
            nameText.text = imageWeaponSP[weaponSP].name;
        }
    }

    public void ShowExplanation(int imgType)
    {
        //Text nameText = explanation.transform.Find("NameText").GetComponent<Text>();
        //Text perText = explanation.transform.Find("PerText").GetComponent<Text>();
        //
        //if (imgType == 0)
        //{
        //    nameText.text = characterNames[1];
        //    perText.text = cperformances[1];
        //}
        explanation.SetActive(true);
    }

    public void ExitExplanation()
    {
        explanation.SetActive(false);
    }

    // 텍스트 파일에서 모든 데이터를 읽어와 배열에 저장
    public static void LoadDataToArray(out string[] names, out string[] performances)
    {
        string filePath = Application.dataPath + "/Script/GameSystem/character_data.txt";
        List<string> nameList = new List<string>();
        List<string> performanceList = new List<string>();

        // 파일이 존재하는지 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError("Character data file does not exist!");
            names = new string[0];
            performances = new string[0];
            return;
        }

        // 파일에서 데이터를 읽어와 배열에 저장
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] keyValue = line.Split('=');
            if (keyValue.Length == 2)
            {
                if (keyValue[0].StartsWith("이름"))
                {
                    nameList.Add(keyValue[1]);
                }
                else if (keyValue[0].StartsWith("성능"))
                {
                    performanceList.Add(keyValue[1]);
                }
            }
        }

        // 배열에 저장
        names = nameList.ToArray();
        performances = performanceList.ToArray();
    }

    // 텍스트 파일에서 모든 데이터를 읽어와 배열에 저장
    public static void LoadDataToArray2(out string[] names, out string[] performances)
    {
        string filePath = Application.dataPath + "/Script/GameSystem/weapon_data.txt";
        List<string> nameList = new List<string>();
        List<string> performanceList = new List<string>();

        // 파일이 존재하는지 확인
        if (!File.Exists(filePath))
        {
            Debug.LogError("Weapon data file does not exist!");
            names = new string[0];
            performances = new string[0];
            return;
        }

        // 파일에서 데이터를 읽어와 배열에 저장
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] keyValue = line.Split('=');
            if (keyValue.Length == 2)
            {
                if (keyValue[0].StartsWith("이름"))
                {
                    nameList.Add(keyValue[1]);
                }
                else if (keyValue[0].StartsWith("성능"))
                {
                    performanceList.Add(keyValue[1]);
                }
            }
        }

        // 배열에 저장
        names = nameList.ToArray();
        performances = performanceList.ToArray();
    }
}
