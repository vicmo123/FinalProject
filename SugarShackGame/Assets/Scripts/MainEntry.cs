using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.GameManagerSetup();
        GameManager.Instance.PreInitialize();
    }

    void Start()
    {
        GameManager.Instance.Initialize();
    }

    void Update()
    {
        GameManager.Instance.Refresh();
    }

    private void FixedUpdate()
    {
        GameManager.Instance.PhysicsRefresh();
    }
    
}
