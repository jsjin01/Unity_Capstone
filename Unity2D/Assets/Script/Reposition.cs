using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;

    void Awake()
    {
        coll = GetComponent<Collider2D>(); //Collider 2D Get
    }

    //switch (transform.tag)
    //{
    //    case "Enemy":
    //        if(coll.enabled) // Live
    //        {
    //            transform.Translate(playerDir* 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f); // respawn
    //        }
    //        break;
    //}
}
