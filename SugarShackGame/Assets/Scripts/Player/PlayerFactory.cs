using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory
{
    private GameObject playerPrefab;
    private Dictionary<ColorCombination, Material> matMap;
    private const string PATH_MAT = "Models/LumberJack/textures/Materials/";
    private const string PATH_PREFAB = "Prefabs/Player/Player";

    public IReadOnlyList<string> beardColorList { get; private set; }
    public IReadOnlyList<string> shirtColorList { get; private set; }

    public PlayerFactory()
    {
        beardColorList = new List<string>();
        shirtColorList = new List<string>();

        matMap = new Dictionary<ColorCombination, Material>();

        LoadAssets();
    }

    private void LoadAssets()
    {
        var mats = Resources.LoadAll<Material>(PATH_MAT);

        List<string> beardColors = new List<string>();
        List<string> shirtColors = new List<string>();

        foreach (var mat in mats)
        {
            string[] colorNames = mat.name.Split("-");

            if (!beardColors.Contains(colorNames[0]))
                beardColors.Add(colorNames[0]);
            if (!shirtColors.Contains(colorNames[1]))
                shirtColors.Add(colorNames[1]);

            matMap.Add(new ColorCombination(colorNames[0], colorNames[1]), mat);
        }

        beardColorList = beardColors;
        shirtColorList = shirtColors;

        playerPrefab = Resources.Load<GameObject>(PATH_PREFAB);
    }

    public Player CreatPlayer(string beardColor, string shirtColor)
    {
        GameObject prefab = GameObject.Instantiate<GameObject>(playerPrefab);
        Player playerToRet = prefab.GetComponent<Player>();

        if (playerToRet.playerRenderer != null)
        {
            playerToRet.playerRenderer.material = matMap[new ColorCombination(beardColor, shirtColor)];
        }

        return playerToRet.GetComponent<Player>();
    }

    public void ChangePlayerColor(ref Player player, string newBeardColor, string newShirtColor)
    {
        if (player.playerRenderer != null)
        {
            player.playerRenderer.material = matMap[new ColorCombination(newBeardColor, newShirtColor)];
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

