using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyPoolManager Epool;
    public Player player;

    void Awake()
    {
        instance = this;
    }
}
