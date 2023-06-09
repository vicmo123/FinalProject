using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Viewport : MonoBehaviour
{
    public Image bucket;
    public TMP_Text nbCans;
    public Image[] images;

    private Image[] slots;
    private Player player;

    private void Start()
    {
        slots = new Image[2];

        for (int i = 0; i < images.Length; i++)
        {
            slots[i] = images[i].GetComponent<Image>();          
        }
        
    }
    public void LinkPlayer(Player player)
    {
        this.player = player;
    }

    public void RefreshSlots(Sprite[] sprites)
    {
        for (int i = 0; i < images.Length; i++)
        {
            slots[i].sprite = sprites[i];
        }
    }

    public void RefreshBucket(float amount)
    {
        bucket.GetComponent<Image>().fillAmount = amount / 30;
    }

    public void RefreshSyrup(int nbCans)
    {
        this.nbCans.text = nbCans.ToString();
    }
}
