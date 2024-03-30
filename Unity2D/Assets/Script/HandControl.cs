using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 handPos;
    

    DIRECTION prevDirection = DIRECTION.RIGHT;

    void Awake()
    {
        player = transform.parent.parent.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        
        if(prevDirection != Player.i.dir)
        {
            if (Player.i.dir == DIRECTION.LEFT)
            {
                handPos = new Vector3(-0.15f, -0.15f, 0);
                spriter.flipX = true;
                spriter.sortingOrder = 6;
            }

            else if (Player.i.dir == DIRECTION.RIGHT)
            {
                handPos = new Vector3(-0.15f, -0.15f, 0);
                spriter.flipX = true;
                spriter.sortingOrder = 4;
            }

            prevDirection = Player.i.dir;
        }
        
    }

    public void SetDirection(DIRECTION direction)
    {
        prevDirection = direction;
    }
}
