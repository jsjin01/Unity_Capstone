using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] itemsPrefabs;
    [SerializeField] float spawnRate = 5;

    Vector2 pos;
    float offset;

    private void Start()
    {
        
    }
}
