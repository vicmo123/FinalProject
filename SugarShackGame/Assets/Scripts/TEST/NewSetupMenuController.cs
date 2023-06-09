using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewSetupMenuController : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    private Dictionary<Player, PlayerViewport> dict;
    private int playerIndex;

    private string[] beards;
    private string[] shirts;
    private int currentShirtIndex;
    private PlayerFactory factory;
    private Player player;

    private float ignoreInputTime = 1.5f;
    private bool inputEnable = false;

    private void Start()
    {
        SetUIResources();
        LoadPlayers();
    }

    private void SetUIResources()
    {
        factory = new PlayerFactory("Prefabs/Player/PlayerDemo");
        beards = factory.beardColors;
        shirts = factory.shirtColors;
    }

    private void LoadPlayers()
    {
        currentShirtIndex = playerIndex;
        player = factory.CreatPlayer(beards[playerIndex], shirts[currentShirtIndex]);

        GameObject spawningParent = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawningParent != null && spawningParent.transform.childCount > 1)
        {
            spawnPosition = spawningParent.transform.GetChild(playerIndex).transform;
            Transform camTransform = spawnPosition.GetChild(0);
            camTransform.gameObject.SetActive(true);
        }

        player.transform.position = spawnPosition.position;
        player.transform.rotation = new Quaternion(0, 1, 0, 0);
        player.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnable = true;
        }
    }

    public void SetColor()
    {
        if (!inputEnable) return;
        PlayerConfigurationManager.Instance.SetPlayerColor(currentShirtIndex, playerIndex, playerIndex);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void LeftArrow()
    {
        currentShirtIndex--;
        if (currentShirtIndex == -1)
        {
            currentShirtIndex = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref player, beards[playerIndex], shirts[currentShirtIndex]);
        StartCoroutine(SelectEffect(player));
    }

    public void RightArrow()
    {
        currentShirtIndex++;
        currentShirtIndex %= shirts.Length;
        factory.ChangePlayerColor(ref player, beards[playerIndex], shirts[currentShirtIndex]);
        StartCoroutine(SelectEffect(player));
    }

    public IEnumerator SelectEffect(Player player)
    {
        float delta = 1f;
        while (delta < 1.2f)
        {
            delta += Time.deltaTime;
            player.transform.localScale = new Vector3(delta, delta, delta);
            yield return null;
        }
    }


    public void ReadyPlayer()
    {
        if (!inputEnable) return;

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
