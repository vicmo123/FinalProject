using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerManager : IFlow
{
    private Image viewport;
    private Player player;

    private Image[] slots;
    private Image bucketUI;
    private Image cansUI;
          
    public void Connect( Image viewport, Image[] slots, Image bucketUI, Image cansUI)
    {
        this.viewport = viewport;
        this.slots = slots;
        this.bucketUI = bucketUI;
        this.cansUI = cansUI;
    }

    public void PreInitialize()
    {

    }

    public void Initialize()
    {
    }

    public void PhysicsRefresh()
    {
    }



    public void Refresh()
    {
    }

    public void LinkPlayer(Player player)
    {
        this.player = player;
    }
}
