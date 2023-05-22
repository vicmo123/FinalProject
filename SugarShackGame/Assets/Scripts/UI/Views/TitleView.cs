using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour
{
    private void Awake()
    {
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
