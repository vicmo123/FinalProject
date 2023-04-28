using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FSM;

public class AnimalStateMachine
{
    private Animal animal;

    public AnimalStateMachine(Animal _animal)
    {
        animal = _animal;
    }

    public string CurrentState { get; set; }

    //States
    public const string Patrol = "Patrol";
    public const string Flee = "Flee";
    public const string Chase = "Chase";
    public const string SpecialAction = "SpecialAction";

    //OnEnter
    public Action OnPatrolEnter = () => { };
    public Action OnFleeEnter = () => { };
    public Action OnChaseEnter = () => { };
    public Action OnSpecialActionEnter = () => { };

    //OnLogic
    public Action OnPatrolLogic = () => { };
    public Action OnFleeLogic = () => { };
    public Action OnChaseLogic = () => { };
    public Action OnSpecialActionLogic = () => { };

    //OnExit
    public Action OnPatrolExit = () => { };
    public Action OnFleeExit = () => { };
    public Action OnChaseExit = () => { };
    public Action OnSpecialActionExit = () => { };

    //Transitions
    public Func<bool> IsPatrolFinished = () => { return false; };
    public Func<bool> IsFleeFinished = () => { return false; };
    public Func<bool> IsFleeTime = () => { return false; };
    public Func<bool> IsChaseFinished = () => { return false; };
    public Func<bool> IsSpecialActionFinished = () => { return false; };
    public Func<bool> IsSpecialActionTime = () => { return false; };

    public StateMachine stateMachine;

    public void InitStateMachine()
    {
        stateMachine = new StateMachine();

        SetStates();
        SetTransitions();

        stateMachine.SetStartState(Patrol);
        stateMachine.Init();
    }

    public void UpdateStateMachine()
    {
        if (animal.agent.enabled == true)
        {
            stateMachine.OnLogic();
        }

        //Debug.Log(CurrentState);
    }

    private void SetStates()
    {
        stateMachine.AddState(Patrol, new State(
                onEnter: _ => OnPatrolEnter.Invoke(),
                onLogic: _ => OnPatrolLogic.Invoke(),
                onExit: _ => OnPatrolExit.Invoke()
            )
        );
        stateMachine.AddState(Flee, new State(
                onEnter: _ => OnFleeEnter.Invoke(),
                onLogic: _ => OnFleeLogic.Invoke(),
                onExit: _ => OnFleeExit.Invoke()
            )
        );
        stateMachine.AddState(Chase, new State(
                onEnter: _ => OnChaseEnter.Invoke(),
                onLogic: _ => OnChaseLogic.Invoke(),
                onExit: _ => OnChaseExit.Invoke()
            )
        );
        stateMachine.AddState(SpecialAction, new State(
                onEnter: _ => OnSpecialActionEnter.Invoke(),
                onLogic: _ => OnSpecialActionLogic.Invoke(),
                onExit: _ => OnSpecialActionExit.Invoke()
            )
        );
    }

    private void SetTransitions()
    {
        stateMachine.AddTransition(Flee, Patrol, _ => IsFleeFinished.Invoke());
        stateMachine.AddTransition(Patrol, Chase, _ => IsPatrolFinished.Invoke());
        stateMachine.AddTransition(Chase, Patrol, _ => IsChaseFinished.Invoke());

        stateMachine.AddTransitionFromAny(new Transition("", SpecialAction, t => IsSpecialActionTime.Invoke()));
        stateMachine.AddTransitionFromAny(new Transition("", Flee, t => IsFleeTime.Invoke()));
    }
}
