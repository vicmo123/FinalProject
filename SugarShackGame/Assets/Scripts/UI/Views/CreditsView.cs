using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsView : MonoBehaviour
{

    public Button previousButton;
    public Canvas mainMenu;

    void Start()
    {
        Button previousBtn = previousButton.GetComponent<Button>();

        previousBtn.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        this.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
