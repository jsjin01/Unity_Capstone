using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager i;

    RectTransform rect;

    void Awake()
    {
        i = this;
        rect = GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    public void Show()
    {
        rect.localScale = Vector3.one;
        GameManager.i.Stop();
        CardChange();
    }

    // Update is called once per frame
    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.i.Resume();
    }

    public void selectCard(int index)
    {
        //Card Select
    }

    void CardChange() //Card Random Change
    {
        //Window SetActive() item -> card type   change!

        //foreach(ItemComponent item in items)
        //{
        //    item.gameObject.SetActive(false);
        //}


        //random 3 (90%,10%)
        float[] probs = new float[10] { 90.0f, 90.0f, 90.0f, 90.0f, 90.0f, 90.0f, 10.0f, 10.0f, 10.0f, 10.0f };
        float total = 100.0f;
        float[] rand = new float[3];

        while (true)
        {
            rand[0] = Random.value * total;
            rand[1] = Random.value * total;
            rand[2] = Random.value * total;

            if (rand[0] != rand[1] && rand[1] != rand[2] && rand[0] != rand[2])
                break;
        }


        //GameObject Random SetActive() item -> card type   change!
        for (int i = 0; i < rand.Length; i++)
        {
            //Item ranItem = items[rand[i]];
            //ranItem.ameObject.SetActive(true);
        }
    }
}
