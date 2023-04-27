using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupView : MonoBehaviour
{
    public Transform player1SPawn;
    public Transform player2Spawn;
    private PlayerFactory factory;
    Player p1;
    Player p2;
    private string[] beards;
    private string[] shirts;
    private string P1CurrentBeard;
    private string P2CurrentBeard;

    Rigidbody m_Rigidbody;
    Vector3 m_YAxis;

    private void Awake()
    {
        factory = new PlayerFactory();
        beards = factory.beardColors;
        shirts = factory.shirtColors;

         p1 = factory.CreatPlayer(beards[0], shirts[0]);
         p2 = factory.CreatPlayer(beards[1], shirts[0]);

        p1.transform.position = player1SPawn.position;
        p1.transform.rotation = new Quaternion(0,1,0,0);
        p2.transform.position = player2Spawn.position;
        p2.transform.rotation = new Quaternion(0, 1, 0, 0);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
           //switch color of the beard! :)
        }

    }

    //   	        1. List of Sprite of all characters declination
    //			  2. Current Sprite : What is the current selection of color of both player
    //			  3. Save name of players
    //			  4. Save input device for player 2
    //			  5. State of player :  {Setup, Ready}
    //
    //              when Ready : not checking inputs 
}
