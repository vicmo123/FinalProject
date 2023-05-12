using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainEntry : MonoBehaviour
{
    GameStateMachine stateMachine;
    private PlayerInputManager playerInputManager;

    [HideInInspector]
    public bool isPaused = false;
    [HideInInspector]
    public bool isPauseFinished = false;
    [HideInInspector]
    public bool isGameOver = false;
    [HideInInspector]
    public bool isExit = false;
    [HideInInspector]
    public float gameDuration;
    [HideInInspector]
    public float timeLeft;

    private bool gameStarted = false;
    private bool exiting = false;

    private void Awake()
    {
        GameManager.Instance.GameManagerSetup();
        GameManager.Instance.PreInitialize();
        GameManager.Instance.Initialize();
        //Setup Game Data : 
        gameDuration = UIManager.Instance.gameDuration;
        //  Assign player with UI viewport
    }

    void Start()
    {
        playerInputManager = PlayerConfigurationManager.Instance.GetComponent<PlayerInputManager>();
        if (playerInputManager.playerCount == 2)
        {
            stateMachine = new GameStateMachine(this);
            stateMachine.InitStateMachine();
            SetDelgsForStateMachine();
            gameStarted = true;
        }
        //playerInputManager.onPlayerJoined += (input) =>
        //{
        //    stateMachine = new GameStateMachine(this);
        //    stateMachine.InitStateMachine();
        //    SetDelgsForStateMachine();
        //    gameStarted = true;
        //};
    }

    void Update()
    {

        //if(timeLeft <= 0)
        //{
        //    isGameOver = true;
        //}
        Debug.Log("MainEntry Refresh");
        if (gameStarted)
            stateMachine.UpdateStateMachine();
    }

    private void FixedUpdate()
    {

    }

    #region StateMachine Setup
    public void SetDelgsForStateMachine()
    {
        //OnEnter
        stateMachine.OnGameplayEnter += () => { OnGameplayEnter(); };
        stateMachine.OnPauseEnter += () => { OnPauseEnter(); };
        stateMachine.OnEndGameEnter += () => { OnEndGameEnter(); };

        //OnLogic
        stateMachine.OnGameplayLogic += () => { OnGameplayLogic(); };
        stateMachine.OnPauseLogic += () => { OnPauseLogic(); };
        stateMachine.OnEndGameLogic += () => { OnEndGameLogic(); };

        //OnExit
        stateMachine.OnGameplayExit += () => { OnGameplayExit(); };
        stateMachine.OnPauseExit += () => { OnPauseExit(); };
        stateMachine.OnEndGameExit += () => { OnEndGameExit(); };

        //Transitions
        stateMachine.IsPaused += () => { return IsPaused(); };
        stateMachine.IsPauseFinished += () => { return IsPauseFinished(); };
        stateMachine.IsGameOver += () => { return IsGameOver(); };
        stateMachine.IsExit += () => { return IsExit(); };
    }

    #region OnEnter
    public void OnGameplayEnter()
    {
        // Debug.Log("OnGameplayEnter");
    }

    public void OnPauseEnter()
    {
        Debug.Log("OnPauseEnter");
        isPauseFinished = false;
    }

    public void OnEndGameEnter()
    {
        Debug.Log("OnEndGameEnter");
        UIManager.Instance.GatherData();
    }


    #endregion

    #region OnLogic
    public void OnGameplayLogic()
    {
        //Debug.Log("OnGameplayLogic");

        stateMachine.CurrentState = GameStateMachine.Gameplay;
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //hihi
            int i = 0;
        }
        GameManager.Instance.Refresh();
        GameManager.Instance.PhysicsRefresh();
    }

    public void OnPauseLogic()
    {
        Debug.Log("OnPauseLogic");
        stateMachine.CurrentState = GameStateMachine.Pause;
        Time.timeScale = 0;
    }

    public void OnEndGameLogic()
    {
        Debug.Log("OnEndGameLogic");
        stateMachine.CurrentState = GameStateMachine.EndGame;
        if (!exiting)
        {
            //Give Data to the UIManager / Score Manager
            UIManager.Instance.LoadOneScene(ScenesNames.EndGame);
            //Clear Managers (lists)   
            exiting = true;
        }
    }


    #endregion


    #region OnExit
    public void OnGameplayExit()
    {
        //Debug.Log("OnGameplayExit");
    }

    public void OnPauseExit()
    {
        Debug.Log("OnPauseExit");
        Time.timeScale = 1;
    }

    public void OnEndGameExit()
    {
        //Debug.Log("OnEndGameExit");
    }


    #endregion


    #region Transitons
    public bool IsPaused()
    {
        if (isPaused)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsPauseFinished()
    {
        if (isPauseFinished)
            return true;
        else
            return false;
    }

    public bool IsGameOver()
    {
        if (isGameOver)
            return true;
        else
            return false;
    }

    public bool IsExit()
    {
        if (isExit)
            return true;
        else
            return false;
    }

    #endregion

    #endregion
}

