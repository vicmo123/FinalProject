using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;
public enum DirectionType { Up, Down, Left, Right };
public enum PlayerID { zero, First, Second };
public class SetupView : MonoBehaviour
{
    public Canvas nextCanvas;
    private PlayerFactory factory;
    private PlayerControls actions;

    #region Player
    private PlayerGameData[] players;

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
    private bool p1Connected = false;
    private bool p2Connected = false;
    #endregion

    #region  Arrows Buttons
    [Header("Arrows")]
    public Button p1_leftArrowBtn;
    public Button p1_rightArrowBtn;
    public Button p2_leftArrowBtn;
    public Button p2_rightArrowBtn;
    private double holdDuration = 3f;
    private double startTime = 0;
    bool actionOver = false;
    #endregion

    #region ConnectedImg
    public Image[] connectImg;
    public TMP_Text[] texts;
    public Image[] devices;
    private Sprite[] devicesSprites;
    #endregion


    private void Awake()
    {
        //REMOVE AFTER TESTING :
        UIManager.Instance.Initialize();
        //-------------------------------
        factory = new PlayerFactory("Prefabs/Player/PlayerDemo");
        beards = factory.beardColors;
        shirts = factory.shirtColors;
        LoadPlayers();
        LoadImages();
        InitActions();
    }

    private void Submit_started(InputAction.CallbackContext obj)
    {
        actionOver = false;
        startTime = obj.startTime;
    }

    private void Submit_performed(InputAction.CallbackContext obj)
    {
        actionOver = true;
        Debug.Log("Submit was held for :" + obj.duration + " duration");
        if (obj.control.device.deviceId == (int)PlayerID.First && obj.duration >= holdDuration)
        {
            connectImg[0].color = new Color(0.237f, 0.99f, 0.088f, 0.63f);
            texts[0].text = "Connected";

            if (obj.control.device.name != "Keyboard")
            {
                //Sprite : controller appears for conection
                devices[0].sprite = devicesSprites[0];
            }
            else
            {
                //Sprite : keyboard appears for conection
                devices[0].sprite = devicesSprites[1];
            }
        }

        if (obj.control.device.deviceId == (int)PlayerID.Second && obj.duration <= holdDuration)
        {
            connectImg[1].color = new Color(0.87f, 0.00f, 0.98f, 0.01f);
            connectImg[1].GetComponentInChildren<TextMeshPro>().text = "Connected";
        }

        SaveColors();
        GoToNextPage();
    }
    private void SaveColors()
    {
        for (int i = 0; i < UIManager.Instance.players.Length; i++)
        {
            UIManager.Instance.players[i].indexBeard = players[i].indexBeard;
            UIManager.Instance.players[i].indexShirt = players[i].indexShirt;
        }
    }

    private void Right_performed(InputAction.CallbackContext obj)
    {
        SwitchColor(obj.control.device.deviceId, DirectionType.Right);
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        SwitchColor(obj.control.device.deviceId, DirectionType.Left);

    }
    private void SwitchColor(int id, DirectionType type)
    {
        switch (type)
        {
            case DirectionType.Left:
                if (id == (int)PlayerID.First)
                    P1LeftArrow();
                else
                    P2LeftArrow();

                break;
            case DirectionType.Right:
                if (id == (int)PlayerID.First)
                    P1RightArrow();
                else
                    P2RightArrow();

                break;
            default:
                break;
        }
        Player player = (id == (int)PlayerID.First) ? p1 : p2;
        StartCoroutine(SelectEffect(player));

    }

    public void P1RightArrow()
    {
        ++P1CurrentShirt;
        P1CurrentShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p1, beards[P1CurrentBeard], shirts[P1CurrentShirt]);
        StartCoroutine(SelectEffect(p1));
    }

    public void P1LeftArrow()
    {
        --P1CurrentShirt;
        if (P1CurrentShirt == -1)
        {
            P1CurrentShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p1, beards[P1CurrentBeard], shirts[P1CurrentShirt]);
        StartCoroutine(SelectEffect(p1));
    }

    public void P2RightArrow()
    {
        ++P2CurrentShirt;
        P2CurrentShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p2, beards[P2CurrentBeard], shirts[P2CurrentShirt]);
        StartCoroutine(SelectEffect(p2));
    }

    public void P2LeftArrow()
    {
        --P2CurrentShirt;
        if (P2CurrentShirt == -1)
        {
            P2CurrentShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p2, beards[P2CurrentBeard], shirts[P2CurrentShirt]);
        StartCoroutine(SelectEffect(p2));
    }

    public IEnumerator SelectEffect(Player player)
    {
        float delta = 1.5f;
        while (delta < 1.7f)
        {
            delta += Time.deltaTime;
            player.transform.localScale = new Vector3(delta, delta, delta);
            yield return null;
        }
    }

    //   	        1. List of Sprite of all characters declination
    //			  2. Current Sprite : What is the current selection of color of both player
    //			  3. Save name of players
    //			  4. Save input device for player 2
    //			  5. State of player :  {Setup, Ready}
    //
    //              when Ready : not checking inputs 

    private void LoadPlayers()
    {
        players = UIManager.Instance.players;
        players[0].indexBeard = 0;
        players[0].indexShirt = 0;
        players[1].indexBeard = 1;
        players[1].indexShirt = 3;
        p1 = factory.CreatPlayer(beards[players[0].indexBeard], shirts[players[0].indexShirt]);
        p2 = factory.CreatPlayer(beards[players[1].indexBeard], shirts[players[1].indexShirt]);

        PlayerInput plInput1 = p1.gameObject.GetComponent<PlayerInput>();
        PlayerInput plInput2 = p2.gameObject.GetComponent<PlayerInput>();

        p1.transform.position = p1SPawn.position;
        p1.transform.rotation = new Quaternion(0, 1, 0, 0);
        p1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        p2.transform.position = p2Spawn.position;
        p2.transform.rotation = new Quaternion(0, 1, 0, 0);
        p2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void LoadImages()
    {
        devicesSprites = Resources.LoadAll<Sprite>("Sprites/UI_Views/Devices");
    }

    private void InitActions()
    {
        actions = new PlayerControls();
        actions.UI_Navigation.Submit.performed += Submit_performed;
        actions.UI_Navigation.Submit.started += Submit_started;
        actions.UI_Navigation.Left.performed += Left_performed;
        actions.UI_Navigation.Right.performed += Right_performed; ;
        actions.UI_Navigation.Enable();

        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    //if id correspond to a player, !assign it
                    for (int i = 0; i < UIManager.Instance.players.Length; i++)
                    {
                        if (UIManager.Instance.players[i].connected == false && UIManager.Instance.players[i].deviceId == 0)
                        {
                            UIManager.Instance.players[i].deviceId = device.deviceId;
                            UIManager.Instance.players[i].deviceName = device.name;
                            UIManager.Instance.players[i].connected = true;
                            Debug.Log("New device added for Player " + UIManager.Instance.players[i].GetId() + " : " + device.name);
                        }
                    }
                    //Assign to a player : Id, device
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged.
                    for (int i = 0; i < UIManager.Instance.players.Length; i++)
                    {
                        if (UIManager.Instance.players[i].connected == true && UIManager.Instance.players[i].deviceId == device.deviceId)
                        {
                            UIManager.Instance.players[i].connected = false;
                        }
                    }
                    break;
                case InputDeviceChange.Reconnected:
                    // Plugged back in.
                    for (int i = 0; i < UIManager.Instance.players.Length; i++)
                    {
                        if (UIManager.Instance.players[i].connected == false && UIManager.Instance.players[i].deviceId == device.deviceId)
                        {
                            UIManager.Instance.players[i].connected = true;
                        }
                    }
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };


    }

    private void GoToNextPage()
    {
        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
    }

}


