using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEMTPE
{
    NONE,
    ANGRY,
    CONCENTRATION,
    RAPTURE,
    CHALLENGE,
    HEALING
}
public class ItemComponent : MonoBehaviour
{
    [SerializeField]ITEMTPE type;
    [SerializeField] float dTime;

    private void Start()
    {
        Invoke("DestroyItem", dTime); //�����ǰ� ���� �ð� ���� ����
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }
    private void GetItem(int index)
    {
        GamePlayerManager.i.setItme(index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetItem((int)type);
            CancelInvoke("DestroyItem");
            DestroyItem();
        }
    }
}
