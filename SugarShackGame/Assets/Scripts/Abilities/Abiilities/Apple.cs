using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Ability
{
    public override void Activate(Player player)
    {
        base.Activate(player);
        //TODO
        //Throw the apple/snowball
        Debug.Log("Apple effect used by : " + player.gameObject.name);
        gameObject.SetActive(false);
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
        Debug.Log("Apple has been created!");
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
