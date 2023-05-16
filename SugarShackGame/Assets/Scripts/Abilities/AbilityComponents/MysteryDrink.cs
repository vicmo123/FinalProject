using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MysteryDrink : UnThrowableAbility
{
    private Action[] drinkEffects;
    public float effectDuration;

    public override void MakeEffect()
    {
        base.MakeEffect();

        drinkEffects[Random.Range(0, drinkEffects.Length)].Invoke();
    }

    public override void InitAbility(Ability _stats, Player _player)
    {
        drinkEffects = new Action[3];
        drinkEffects[0] = () => { SlowDownEffect(); };
        drinkEffects[1] = () => { BoostEffect(); };
        drinkEffects[2] = () => { InversedControls(); };

        base.InitAbility(_stats, _player);
        MakeEffect();
    }

    public void SlowDownEffect()
    {

    }

    public void BoostEffect()
    {

    }

    public void InversedControls()
    {

    }
}
