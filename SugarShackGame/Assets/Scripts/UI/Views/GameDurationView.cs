using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameDurationView : MonoBehaviour
{
    
    private void Start()
    {
        Cursor.visible = false;
    }
    
    public void SetGameDuration(float duration)
    {
        UIManager.Instance.gameDuration = duration * 60.0f;

        StartGameBtnPressed();
    }

    public void StartGameBtnPressed()
    {
        UIManager.Instance.LoadNextScene();
    }

}
