using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    [SerializeField]
    private int maxPlayers = 2;
    [SerializeField]
    private GameObject StartCanvas;
    [SerializeField]
    private GameObject[] selectionCanvas;

    public static PlayerConfigurationManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Trying to instantiate another Test_Multiplayer_Input, which is a singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void SetPlayerColor(int IndexColorShirt, int IndexColorBeard, int index)
    {
        playerConfigs[index].IndexColorBeard = IndexColorBeard;
        playerConfigs[index].IndexColorShirt = IndexColorShirt;

    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            try
            {
                SceneManager.LoadScene("NewGamePlay");
            }
            catch
            {
                throw new System.Exception("Cannot load next scene, might be the incorrect spelling.");
            }
        }
    }

    public void HandlePLayerJoin(PlayerInput pi)
    {
        if (pi.playerIndex == 0)
            StartCanvas.SetActive(false);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));

            if(selectionCanvas.Length > 1)
            {
                CustomInputHandler inputHandler = pi.gameObject.transform.GetComponent<CustomInputHandler>();
                selectionCanvas[pi.playerIndex].GetComponent<SelectionPlayerCanvas>().SetLinks(pi, inputHandler);
            }         
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }
}

public class PlayerConfiguration
{
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public int IndexColorShirt { get; set; }
    public int IndexColorBeard { get; set; }

    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public void SetCamera(Camera camera)
    {
        Input.camera = camera;
    }
}