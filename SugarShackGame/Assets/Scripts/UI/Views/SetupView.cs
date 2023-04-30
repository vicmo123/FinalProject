using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;
public enum DirectionType { Up, Down, Left, Right };
public enum DeviceType { Controller, Keyboard, Mouse };
public enum PlayerID { zero, First, Second };
public class SetupView : MonoBehaviour
{
    private bool DEBUG_MODE = true;

    Dictionary<Player, PlayerViewport> dict;
    public Canvas nextCanvas;
    private PlayerFactory factory;
    private PlayerControls actions;

    public PlayerViewport viewport1;
    public PlayerViewport viewport2;


    #region PlayerGameData
    private PlayerGameData[] players;

    public Transform p1SPawn;
    public Transform p2Spawn;

    private Player p1;
    private Player p2;
    private string[] beards;
    private string[] shirts;
    #endregion

    private void Awake()
    {
        //REMOVE AFTER TESTING :
        UIManager.Instance.Initialize();
        //-------------------------------

        SetUIResources();
        LoadPlayers();
        InitActions();
    }

    private void SetDefaultPlayerGameData()
    {
        UIManager.Instance.players[0].indexBeard = 0;
        UIManager.Instance.players[0].indexShirt = 0;
        UIManager.Instance.players[1].indexBeard = 1;
        UIManager.Instance.players[1].indexShirt = 3;
    }

    private void LoadPlayers()
    {
        players = UIManager.Instance.players;

        SetDefaultPlayerGameData();

        p1 = factory.CreatPlayer(beards[0], shirts[0]);
        p2 = factory.CreatPlayer(beards[1], shirts[3]);
        SetViewports();

        p1.transform.position = p1SPawn.position;
        p1.transform.rotation = new Quaternion(0, 1, 0, 0);
        p1.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        p2.transform.position = p2Spawn.position;
        p2.transform.rotation = new Quaternion(0, 1, 0, 0);
        p2.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void SetViewports()
    {
        dict = new Dictionary<Player, PlayerViewport>();
        dict.Add(p1, viewport1);
        dict.Add(p2, viewport2);
        //Assign the player field in viewport
        dict[p1].LinkPlayer(p1);
        dict[p2].LinkPlayer(p2);
    }

    private void InitActions()
    {
        actions = new PlayerControls();
        actions.UI_Navigation.Submit.performed += Submit_performed;
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


    private void SetUIResources()
    {
        factory = new PlayerFactory("Prefabs/Player/PlayerDemo");
        beards = factory.beardColors;
        shirts = factory.shirtColors;
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

    private void SaveColors()
    {
        for (int i = 0; i < UIManager.Instance.players.Length; i++)
        {
            UIManager.Instance.players[i].indexBeard = players[i].indexBeard;
            UIManager.Instance.players[i].indexShirt = players[i].indexShirt;
        }
    }


    private void GoToNextPage()
    {
        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        nextCanvas.gameObject.SetActive(true);
    }

    #region Actions
    #region ArrowActions
    public void P1RightArrow()
    {
        UIManager.Instance.players[0].indexShirt++;
        UIManager.Instance.players[0].indexShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p1, beards[UIManager.Instance.players[0].indexBeard], shirts[UIManager.Instance.players[0].indexShirt]);

        StartCoroutine(SelectEffect(p1));
    }

    public void P1LeftArrow()
    {
        UIManager.Instance.players[0].indexShirt--;
        if (UIManager.Instance.players[0].indexShirt == -1)
        {
            UIManager.Instance.players[0].indexShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p1, beards[UIManager.Instance.players[0].indexBeard], shirts[UIManager.Instance.players[0].indexShirt]);
        StartCoroutine(SelectEffect(p1));
    }

    public void P2RightArrow()
    {
        UIManager.Instance.players[1].indexShirt++;
        UIManager.Instance.players[1].indexShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p2, beards[UIManager.Instance.players[1].indexBeard], shirts[UIManager.Instance.players[1].indexShirt]);
        StartCoroutine(SelectEffect(p2));
    }

    public void P2LeftArrow()
    {
        UIManager.Instance.players[1].indexShirt--;
        if (UIManager.Instance.players[1].indexShirt == -1)
        {
            UIManager.Instance.players[1].indexShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p2, beards[UIManager.Instance.players[1].indexBeard], shirts[UIManager.Instance.players[1].indexShirt]);
        StartCoroutine(SelectEffect(p2));
    }
    #endregion

    private void Right_performed(InputAction.CallbackContext obj)
    {
        SwitchColor(obj.control.device.deviceId, DirectionType.Right);
    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        SwitchColor(obj.control.device.deviceId, DirectionType.Left);
    }


    private void Submit_performed(InputAction.CallbackContext obj)
    {
        if (obj.control.device.deviceId == (int)PlayerID.First)
        {
            UIManager.Instance.players[0].deviceId = 1;
            UIManager.Instance.players[0].deviceName = obj.control.device.name;
            viewport1.DisplayReady();
        }
        else if (obj.control.device.deviceId == (int)PlayerID.Second)
        {
            UIManager.Instance.players[1].deviceId = 2;
            UIManager.Instance.players[1].deviceName = obj.control.device.name;
            viewport2.DisplayReady();
        }

        if (viewport1.IsReady() && viewport2.IsReady())
        {
            SaveColors();
            GoToNextPage();
        }

        //for debug purpose :
        if (DEBUG_MODE)
        {
            SaveColors();
            GoToNextPage();
            DisplayAllPlayerGameData();
        }
    }

    private void DisplayAllPlayerGameData()
    {
        foreach (var item in UIManager.Instance.players)
        {
            Debug.Log($"Player info : connected:{item.connected}, deviceId:{item.deviceId}, deviceName:{item.deviceName}, indexBeard:{item.indexBeard}, indexShirt:{item.indexShirt}, name:{item.name}");
        }
    }

    #endregion
}