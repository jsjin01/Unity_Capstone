using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UISystem : MonoBehaviour
{
    public InfoType type;
    public Text nameText;   //ImageName
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

    public int character = 0;  //Character No
    public int weaponCR = 0;   //CR Weapon No
    public int weaponSO = 0;   //SO Weapon No
    public int weaponMG = 0;   //MG Weapon No
    public int weaponSP = 0;   //SP Weapon No

    Text myText;    //Stage Text
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
        if(explanation != null)
        {
            LoadDataToArray(out characterNames, out cperformances);
            LoadDataToArray2(out weaponNames, out wperformances);
        }
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
            case InfoType.Level:
                myText.text = string.Format("Lv.{0}", GamePlayerManager.i.lv + 1);
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
    //Show Name, Show Perfomance

    public void GameStart()
    {
        //각각의 인덱스 가져오기
        int c = GameObject.Find("CharcterImg").GetComponent<UISystem>().character;
        int cr = GameObject.Find("CloseRangeImg").GetComponent<UISystem>().weaponCR;
        int so = GameObject.Find("StandOffImg").GetComponent<UISystem>().weaponSO;
        int mw = GameObject.Find("MagicImg").GetComponent<UISystem>().weaponMG;
        int sw = GameObject.Find("SWImg").GetComponent<UISystem>().weaponSP;
        
        //해당 캐릭터 객체 활성화
        GameObject.Find("Character").transform.GetChild(c).gameObject.SetActive(true);
        GamePlayerManager.i.selectCharacter();
        GamePlayerMoveControl.i.anit = GamePlayerManager.i.Character.transform.GetChild(0).GetComponent<Animator>();

        //Main UI 가져오기
        UIManager.i.WeaponSet = GameObject.Find("WeaponSet").gameObject;

        //무기 설정
        WeaponManager.i.SetWeapon(cr,so,mw,sw, GamePlayerManager.i.CC);
        WeaponManager.i.changeWeapon();

        //게임 시작
        GameManager.i.isLive = true;
    }
    public void ShowExplanation(int imgType)
    {
        // explanation 객체가 null인지 확인
        if (explanation != null)
        {
            // explanation 게임 오브젝트의 자식에 있는 Text 컴포넌트를 가져옴
            Text _nameText = explanation.transform.Find("ExplantionTitle").GetComponent<Text>();
            Text _perText = explanation.transform.Find("ExplantionT").GetComponent<Text>();

            if (imgType == 0)
            {
                _nameText.text = characterNames[character];
                _perText.text = cperformances[character];
            }
            else if(imgType == 1)
            {
                _nameText.text = weaponNames[weaponCR];
                _perText.text = wperformances[weaponCR];
            }
            else if (imgType == 2)
            {
                _nameText.text = weaponNames[weaponSO + 5];
                _perText.text = wperformances[weaponSO + 5];
            }
            else if (imgType == 3)
            {
                _nameText.text = weaponNames[weaponMG + 10];
                _perText.text = wperformances[weaponMG + 10];
            }
            else if (imgType == 4)
            {
                _nameText.text = weaponNames[weaponSP + 15];
                _perText.text = wperformances[weaponSP + 15];
            }
            explanation.SetActive(true);
        }
        else
        {
            // explanation 객체가 null인 경우에 실행될 코드
            Debug.LogWarning("Explanation object is null!");
        }
    }
    //Exit Explanation
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
