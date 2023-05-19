using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class NewSetupMenuController : MonoBehaviour
{
    private GameObject selectionCanvas;
    private Transform playerCanvas;
    private Transform selectionMenu;
    private Transform readyMenu;

    private PlayerInputManager playerInputManager;
    private CustomInputHandler inputHandler;
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

    private float ignoreInputTime = 0.2f;
    private float startInputTime = 0.5f;
    private bool inputEnable = false;
    private bool readyPlayer = false;
    private bool spawnPlayer = false;

    private void Start()
    {
        SetUIResources();
        LoadPlayers();
        playerInputManager = GameObject.FindGameObjectWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
    }

    private void SetUIResources()
    {
        factory = new PlayerFactory("Prefabs/Player/PlayerDemo");
        beards = factory.beardColors;
        shirts = factory.shirtColors;

        selectionCanvas = GameObject.Find("SelectionCanvas");
        playerCanvas = selectionCanvas.transform.GetChild(playerIndex);
        selectionMenu = playerCanvas.GetChild(0);
        readyMenu = selectionCanvas.transform.GetChild(1);
        
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

    public void SetPlayerIndex(int pi, CustomInputHandler _inputHandler)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi + 1).ToString());
        
        inputHandler = _inputHandler;
        inputHandler.Jump = false;
    }

    private void Update()
    {
        if(!spawnPlayer)
        {
            playerInputManager.onPlayerJoined += Instance_onPlayerJoined;
            //InputSystem.onAnyButtonPress.CallOnce(any => WaitForSpawn());           
        }
        else if (Time.time > ignoreInputTime )
        {
            inputEnable = true;
            if (!readyPlayer)
            {
                bool selectLeft = inputHandler.SelectLeft;
                bool selectright = inputHandler.SelectRight;
                Vector2 move = new Vector3(inputHandler.Move.x, 0.0f, inputHandler.Move.y).normalized;

                if(selectLeft)
                    LeftArrow();
                else if (move == Vector2.left)
                    LeftArrow();

                if (selectright)
                    RightArrow();
                else if (move == Vector2.right)
                    RightArrow();

            }

            bool submit = inputHandler.Jump;
            if (submit)
            {
                ReadyPlayer();
            }
        }
    }

    private void Instance_onPlayerJoined(PlayerInput obj)
    {
        Debug.Log("A player just joined.");
        ignoreInputTime = Time.time + startInputTime;
        spawnPlayer = true;
    }

    private void WaitForSpawn()
    {
        Debug.Log("Wait for spawn");
        spawnPlayer = true;
        ignoreInputTime = Time.time + startInputTime;
    }

    public void SetColor()
    {
        //PlayerConfigurationManager.Instance.SetPlayerColor(currentShirtIndex, playerIndex, playerIndex);
        //readyPanel.SetActive(true);
        //readyButton.Select();
        //menuPanel.SetActive(false);

        //ResetTime();
    }

    public void LeftArrow()
    {
        if (!inputEnable) return;
        currentShirtIndex--;
        if (currentShirtIndex == -1)
        {
            currentShirtIndex = shirts.Length - 1;
        }
        factory.ChangePlayerColor(ref player, beards[playerIndex], shirts[currentShirtIndex]);
        StartCoroutine(SelectEffect(player));

        ResetTime();
    }

    public void RightArrow()
    {
        if (!inputEnable) return;
        currentShirtIndex++;
        currentShirtIndex %= shirts.Length;
        factory.ChangePlayerColor(ref player, beards[playerIndex], shirts[currentShirtIndex]);
        StartCoroutine(SelectEffect(player));

        ResetTime();
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

        Debug.Log("ReadyPlayer!");
        PlayerConfigurationManager.Instance.SetPlayerColor(currentShirtIndex, playerIndex, playerIndex);
        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);

        readyPlayer = true;
        //readyButton.gameObject.SetActive(false);
    }

    private void ResetTime()
    {
        ignoreInputTime = Time.time + ignoreInputTime;
        inputEnable = false;
    }
}
