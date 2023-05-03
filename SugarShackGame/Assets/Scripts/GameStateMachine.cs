using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FSM;
public class GameStateMachine
{
    private MainEntry mainEntry;

    public GameStateMachine(MainEntry mainEntry)
    {
        this.mainEntry = mainEntry;
    }

    public string CurrentState { get; set; }

    //States
    public const string Gameplay = "Gameplay";
    public const string Pause = "Pause";
    public const string EndGame = "CalculateScore";

    //OnEnter
    public Action OnGameplayEnter = () => { };
    public Action OnPauseEnter = () => { };
    public Action OnEndGameEnter = () => { };

    //OnLogic
    public Action OnGameplayLogic = () => { };
    public Action OnPauseLogic = () => { };
    public Action OnEndGameLogic = () => { };

    //OnExit
    public Action OnGameplayExit = () => { };
    public Action OnPauseExit = () => { };
    public Action OnEndGameExit = () => { };

    //Transitions
    public Func<bool> IsPaused = () => { return false; };
    public Func<bool> IsPauseFinished = () => { return false; };
    public Func<bool> IsGameOver = () => { return false; };
    public Func<bool> IsExit = () => { return false; };

    public StateMachine stateMachine;

    public void InitStateMachine()
    {
        stateMachine = new StateMachine();

        SetStates();
        SetTransitions();

        stateMachine.SetStartState(Gameplay);
        stateMachine.Init();
    }

    public void UpdateStateMachine()
    {
        if (mainEntry)
        {
            stateMachine.OnLogic();
        }

        //Debug.Log(CurrentState);
    }

    private void SetStates()
    {
        stateMachine.AddState(Gameplay, new State(
                onEnter: _ => OnGameplayEnter.Invoke(),
                onLogic: _ => OnGameplayLogic.Invoke(),
                onExit: _ => OnGameplayExit.Invoke()
            )
        );
        stateMachine.AddState(Pause, new State(
         onEnter: _ => OnPauseEnter.Invoke(),
         onLogic: _ => OnPauseLogic.Invoke(),
         onExit: _ => OnPauseExit.Invoke()
     )
        );
        stateMachine.AddState(EndGame, new State(
                onEnter: _ => OnEndGameEnter.Invoke(),
                onLogic: _ => OnEndGameLogic.Invoke(),
                onExit: _ => OnEndGameExit.Invoke()
            )
        );
       
    }

    private void SetTransitions()
    {
        stateMachine.AddTransition(Gameplay, Pause, _ => IsPaused.Invoke());
        stateMachine.AddTransition(Pause, Gameplay, _ => IsPauseFinished.Invoke());
        stateMachine.AddTransition(Pause, EndGame, _ => IsExit.Invoke());
        stateMachine.AddTransition(Gameplay, EndGame, _ => IsGameOver.Invoke());
    }
}
