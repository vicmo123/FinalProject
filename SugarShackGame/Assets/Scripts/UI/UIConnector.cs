using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIConnector : MonoBehaviour, IFlow
{
    public Canvas pause_window;
    public Canvas endGame_window;
    public Canvas mainMenu_window;
    public TMP_Text timerMin_TextBox;
    public TMP_Text timerSec_TextBox;
    public Image viewport1;
    public Image viewport2;
    public Image slot1P1;
    public Image slot2P1;
    public Image slot1P2;
    public Image slot2P2;

    public Image bucketP1;
    public Image bucketP2;
    public Image cansP1;
    public Image cansP2;


    private UIPlayerManager UIManagerP1;
    private UIPlayerManager UIManagerP2;

    bool gameOnPause = false;
    float countdown;
    const float GAME_DURATION = 320;
    public void PreInitialize()
    {
    }
    public void Initialize()
    {
        Image[] slots = { slot1P1, slot2P1 };
        UIManagerP1.Connect(viewport1, slots, bucketP1, cansP1);
        UIManagerP2.Connect(viewport1, slots, bucketP1, cansP1);
    }

    public void PhysicsRefresh()
    {
    }

    

    public void Refresh()
    {
    }
    
}
