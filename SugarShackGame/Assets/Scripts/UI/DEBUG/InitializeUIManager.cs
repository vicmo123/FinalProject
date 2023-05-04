using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUIManager : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.Initialize();
    }
}
