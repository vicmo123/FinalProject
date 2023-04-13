using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = new GameManager();
        gameManager.PreInitialize();
    }

    void Start()
    {
        gameManager.Initialize();
    }

    void Update()
    {
        gameManager.Refresh();
    }

    private void FixedUpdate()
    {
        gameManager.PhysicsRefresh();
    }
    
}
