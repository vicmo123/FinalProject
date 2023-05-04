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
    public Sprite test;
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
}
