using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject PlayerSetupMenuPrefab;
    public PlayerInput pi;
    public CustomInputHandler inputHandler;
    private void Awake()
    {
       var rootMenu =  GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = Instantiate(PlayerSetupMenuPrefab, rootMenu.transform);
            pi.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<NewSetupMenuController>().SetPlayerIndex(pi.playerIndex, inputHandler); 
        }
    }
}
