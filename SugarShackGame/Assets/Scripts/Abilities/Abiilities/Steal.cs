using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : Ability
{
    public override void SpawnAbility(PlayerAbility_Test player)
    {
        base.SpawnAbility(player);
        //TODO
        //Effect of the Steal to 
        // 1. Empty Slot of the other player
        // 2. fill player's Slot with stolen ability
        Debug.Log("Steal effect used by : " + player.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpawnAbility(collision.gameObject.GetComponent<PlayerAbility_Test>());
            gameObject.SetActive(false);
        }
    }

    public override void PreInitialize()
    {
        base.PreInitialize();
        Debug.Log("Steal has been created!");
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void PhysicsRefresh()
    {
        base.PhysicsRefresh();
    }


    public override void Refresh()
    {
        base.Refresh();
    }

}
