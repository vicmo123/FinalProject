using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class SelectionPlayerCanvas : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject readyMenu;
    public GameObject selectionMenu;
    public PlayerInputManager playerInputManager;

    private PlayerInput input;
    private CustomInputHandler inputHandler;
    private float ignoreInputTime = 0.2f;
    private float startInputTime = 0.5f;
    bool setup = false;
    bool ready = false;
    bool spawnPlayer = false;

    private void Start()
    {
        ignoreInputTime = Time.time + startInputTime;        
    }

    // Update is called once per frame
    void Update()
    {
       
      if(Time.time > ignoreInputTime)
        {
            if (!spawnPlayer)
            {
                //playerInputManager.onPlayerJoined += Instance_onPlayerJoined; ;
                //InputSystem.onAnyButtonPress.CallOnce(any => WaitForSpawn());
            }
            else if (setup && !ready)
            {
                bool selectLeft = inputHandler.SelectLeft;
                bool selectright = inputHandler.SelectRight;
                Vector2 move = new Vector3(inputHandler.Move.x, 0.0f, inputHandler.Move.y).normalized;
                if (move == Vector2.left || selectLeft)
                    ClickArrow(leftArrow.transform);
                if (move == Vector2.right || selectright)
                    ClickArrow(rightArrow.transform);


                bool submit = inputHandler.Jump;
                if (submit)
                {
                    SubmitReady();
                }

            }
        }
    }

    public void PlayerJoined()
    {
        Debug.Log("Player joined in SelectionPlayerCanvas");
        spawnPlayer = true;
        ignoreInputTime = Time.time + startInputTime;
    }

    private void WaitForSpawn()
    {
        Debug.Log("Wait for spawn");
        spawnPlayer = true;      
        ignoreInputTime = Time.time + startInputTime;
    }

    public void ClickArrow(Transform arrow)
    {
        StartCoroutine(SelectEffect(arrow));
    }

    public void SetLinks(PlayerInput pi, CustomInputHandler _inputHandler)
    {
        input = pi;
        inputHandler = _inputHandler;
        setup = true;
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    public IEnumerator SelectEffect(Transform arrow)
    {
        float delta = 1f;
        while (delta < 1.2f)
        {
            delta += Time.deltaTime;
            arrow.localScale = new Vector3(delta, delta, delta);
            yield return null;
        }
    }

    private void SubmitReady()
    {
        ready = true;
        readyMenu.gameObject.SetActive(true);
        selectionMenu.gameObject.SetActive(false);
    }
}
