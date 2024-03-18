using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    //// prefabs
    //public GameObject[] prefabs;

    //// pool list
    //List<GameObject>[] pools;

    //void Awake()
    //{
    //    pools = new List<GameObject>[prefabs.Length];

    //    for (int i = 0; i < pools.Length; i++)
    //    {
    //        pools[i] = new List<GameObject>(); //pools init
    //    }
    //}

    //public GameObject Get(int index)
    //{
    //    GameObject select = null;

    //    // unenable game object find
    //    foreach (GameObject item in pools[index])
    //    {
    //        if (!item.activeSelf)    // find
    //        {
    //            select = item;
    //            select.SetActive(true);
    //            break;
    //        }
    //    }
    //    // all enable
    //    if (!select)
    //    {
    //        select = Instantiate(prefabs[index], transform); // new
    //        pools[index].Add(select); // make
    //    }

    //    return select;
    //}
    //6.25
}
