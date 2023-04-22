using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketCover : Ability
{
    public override void SpawnAbility(PlayerAbility_Test player)
    {
        base.SpawnAbility(player);
        //TODO
        //Effect of the BucketCover to modify behaviour of the player's bucket
        Debug.Log("BucketCover effect used by : " + player.gameObject.name);
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
        Debug.Log("BucketCover has been created!");
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
