using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;

    private List<Transform> childs;
    private List<Player> players;
    private List<PlayerInputHandler> playerInoutHandlers;

    private PlayerInputManager pim;
    // Start is called before the first frame update
    void Start()
    {
        PlayerConfigurationManager pcm = FindObjectOfType<PlayerConfigurationManager>();
        PlayerInputManager pim = pcm.GetComponent<PlayerInputManager>();
        //pim.playerPrefab = 
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        Debug.Log("playerConfigs.Length" + playerConfigs.Length);

        //pim = pcm.GetComponent<PlayerInputManager>();

        players = new List<Player>();
        //childs = new List<Transform>();
        //playerInoutHandlers = new List<PlayerInputHandler>();


        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //Instantiating the player + putting it in a list
            players.Add(PlayerManager.Instance.InitializePlayer(playerConfigs[i]));
            Camera camera = players[i].GetComponentInChildren<Camera>();
            Rect cameraRect = new Rect(camera.rect)
            {
                x = i * 0.5f,
                width = 0.5f
            };
            camera.rect = cameraRect;

            playerConfigs[i].SetCamera(players[i].GetComponentInChildren<Camera>());
        }
        //    //Set the camera on the PlayerInput on PlayerConfiguration to be linked to the camera of Player(cinemachine)

        //    //Give the PlayerInput(on PlayerConfiguration) to PlayerController on Player
        //    players[i].GetComponent<PlayerController>().SetPlayerInput(playerConfigs[i].Input);
        //    Debug.Log("PlayerController is : " + players[i].GetComponent<PlayerController>() + " has input " + players[i].GetComponent<PlayerController>()._playerInput);

        //}

        //if (pcm.transform.childCount > 0)
        //{
        //    //get the children : PlayerConfiguration and Register to actions
        //    for (int i = 0; i < pcm.transform.childCount; i++)
        //    {
        //       //pcm.transform.GetChild(i).GetComponent<PlayerInputHandler>().RegisterToActions();
        //    }
        //}


    }


}
