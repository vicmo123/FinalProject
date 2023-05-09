using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : ThrowableAbility
{
    public float attractionDistance = 20f;
    private float timeBeforeAddingPoints = 1f;
    private System.Action OnDeactivate = () => { };

    public override void Initialize()
    {
        base.Initialize();
        timer.OnTimeIsUpLogic += () =>
        {
            OnDeactivate.Invoke();
        };
    }
    public override void InitAbility(Ability _stats, Player _player)
    {
        base.InitAbility(_stats, _player);
        MakeEffect();
    }

    public override void MakeEffect()
    {
        var animalInScene = GameObject.FindGameObjectsWithTag("Animal");
        foreach (var animal in animalInScene)
        {
            float toAppleDistance = (transform.position - animal.transform.position).magnitude;
            if(toAppleDistance <= attractionDistance)
            {
                // Bonus
                if (Time.time > timeBeforeAddingPoints)
                {
                    thrower.GetComponent<Player>().playerScore.AddBonus(PlayerScore.Bonus.APPLE);
                    timeBeforeAddingPoints = Time.time + timeBeforeAddingPoints;
                }

                Animal animalComponent = animal.GetComponent<Animal>();
                animalComponent.chaseTarget = gameObject;
                OnDeactivate += () =>
                {
                    animalComponent.chaseTarget = null;
                };
            }
        }
    }

    public override void OnCollisionLogic(Collision collision)
    {
        MakeEffect();
    }
}
