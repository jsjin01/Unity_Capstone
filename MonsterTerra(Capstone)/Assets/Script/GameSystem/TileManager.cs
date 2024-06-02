using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Ÿ�� ũ��
    private const float tileWidth = 36f;
    private const float tileHeight = 20f;

    // Ÿ���� ���ġ�ϴ� ���� �Ÿ�
    private const float thresholdX = 18f;
    private const float thresholdY = 10f;

    [SerializeField]
    private GameObject[] tiles;

    private void Update()
    {
        TilePos();
    }

    void TilePos()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            Vector3 tilePos = tiles[i].transform.position;
            Vector3 playerPos = GamePlayerMoveControl.i.playerPos;

            if (playerPos.x - tilePos.x > thresholdX)
            {
                tilePos.x += tileWidth;
            }
            else if (playerPos.x - tilePos.x < -thresholdX)
            {
                tilePos.x -= tileWidth;
            }

            if (playerPos.y - tilePos.y > thresholdY)
            {
                tilePos.y += tileHeight;
            }
            else if (playerPos.y - tilePos.y < -thresholdY)
            {
                tilePos.y -= tileHeight;
            }

            tiles[i].transform.position = tilePos;
        }
    }
}
