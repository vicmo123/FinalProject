using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

public class FloatingPointsHandler : MonoBehaviour, IFlow
{
    public GameObject floatingPoint;
    public Transform attatchPoint;
    private Color color;
    private float points;
    private PlayerScore playerScore;
    private Color[] colors;
    private bool miror = false;
    
    public Action<string> makePointsEffect;


    public void PreInitialize()
    {
        makePointsEffect = (val) => { SpawnFloatPoint(val); };
        colors = new Color[5] { Color.blue, Color.yellow, Color.magenta, Color.green, Color.red };
    }

    private void SpawnFloatPoint(string points)
    {
        GameObject newFloatingPoint = Instantiate<GameObject>(floatingPoint);
        newFloatingPoint.transform.SetParent(attatchPoint);
        TMP_Text text = newFloatingPoint.GetComponent<TMP_Text>();
        text.text = "+" + points;
        int colorIndex = Random.Range(0, (int)colors.Length);
        text.color = colors[colorIndex];
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
    }

    public void PhysicsRefresh()
    {
    }
}