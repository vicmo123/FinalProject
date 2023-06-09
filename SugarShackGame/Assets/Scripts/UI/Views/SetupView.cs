using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;
public enum DirectionType { Up, Down, Left, Right };
public enum DeviceType { Controller, Keyboard, Mouse, XInputControllerWindows };
public enum PlayerID { zero, First, Second };
public class SetupView : MonoBehaviour
{
    private bool DEBUG_MODE = false;

    Dictionary<Player, PlayerViewport> dict;
    public Canvas nextCanvas;
    public GameObject nextCanvasFirstBtn;
    private PlayerFactory factory;
    private PlayerControls actions;

    public PlayerViewport viewport1;
    public PlayerViewport viewport2;
    private bool inputEnabled = true;


    #region PlayerGameData
    private PlayerGameData[] playersGD;

    public Transform p1SPawn;
    public Transform p2Spawn;

    private Player p1;
    private Player p2;
    private string[] beards;
    private string[] shirts;
    #endregion

    private void Awake()
    {
        Debug.Log("Steup Scene");
        this.gameObject.SetActive(true);
        nextCanvas.gameObject.SetActive(false);
        Cursor.visible = false;
        SetUIResources();
        LoadPlayers();
        InitActions();
    }

    private void Update()
    {
        if (!inputEnabled)
        {
            actions.Disable();
        }
        else
        {
            actions.Enable();
        }
    }

    private void SetDefaultPlayerGameData()
    {
        UIManager.Instance.playersGD[0].indexBeard = 0;
        UIManager.Instance.playersGD[0].indexShirt = 0;
        UIManager.Instance.playersGD[1].indexBeard = 1;
        UIManager.Instance.playersGD[1].indexShirt = 3;
    }

    private void LoadPlayers()
    {
        playersGD = UIManager.Instance.playersGD;

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
        actions.UI_Navigation.Right.performed += Right_performed;
        actions.UI_Navigation.Enable();       
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
        for (int i = 0; i < UIManager.Instance.playersGD.Length; i++)
        {
            UIManager.Instance.playersGD[i].indexBeard = playersGD[i].indexBeard;
            UIManager.Instance.playersGD[i].indexShirt = playersGD[i].indexShirt;
        }
    }


    private void GoToNextPage()
    {
        //Hide the characters
        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);
        //Show next Canvas
        nextCanvas.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        //Button selected by default
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextCanvasFirstBtn);
    }

    #region Actions
    #region ArrowActions
    public void P1RightArrow()
    {
        UIManager.Instance.playersGD[0].indexShirt++;
        UIManager.Instance.playersGD[0].indexShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p1, beards[UIManager.Instance.playersGD[0].indexBeard], shirts[UIManager.Instance.playersGD[0].indexShirt]);

        StartCoroutine(SelectEffect(p1));
    }

    public void P1LeftArrow()
    {
        UIManager.Instance.playersGD[0].indexShirt--;
        if (UIManager.Instance.playersGD[0].indexShirt == -1)
        {
            UIManager.Instance.playersGD[0].indexShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p1, beards[UIManager.Instance.playersGD[0].indexBeard], shirts[UIManager.Instance.playersGD[0].indexShirt]);
        StartCoroutine(SelectEffect(p1));
    }

    public void P2RightArrow()
    {
        UIManager.Instance.playersGD[1].indexShirt++;
        UIManager.Instance.playersGD[1].indexShirt %= shirts.Length;
        factory.ChangePlayerColor(ref p2, beards[UIManager.Instance.playersGD[1].indexBeard], shirts[UIManager.Instance.playersGD[1].indexShirt]);
        StartCoroutine(SelectEffect(p2));
    }

    public void P2LeftArrow()
    {
        UIManager.Instance.playersGD[1].indexShirt--;
        if (UIManager.Instance.playersGD[1].indexShirt == -1)
        {
            UIManager.Instance.playersGD[1].indexShirt = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref p2, beards[UIManager.Instance.playersGD[1].indexBeard], shirts[UIManager.Instance.playersGD[1].indexShirt]);
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
        Debug.Log(obj.control.device.deviceId);
        if (!viewport1.IsReady())
        {
            viewport1.DisplayReady();
        }
        else if (!viewport2.IsReady())
        {
            viewport2.DisplayReady();
        }

        if (viewport1.IsReady() && viewport2.IsReady())
        {
            Debug.Log("Everyone is ready!");
            DisableInputForDelay(1.5f);
            ExitActions();
            GoToNextPage();
        }
    }

    private void DisplayAllPlayerGameData()
    {
        foreach (var item in UIManager.Instance.playersGD)
        {
            Debug.Log($"Player info : connected:{item.connected}, deviceId:{item.deviceId}, deviceName:{item.deviceName}, indexBeard:{item.indexBeard}, indexShirt:{item.indexShirt}, name :{item.name}");
        }
    }

    public void DisableInputForDelay(float delayTime)
    {
        if (inputEnabled)
        {
            StartCoroutine(DisableInputCoroutine(delayTime));
        }
    }

    private IEnumerator DisableInputCoroutine(float delayTime)
    {
        inputEnabled = false;
        actions.Disable();
        ;
        yield return new WaitForSeconds(delayTime);

        inputEnabled = true;
    }

    #endregion

    private void ExitActions()
    {
        actions.UI_Navigation.Submit.performed -= Submit_performed;
        actions.UI_Navigation.Left.performed -= Left_performed;
        actions.UI_Navigation.Right.performed -= Right_performed;
    } 
}