using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Viewport : MonoBehaviour
{
    public Image bucket;
    public TMP_Text nbCans;
    public Image[] slots;
    private Player player;

    public void LinkPlayer(Player player)
    {
        this.player = player;
    }

    public void RefreshSlots(Sprite[] sprites)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = sprites[i];
        }
    }
}
