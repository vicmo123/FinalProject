using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsView : MonoBehaviour
{
    public Canvas mainMenu;

    private void Update()
    {
        if (Input.anyKey)
        {
            TaskOnClick();
        }
    }
    void TaskOnClick()
    {
        this.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
