using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour
{
    public PlayerAbility_Test p1;
    public PlayerAbility_Test p2;
   
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Player 1 is activating the ability
            p1.UsePower();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //Player 2 is activating the ability
            p2.UsePower();

        }
    }

    public void P1ActivateAbility()
    {
        p1.UsePower();
    }

    public void P2ActivateAbility()
    {
        p2.UsePower();
    }
}
