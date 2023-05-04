using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { Title, SettingUp, Game, Pause, GameOver }

public class MainEntry : MonoBehaviour
{
    GameStateMachine stateMachine;

    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isExit = false;

    private void Awake()
    {
        stateMachine = new GameStateMachine(this);
        stateMachine.InitStateMachine();
        SetDelgsForStateMachine();
        GameManager.Instance.GameManagerSetup();
        GameManager.Instance.PreInitialize();
    }

    void Start()
    {
        GameManager.Instance.Initialize();
    }

    void Update()
    {
        stateMachine.UpdateStateMachine();

        GameManager.Instance.Refresh();
    }

    private void FixedUpdate()
    {
        GameManager.Instance.PhysicsRefresh();
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
        //MainEntry calls PreInit IFlow
        //MainEntry calls Init IFlow
        //Setup Game Data : 
        //  Players colors, buckets colors
        //  Assign player with UI viewport
    }

    public void OnPauseEnter()
    {
        //Stop Updating IFlow
        //Disable game controls
        //Enable UI controls
        //Display UI : Pause View
    }

    public void OnEndGameEnter()
    {
        //Stop Updating IFlow
        //Disable game controls
    }

    
    #endregion

    #region OnLogic
    public void OnGameplayLogic()
    {
        stateMachine.CurrentState = GameStateMachine.Gameplay;
        //Refresh IFlow
    }

    public void OnPauseLogic()
    {
        stateMachine.CurrentState = GameStateMachine.Pause;
        //Wait until button pressed
        //if exit : IsExit = true;
        //if resume : IsPaused = false;
    }

    public void OnEndGameLogic()
    {
        stateMachine.CurrentState = GameStateMachine.EndGame;
        //Give Data to the UIManager / Score Manager
        //Clear Managers (lists)      

    }

    
    #endregion


    #region OnExit
    public void OnGameplayExit()
    {

    }

    public void OnPauseExit()
    {
        //disable Pause View
        //Disable UI controls
        //Enable Game Controls
    }

    public void OnEndGameExit()
    {
        //Enable UI controls
        //Load Next Scene
    }


    #endregion


    #region Transitons
    public bool IsPaused()
    {
        if (isPaused)
            return true;
        else
            return false;
    }

    public bool IsPauseFinished()
    {
        if (!isPaused)
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

