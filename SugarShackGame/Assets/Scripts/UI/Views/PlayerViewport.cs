using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerViewport : MonoBehaviour
{
    public Transform spawn;
    public Button leftArrowBtn;
    public Button rightArrowBtn;
    public Image connectImg;
    public TMP_Text text;
    public Image deviceImg;
    public Image readyImg;
    private Player player;
    private Sprite[] devicesSprites;
    private Sprite[] readySprites;
    public double startTime = 0;
    private bool isReady = false;
    
    private void LoadResources()
    {
        devicesSprites = Resources.LoadAll<Sprite>("Sprites/UI_Views/Devices");
        readySprites = Resources.LoadAll<Sprite>("Sprites/UI_Views/Ready");
    }

    public void LinkPlayer(Player player)
    {
        LoadResources();

        this.player = player;
        PlayerInput input = player.gameObject.GetComponent<PlayerInput>();
        if (input.user.pairedDevices.Count > 0)
        {
            foreach (InputDevice item in input.user.pairedDevices)
            {
                if (item.device.name == DeviceType.Keyboard.ToString())
                {
                    DisplayConnecteMsg(DeviceType.Keyboard);                    
                }
                else if (item.device.name == DeviceType.Mouse.ToString())
                {
                    DisplayConnecteMsg(DeviceType.Keyboard);
                }
                else if (item.device.name == DeviceType.XInputControllerWindows.ToString())
                {
                    DisplayConnecteMsg(DeviceType.Controller);
                }
            }
        }
        readyImg.sprite = readySprites[1];
    }

    public void DisplayConnecteMsg(DeviceType type)
    {
        connectImg.color = new Color(0.237f, 0.99f, 0.088f, 0.63f);
        text.text = "Connected";

        DisplayDeviceImg(type);
    }

    public void DisplayDeviceImg(DeviceType type)
    {
        switch (type)
        {
            case DeviceType.Controller:
                deviceImg.sprite = devicesSprites[0];
                break;
            case DeviceType.Keyboard:
                deviceImg.sprite = devicesSprites[1];
                break;
            case DeviceType.Mouse:
                deviceImg.sprite = devicesSprites[1];
                break;
            default:
                break;
        }
    }

    public void DisplayReady()
    {
        readyImg.sprite = readySprites[1];
        isReady = true;
    }

    public bool IsReady()
    {
        return isReady;
    }
    
    public void FeedbackArrow(DirectionType direction)
    {
        //Polishing
    }    
}