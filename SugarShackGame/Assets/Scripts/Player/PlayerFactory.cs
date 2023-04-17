using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class PlayerFactory
{
    public int numberOfPlayers = 2;
    private GameObject playerPrefab;
    private Dictionary<KeyValuePair<string, string>, Material> matMap;
    private const string pathMat = "Models/LumberJack/textures/Materials/";
    private const string pathPregab = "Prefabs/Player/Player";

    public List<string> beardColors;
    public List<string> shirtColors;

    public PlayerFactory()
    {
        beardColors = new List<string>();
        shirtColors = new List<string>();

        matMap = new Dictionary<KeyValuePair<string, string>, Material>();
        var mats = Resources.LoadAll<Material>(pathMat);
        
        playerPrefab = Resources.Load<GameObject>(pathPregab);

        foreach (var mat in mats)
        {
            string[] colorNames = mat.name.Split("-");

            if (!beardColors.Contains(colorNames[0]))
                beardColors.Add(colorNames[0]);
            if (!shirtColors.Contains(colorNames[1]))
                shirtColors.Add(colorNames[1]);

            matMap.Add(new KeyValuePair<string, string>(colorNames[0], colorNames[1]), mat);
        }
    }

    public Player CreatPlayer(string beardColor, string shirtColor)
    {
        GameObject playerToRet = GameObject.Instantiate<GameObject>(playerPrefab);

        Renderer renderer = playerToRet.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = matMap[new KeyValuePair<string, string>(beardColor, shirtColor)];
        }

        return playerToRet.GetComponent<Player>();
    }
}

