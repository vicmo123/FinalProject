using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : UnThrowableAbility
{
    public SoundListEnum hornSound;
    public float repulsionZoneRadius = 20f;

    public override void Initialize()
    {
        base.Initialize();
        //Todo change the sound for the horn sound
        hornSound = SoundListEnum.Bell;
    }
    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        MakeEffect();
    }

    private void PlayTheHorn()
    {
        SoundManager.Play?.Invoke(hornSound);
    }

    public override void MakeEffect()
    {
        PlayTheHorn();

        var animalInScene = GameObject.FindGameObjectsWithTag("Animal");
        foreach (var animal in animalInScene)
        {
            float toAppleDistance = (transform.position - animal.transform.position).magnitude;
            if (toAppleDistance <= repulsionZoneRadius)
            {
                Animal animalComponent = animal.GetComponent<Animal>();
                animalComponent.chaseTarget = null;
                animalComponent.isScared = true;
            }
        }
    }

    
}
