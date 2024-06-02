using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager i;


    //인게임 UI
    public GameObject WeaponSet; //웨폰 표기되는 부분 UI
    public string[] Crname;
    public string[] Soname;
    public string[] Mwname;
    public string[] Swname;
    public Sprite[] WeaponImg;

    [SerializeField]GameObject ItemUI = null; //아이템 UI를 가져옴
    public Sprite[] ItemImg;

    //버프 관련 
    GameObject buffUI = null; //버프 UI를 가져옴

    //Option 관련
    [SerializeField] Slider back_vol;
    [SerializeField] Slider effect_vol;
    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        back_vol.value = PlayerPrefs.GetFloat("Volume_Back", 100f);
        effect_vol.value = PlayerPrefs.GetFloat("Volume_Effect", 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Spawner.i.BossKillCount == 2)
        {
            SoundManger.i.PlaySound(14);
            GameObject.Find("MainUI").gameObject.SetActive(false);
            GameObject.Find("GameClear").gameObject.SetActive(true);
            GamePlayerManager.i.hp = GamePlayerManager.i.MaxHp;
            GameManager.i.isLive = false;
        }

        setVolume_effect(effect_vol.value);
        setVolume_background(back_vol.value);
    }

    public void SetWeaponImg(int idx, int i)//무슨 타입, 몇번째 무기 => 무기 이미지 설정
    {
        if(idx == 1)//근거리
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx-1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "CloseRange";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Crname[i];
        }
        else if(idx == 2)//원거리
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx-1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "StandOff";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Soname[i];
        }
        else if(idx == 3)//마법
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx - 1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "MagicWeapon";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Mwname[i];
        }
        else if (idx == 4)//서포팅
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx-1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "SupportWeapon";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Swname[i];
        }
    }

    public void BuffOn(int idx) //활성화
    {
        if(buffUI == null)
        {
            buffUI = GameObject.Find("buff").gameObject; // 버프 UI를 가져옴
        }
        buffUI.transform.GetChild(idx).gameObject.SetActive(true); //활성화
    }

    public void BuffOff(int idx)//비활성화
    {
        if (buffUI == null)
        {
            buffUI = GameObject.Find("buff").gameObject; // 버프 UI를 가져옴
        }

        buffUI.transform.GetChild(idx).gameObject.SetActive(false); //비활성화
    }

    public void ItemImgChange(int num, int idx) //아이템 바꾸기
    {
        if(ItemUI == null)
        {
            ItemUI = GameObject.Find("Item").gameObject;
        }
        ItemUI.transform.GetChild(num).GetChild(2).GetComponent<Image>().sprite = ItemImg[idx];
    }

    public void BtnSound() //버튼음
    {
        SoundManger.i.PlaySound(0);
    }

    public void setVolume_background(float volume)
    {
        Debug.Log("배경음악: " + volume);
        PlayerPrefs.SetFloat("Volume_Back", volume);
        PlayerPrefs.Save();
    }
    public void setVolume_effect(float volume)
    {
        Debug.Log("효과음: " + volume);
        PlayerPrefs.SetFloat("Volume_Effect", volume);
        PlayerPrefs.Save();
    }

    public void SetTime()
    {
        Time.timeScale = 1.0f;
    }
}
