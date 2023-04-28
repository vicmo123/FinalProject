using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory
{
    private GameObject playerPrefab;
    private Dictionary<ColorCombination, Material> materialsMap;
    private const string PATH_MAT = "Models/LumberJack/textures/Materials/";
    private string PATH_PREFAB = "Prefabs/Player/Player";

    public string[] beardColors { get; private set; }
    public string[] shirtColors { get; private set; }

    public PlayerFactory(string File_Path)
    {
        this.PATH_PREFAB = File_Path;
        LoadAssets();
    }

    private void LoadAssets()
    {
        materialsMap = new Dictionary<ColorCombination, Material>();
        var mats = Resources.LoadAll<Material>(PATH_MAT);

        List<string> beardColorsList = new List<string>();
        List<string> shirtColorsList = new List<string>();

        foreach (var mat in mats)
        {
            string[] colorNames = mat.name.Split("-");

            if (!beardColorsList.Contains(colorNames[0]))
                beardColorsList.Add(colorNames[0]);
            if (!shirtColorsList.Contains(colorNames[1]))
                shirtColorsList.Add(colorNames[1]);

            materialsMap.Add(new ColorCombination(colorNames[0], colorNames[1]), mat);
        }
        
        beardColors = beardColorsList.ToArray();
        shirtColors = shirtColorsList.ToArray();

        playerPrefab = Resources.Load<GameObject>(PATH_PREFAB);
    }

    public Player CreatPlayer(string beardColor, string shirtColor)
    {
        GameObject prefab = GameObject.Instantiate<GameObject>(playerPrefab);
        Player playerToRet = prefab.GetComponent<Player>();

        if (playerToRet.renderers != null)
        {
            foreach (var renderer in playerToRet.renderers)
            {
                renderer.material = materialsMap[new ColorCombination(beardColor, shirtColor)];
            }
        }

        return playerToRet.GetComponent<Player>();
    }

    public void ChangePlayerColor(ref Player player, string newBeardColor, string newShirtColor)
    {
        if (player.renderers != null)
        {
            foreach (var renderer in player.renderers)
            {
                renderer.material = materialsMap[new ColorCombination(newBeardColor, newShirtColor)];
            }
        }
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

