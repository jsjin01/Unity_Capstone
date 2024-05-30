using System.Collections;
using System.Collections.Generic;
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
}
