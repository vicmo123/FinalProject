using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InitializeLevel : MonoBehaviour
{    
    private List<Player> players;    

    void Start()
    {
        PlayerConfigurationManager pcm = FindObjectOfType<PlayerConfigurationManager>();
     
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        

        players = new List<Player>();


        for (int i = 0; i < playerConfigs.Length; i++)
        {
            //Instantiating the player + putting it in a list
            players.Add(PlayerManager.Instance.InitializePlayer(playerConfigs[i]));
            Camera camera = players[i].GetComponentInChildren<Camera>();
            //Setting the split screen
            Rect cameraRect = new Rect(camera.rect)
            {
                x = i * 0.5f,
                width = 0.5f
            };
            camera.rect = cameraRect;

            playerConfigs[i].SetCamera(players[i].GetComponentInChildren<Camera>());
        }
    }
}