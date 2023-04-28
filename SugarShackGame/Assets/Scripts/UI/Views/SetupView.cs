using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupView : MonoBehaviour
{
   
    private PlayerFactory factory;
    private bool p1Connected;
    private bool p2Connected;

    #region Player
    public Transform p1SPawn;
    public Transform p2Spawn;
    private Player p1;
    private Player p2;
    private string[] beards;
    private string[] shirts;
    private int P1CurrentBeard = 0;
    private int P2CurrentBeard = 1;
    private int P1CurrentShirt = 0;
    private int P2CurrentShirt = 3;
    private string p1Device;
    private string p2Device;
    #endregion

    #region  Arrows Buttons
    [Header("Arrows")]
    public Button p1_leftArrowBtn;
    public Button p1_rightArrowBtn;
    public Button p2_leftArrowBtn;
    public Button p2_rightArrowBtn;
    #endregion


    private void Awake()
    {
        factory = new PlayerFactory("Prefabs/Player/PlayerDemo");
        beards = factory.beardColors;
        shirts = factory.shirtColors;
        InitializeButtons();
        LoadPlayers();  
    }
    private void Update()
    {

        if(Input.GetJoystickNames().Length > 0 )
        {
            if (!p1Connected)
            {
               string[] joystickName =  Input.GetJoystickNames();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //++P1CurrentBeard;
            ++P1CurrentShirt;
            // P1CurrentBeard %= beards.Length;
            P1CurrentShirt %= shirts.Length;
            factory.ChangePlayerColor(ref p1, beards[P1CurrentBeard], shirts[P1CurrentShirt]);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //--P1CurrentBeard;
            --P1CurrentShirt;
            if (P1CurrentShirt == -1)
            {
                P1CurrentShirt = shirts.Length - 1;
            }
            //if (P1CurrentBeard == -1)
            //{
            //    P1CurrentBeard = beards.Length - 1;
            //}
            factory.ChangePlayerColor(ref p1, beards[P1CurrentBeard], shirts[P1CurrentShirt]);
        }

    }

    public void P1RightArrow()
    {
        ++P1CurrentShirt;
        P1CurrentShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p1, beards[P1CurrentBeard], shirts[P1CurrentShirt]);
    }

    public void P1LeftArrow()
    {
        --P1CurrentShirt;
        if (P1CurrentShirt == -1)
        {
            P1CurrentShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p1, beards[P1CurrentBeard], shirts[P1CurrentShirt]);
    }

    public void P2RightArrow()
    {
        ++P2CurrentShirt;
        P2CurrentShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p2, beards[P2CurrentBeard], shirts[P2CurrentShirt]);
    }

    public void P2LeftArrow()
    {
        --P2CurrentShirt;
        if (P2CurrentShirt == -1)
        {
            P2CurrentShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p2, beards[P2CurrentBeard], shirts[P2CurrentShirt]);
    }




    //   	        1. List of Sprite of all characters declination
    //			  2. Current Sprite : What is the current selection of color of both player
    //			  3. Save name of players
    //			  4. Save input device for player 2
    //			  5. State of player :  {Setup, Ready}
    //
    //              when Ready : not checking inputs 
    private void InitializeButtons()
    {
        Button p1_Left = p1_leftArrowBtn.GetComponent<Button>();
        p1_Left.onClick.AddListener(P1LeftArrow);

        Button p1_Right = p1_rightArrowBtn.GetComponent<Button>();
        p1_Right.onClick.AddListener(P1RightArrow);

        Button p2_Left = p2_leftArrowBtn.GetComponent<Button>();
        p2_Left.onClick.AddListener(P2LeftArrow);

        Button p2_Right = p2_rightArrowBtn.GetComponent<Button>();
        p2_Right.onClick.AddListener(P2RightArrow);
    }
    private void LoadPlayers()
    {
        p1 = factory.CreatPlayer(beards[0], shirts[0]);
        p2 = factory.CreatPlayer(beards[1], shirts[3]);

        p1.transform.position = p1SPawn.position;
        p1.transform.rotation = new Quaternion(0, 1, 0, 0);
        p1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        p2.transform.position = p2Spawn.position;
        p2.transform.rotation = new Quaternion(0, 1, 0, 0);
        p2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
