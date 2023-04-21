using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : Ability
{
    public override void Activate(Player player)
    {
        base.Activate(player);
        //TODO
        //Effect of the horn to modify behaviour of animals
        Debug.Log("Horn effect used by : " + player.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Activate(collision.gameObject.GetComponent<Player>());
            gameObject.SetActive(false);
        }
    }

    public override void PreInitialize()
    {
        base.PreInitialize();
        Debug.Log("Horn has been created!");
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
