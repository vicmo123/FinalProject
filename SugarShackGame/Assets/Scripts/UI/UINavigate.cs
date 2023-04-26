using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum UIPage { Controls, Credits, EndGame, Game, MainMenu, GameDuration, Setup, Pause, Title };

public class UINavigate : MonoBehaviour
{
    private Dictionary<string, GameObject> resourceDict;
    private string folderPath = "Prefabs/UI/Windows";
    private void Awake()
    {
        LoadUI();
    }

    public void LoadUI()
    {
        resourceDict = new Dictionary<string, GameObject>();
        InitializeUI();
    }

    public void Display(UIPage currentPage, UIPage nextPage)
    {
        resourceDict[currentPage.ToString()].SetActive(false);
        resourceDict[nextPage.ToString()].SetActive(true);
    }

    private void InitializeUI()
    {
        var prefabs = Resources.LoadAll<GameObject>(folderPath);

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject go = GameObject.Instantiate(prefabs[i]);
            go.transform.SetParent(this.transform);
            if (prefabs[i].name == "Title")
                go.SetActive(true);
            else
                go.SetActive(false);
            
            resourceDict.Add(prefabs[i].name, go);
        }
    }
    
}