using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManger : MonoBehaviour
{
    public static SoundManger i;
    GameObject baseobj;
    [SerializeField] AudioSource Bgm;
    [SerializeField] AudioClip[] clipList;
    /// <summary>
    /// UI 조작음
    /// 0 => 버튼음
    /// 인게임 소리
    /// 1 => 플레이어가 맞는 소리
    /// 2 => 몬스터가 데미지 입는 부분
    /// 3 => Level UP 소리
    /// 4 => Long Sword & Dagger & Spear
    /// 5 => Axe & sickle
    /// 6 => Bow & CrossBow
    /// 7 => Gun
    /// 8 => Thunder
    /// 9 => magic
    /// 10 => support
    /// 11 => 물약 획득 소리
    /// 12 => 물약 사용 소리
    /// 13 => 죽었을 때 사운드
    /// 14 => 깻을 때 사운드
    /// 15 => skill
    /// </summary>
    /// 
    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        baseobj = transform.GetChild(0).gameObject;
        Bgm = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Bgm.volume = PlayerPrefs.GetFloat("Volume_Back", 50);
    }

    public void PlaySound(int id, bool _loop = false)          //사운드 타입과 타입에 따른 인덱스 
    {
        AudioSource s = null;

        //사운드가 플레이 중이지 않은 오브젝트가 있다면
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().isPlaying == false)
            {
                s = transform.GetChild(i).GetComponent<AudioSource>();
                break;
            }
        }

        if (s == null)
        {
            GameObject temp = Instantiate(baseobj, transform); //오브젝트 하나 생성
            s = temp.GetComponent<AudioSource>();
        }

        s.volume = PlayerPrefs.GetFloat("Volume_Effect", 50);
        s.clip = clipList[id];                  //사운드 클립 변경
        s.loop = _loop;                         //루프 상태 변경
        s.Play();                               //사운드 플레이
    }


    public void StopSound(int kind,int id)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<AudioSource>().clip == clipList[id] &&
                transform.GetChild(i).GetComponent<AudioSource>().isPlaying)
            {
                transform.GetChild(i).GetComponent<AudioSource>().Stop();
                break;
            }
        }
    }
}
