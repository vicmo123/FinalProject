using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("title scene");
        UIManager.Instance.Initialize();
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            TaskOnClick();
        }
    }
    void TaskOnClick()
    {
        UIManager.Instance.LoadNextScene();
    }
}
