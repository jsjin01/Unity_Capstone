using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum GUN 
{
    HANDGUN,    
    SHOTGUN,   
    RIFFLE     
}

public class GunWeaponComponent : MonoBehaviour
{
    public static GunWeaponComponent i;
    Vector3 Pos = Vector3.zero;                 
    Vector3 aimPos = Vector3.zero;              
    Quaternion rotation = Quaternion.identity;

    public int atk { get; private set; } = 10;              
    float atkSpd = 0.1f;                                
    public GUN type = GUN.HANDGUN;    
    bool isAttack = true;                                      
                                          
    [SerializeField] Sprite[] gunSprite;    
    [SerializeField] SpriteRenderer sr;

    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        WeaponComponent.i.gEvt += () =>
        {
            StopAllCoroutines();
            isAttack = true;
        };
    }
    // Update is called once per frame
    public void Update()
    {
        type = WeaponComponent.i.g;
        ChangeSprite(type);
        Pos = transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            if (isAttack)
            {
                Shoot(Player.i.dir);
            }
        }
    }

    public void Shoot(DIRECTION dir)
    {
        StartCoroutine(ShootCol());
        Aim(dir);
        BulletPoolManager.i.UseBullet(Pos, aimPos, rotation);
    }

    void Aim(DIRECTION dir) 
    {
        if(dir == DIRECTION.UP)
        {
            rotation = Quaternion.Euler(0,0,0);
            aimPos = new Vector3(0,1,0);
        }
        else if (dir == DIRECTION.DOWN)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            aimPos = new Vector3(0, -1, 0);
        }
        else if (dir == DIRECTION.LEFT)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            aimPos = new Vector3(-1, 0, 0);
        }
        else if (dir == DIRECTION.RIGHT)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            aimPos = new Vector3(1, 0, 0);
        }
    }

    IEnumerator ShootCol() //Atk Rate
    {
        isAttack = false;
        yield return new WaitForSeconds(atkSpd);
        isAttack = true;
    }

    void ChangeSprite(GUN g)
    {
        if(g == GUN.HANDGUN)
        {
            sr.sprite = gunSprite[0];
        }
        else if(g == GUN.SHOTGUN)
        {
            sr.sprite = gunSprite[1];
        }
        else if (g == GUN.RIFFLE)
        {
            sr.sprite = gunSprite[2];
        }
    }
}
