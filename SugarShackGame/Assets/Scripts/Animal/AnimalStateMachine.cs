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

    #region StateMachine
    public string CurrentState;

    //States
    public const string Patrol = "Patrol";
    public const string Flee = "Flee";
    public const string Chase = "Chase";
    public const string Attack = "Attack";
    public const string SpecialAction = "SpecialAction";
    public const string Ragdoll = "Ragdoll";

    //OnEnter
    public Action OnPatrolEnter = () => { };
    public Action OnFleeEnter = () => { };
    public Action OnChaseEnter = () => { };
    public Action OnAttackEnter = () => { };
    public Action OnSpecialActionEnter = () => { };
    public Action OnRagdollEnter = () => { };

    //OnLogic
    public Action OnPatrolLogic = () => { };
    public Action OnFleeLogic = () => { };
    public Action OnChaseLogic = () => { };
    public Action OnAttackLogic = () => { };
    public Action OnSpecialActionLogic = () => { };
    public Action OnRagdollLogic = () => { };

    //OnExit
    public Action OnPatrolExit = () => { };
    public Action OnFleeExit = () => { };
    public Action OnChaseExit = () => { };
    public Action OnAttackExit = () => { };
    public Action OnSpecialActionExit = () => { };
    public Action OnRagdollExit = () => { };

    //Transitions
    public Func<bool> IsPatrolFinished = () => { return false; };
    public Func<bool> IsFleeFinished = () => { return false; };
    public Func<bool> IsChaseFinished = () => { return false; };
    public Func<bool> IsAttackFinished = () => { return false; };
    public Func<bool> IsSpecialActionFinished = () => { return false; };
    public Func<bool> IsRagdollFinished = () => { return false; };
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
        stateMachine.AddState(Attack, new State(
            onEnter: _ => OnAttackEnter.Invoke(),
            onLogic: _ => OnAttackLogic.Invoke(),
            onExit: _ => OnAttackExit.Invoke()
            )
        );
        stateMachine.AddState(SpecialAction, new State(
            onEnter: _ => OnSpecialActionEnter.Invoke(),
            onLogic: _ => OnSpecialActionLogic.Invoke(),
            onExit: _ => OnSpecialActionExit.Invoke()
            )
        );
        stateMachine.AddState(Ragdoll, new State(
            onEnter: _ => OnRagdollEnter.Invoke(),
            onLogic: _ => OnRagdollLogic.Invoke(),
            onExit: _ => OnRagdollExit.Invoke()
            )
         );
    }

    private void SetTransitions()
    {
        stateMachine.AddTransition(Patrol, Flee, _ => IsPatrolFinished.Invoke());
        stateMachine.AddTransition(Flee, Patrol, _ => IsFleeFinished.Invoke());
        stateMachine.AddTransition(Patrol, Chase, _ => animal.chaseTarget != null);
        stateMachine.AddTransition(Chase, Attack, _ => false);
        stateMachine.AddTransition(Chase, Patrol, _ => IsChaseFinished.Invoke());

        stateMachine.AddTransitionFromAny(new Transition("", Ragdoll, t => animal.stats.hp <= 0));
        stateMachine.AddTransitionFromAny(new Transition("", SpecialAction, t => IsSpecialActionTime.Invoke()));
    }
    #endregion
}