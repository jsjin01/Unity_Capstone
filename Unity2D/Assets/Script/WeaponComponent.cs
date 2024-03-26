using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    public static WeaponComponent i;
    [SerializeField]GameObject[] Weapons = new GameObject[3];
    int index = 0;

    public Action gEvt;
    public Action sEvt;
    public Action mEvt;
    
    //arms-setting
    [SerializeField] public GUN g;
    [SerializeField] public STICK s;
    [SerializeField] public MAGIC m;
    // Start is called before the first frame update
    private void Awake()
    {
        i = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }

        //arms reorientation
        if (Player.i.dir == DIRECTION.RIGHT)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if(Player.i.dir == DIRECTION.LEFT)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void ChangeWeapon() //a weapon replacement
    {
        if(index == 0)
        {
            gEvt();
            Weapons[index].SetActive(false);
            Weapons[index+1].SetActive(true);
            index++;
        }
        else if(index == 1)
        {
            sEvt();
            Weapons[index].SetActive(false);
            Weapons[index + 1].SetActive(true);
            index++;
        }
        else
        {
            mEvt();
            Weapons[index].SetActive(false);
            index = 0;
            Weapons[index].SetActive(true);
        }
    }
}
