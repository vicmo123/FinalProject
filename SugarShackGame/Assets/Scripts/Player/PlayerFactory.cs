using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory
{
    private GameObject playerPrefab;
    private Dictionary<ColorCombination, Material> matMap;
    private const string PATH_MAT = "Models/LumberJack/textures/Materials/";
    private const string PATH_PREFAB = "Prefabs/Player/Player";

    public List<string> beardColorList { get; private set; }
    public List<string> shirtColorList { get; private set; }

    public PlayerFactory()
    {
        beardColorList = new List<string>();
        shirtColorList = new List<string>();

        matMap = new Dictionary<ColorCombination, Material>();
        var mats = Resources.LoadAll<Material>(PATH_MAT);
        
        foreach (var mat in mats)
        {
            string[] colorNames = mat.name.Split("-");

            if (!beardColorList.Contains(colorNames[0]))
                beardColorList.Add(colorNames[0]);
            if (!shirtColorList.Contains(colorNames[1]))
                shirtColorList.Add(colorNames[1]);

            matMap.Add(new ColorCombination(colorNames[0], colorNames[1]), mat);
        }

        playerPrefab = Resources.Load<GameObject>(PATH_PREFAB);
    }

    public Player CreatPlayer(string beardColor, string shirtColor)
    {
        GameObject playerToRet = GameObject.Instantiate<GameObject>(playerPrefab);

        Renderer renderer = playerToRet.transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = matMap[new ColorCombination(beardColor, shirtColor)];
        }

        return playerToRet.GetComponent<Player>();
    }

    private struct ColorCombination
    {
        public string beardColor;
        public string shirtColor;

        public ColorCombination(string beardColor, string shirtColor)
        {
            this.beardColor = beardColor;
            this.shirtColor = shirtColor;
        }
    }
}

