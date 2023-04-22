using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : Ability
{
    public override void SpawnAbility(PlayerAbility_Test player)
    {
        base.SpawnAbility(player);       
        Activate(player);        
    }

    public void Activate(PlayerAbility_Test player)
    {
        Debug.Log("Horn effect used by : " + player.gameObject.name);
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
