using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndGame : MonoBehaviour
{
    public GameObject exitBtn;

    private void Start()
    {
        UIManager.Instance.CurrentScene = 1;
    }
    public void ExitBtnPressed()
    {        
        UIManager.Instance.LoadOneScene(ScenesNames.MainMenu);
    }
}
