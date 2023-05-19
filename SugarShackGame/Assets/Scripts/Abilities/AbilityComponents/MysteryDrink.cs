using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MysteryDrink : UnThrowableAbility
{
    private Action[] drinkEffects;
    public float fallOverForce = 5f;

    public float inversedControlsDuration = 5f;
    public float boostDuration = 7f;
    public float slowDownDuration = 5f;

    public override void MakeEffect()
    {
        base.MakeEffect();

        drinkEffects[Random.Range(0, drinkEffects.Length)].Invoke();
    }

    public override void InitAbility(Ability _stats, Player _player)
    {
        SetEffects(() => SlowDownEffect(),
                   () => BoostEffect(),
                   () => InversedControls(),
                   () => FallOverEffect()
        );

        base.InitAbility(_stats, _player);
        MakeEffect();
    }

    public void SlowDownEffect()
    {
        var playerController = player.gameObject.GetComponent<PlayerController>();
        playerController.SlowDownEffect(slowDownDuration);
    }

    public void BoostEffect()
    {
        var playerController = player.gameObject.GetComponent<PlayerController>();
        playerController.BoostEffect(boostDuration);
    }

    public void InversedControls()
    {
        var playerController = player.gameObject.GetComponent<PlayerController>();
        playerController.InversedControlsEffect(inversedControlsDuration);
    }

    public void FallOverEffect()
    {
        var ragdollComponenent = player.gameObject.GetComponent<Ragdoll>();
        ragdollComponenent.ragdollTriggerAllTrampoline(ragdollComponenent.transform.forward * fallOverForce);
    }

    private void SetEffects(params Action[] actionsToSet)
    {
        drinkEffects = new Action[actionsToSet.Length];

        for (int i = 0; i < actionsToSet.Length; i++)
        {
            drinkEffects[i] = actionsToSet[i];
        }
    }
}
