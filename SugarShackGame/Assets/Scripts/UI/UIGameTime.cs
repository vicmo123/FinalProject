using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[Manager(typeof(PlayerManager))]
public class UIGameTime : IFlow
{
    private Image[] P1UISlots = new Image[2];
    private Image[] P2UISlots = new Image[2];


    private Ability[] p1Slots;
    private Ability[] p2Slots;

    #region Singleton
    private static UIGameTime instance;

    public static UIGameTime Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIGameTime();
            }
            return instance;
        }
    }

    private UIGameTime()
    {
        //Private constructor to prevent outside instantiation
    }
    #endregion



    public void PreInitialize()
    {
        
        LoadSlots();
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
    }

    public void PhysicsRefresh()
    {
    }
    public void LoadSlots()
    {
        p1Slots = new Ability[2];
        p1Slots[0] = PlayerManager.Instance.players[0].abilityHander.abilitySlots[0];
        p1Slots[1] = PlayerManager.Instance.players[0].abilityHander.abilitySlots[1];

        p2Slots = new Ability[2];
        p2Slots[0] = PlayerManager.Instance.players[1].abilityHander.abilitySlots[0];
        p2Slots[1] = PlayerManager.Instance.players[1].abilityHander.abilitySlots[1];
    }

    public void UpdateSlots()
    {

        P1UISlots[0].sprite = p1Slots[0].sprite;
        P1UISlots[1].sprite = p1Slots[1].sprite;
        P2UISlots[0].sprite = p2Slots[0].sprite;
        P2UISlots[1].sprite = p2Slots[1].sprite;
    }

    public void ConnectUISlots(Image[] p1Slots, Image[] p2Slots)
    {
        for (int i = 0; i < p1Slots.Length; i++)
        {
            this.P1UISlots[i]  = p1Slots[i];
        }

        for (int i = 0; i < p2Slots.Length; i++)
        {
            this.P2UISlots[i] = p2Slots[i];
        }
    }
}
