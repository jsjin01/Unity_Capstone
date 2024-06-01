using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager i;


    //�ΰ��� UI
    public GameObject WeaponSet; //���� ǥ��Ǵ� �κ� UI
    public string[] Crname;
    public string[] Soname;
    public string[] Mwname;
    public string[] Swname;
    public Sprite[] WeaponImg;

    [SerializeField]GameObject ItemUI = null; //������ UI�� ������
    public Sprite[] ItemImg;

    //���� ���� 
    GameObject buffUI = null; //���� UI�� ������

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

    public void SetWeaponImg(int idx, int i)//���� Ÿ��, ���° ���� => ���� �̹��� ����
    {
        if(idx == 1)//�ٰŸ�
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx-1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "CloseRange";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Crname[i];
        }
        else if(idx == 2)//���Ÿ�
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx-1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "StandOff";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Soname[i];
        }
        else if(idx == 3)//����
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx - 1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "MagicWeapon";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Mwname[i];
        }
        else if (idx == 4)//������
        {
            WeaponSet.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = WeaponImg[idx-1];
            WeaponSet.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "SupportWeapon";
            WeaponSet.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = Swname[i];
        }
    }

    public void BuffOn(int idx) //Ȱ��ȭ
    {
        if(buffUI == null)
        {
            buffUI = GameObject.Find("buff").gameObject; // ���� UI�� ������
        }
        buffUI.transform.GetChild(idx).gameObject.SetActive(true); //Ȱ��ȭ
    }

    public void BuffOff(int idx)//��Ȱ��ȭ
    {
        if (buffUI == null)
        {
            buffUI = GameObject.Find("buff").gameObject; // ���� UI�� ������
        }

        buffUI.transform.GetChild(idx).gameObject.SetActive(false); //��Ȱ��ȭ
    }

    public void ItemImgChange(int num, int idx) //������ �ٲٱ�
    {
        if(ItemUI == null)
        {
            ItemUI = GameObject.Find("Item").gameObject;
        }
        ItemUI.transform.GetChild(num).GetChild(2).GetComponent<Image>().sprite = ItemImg[idx];
    }
}
