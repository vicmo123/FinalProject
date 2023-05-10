using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnable = false;

    public void SetPlayerIndex( int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString() );
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Update()
    {
        if(Time.time > ignoreInputTime)
        {
            inputEnable = true;
        }
    }

    public void SetColor(int indexShirt)
    {
        if (!inputEnable) return;
        int indexBeard = playerIndex == 0 ? 1 : 0;
        PlayerConfigurationManager.Instance.SetPlayerColor(indexShirt, indexBeard, playerIndex);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void ReadyPlayer()
    {
        if (!inputEnable) return;

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
