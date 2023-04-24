using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBottle : Ability
{
    public override void SpawnAbility(PlayerAbility_Test player)
    {
        base.SpawnAbility(player);
        //TODO
        //Effect of the MysteryBottle to modify behaviour of player
        //Random between 3 effets :
        // 1. Drunk
        // 2. Freeze
        // 3. Slow
        Debug.Log("MysteryBottle effect used by : " + player.gameObject.name);
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
        Debug.Log("MysteryBottle has been created!");
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