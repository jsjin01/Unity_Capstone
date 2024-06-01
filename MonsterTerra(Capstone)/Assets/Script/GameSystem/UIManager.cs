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

    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
