using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class ClearInputs : MonoBehaviour
{
    public Camera camera;
    public GameObject levelInitializer;

    private void Start()
    {
        FindObjectOfType<PlayerInputManager>().transform.SetParent(levelInitializer.transform);
    }

    public void Clearing()
    {
        camera.gameObject.SetActive(true);
        Destroy(levelInitializer);

        Debug.Log("JOB IS DONE CLEARED");

        //PlayerInputManager playerInputManager =  FindObjectOfType<PlayerInputManager>();
        //List<GameObject> playerInputs = new List<GameObject>();

        //if (playerInputManager != null && playerInputManager.playerCount > 1) {
        //    for (int i = playerInputManager.playerCount - 1; i >= 0; i--)
        //    {
        //       playerInputs.Add(playerInputManager.transform.GetChild(i).gameObject);
        //    }
        //}
        //Destroy(EventSystem.current.gameObject);
        //Destroy(playerInputManager.gameObject);
        //playerInputs.Clear();
    }
}
