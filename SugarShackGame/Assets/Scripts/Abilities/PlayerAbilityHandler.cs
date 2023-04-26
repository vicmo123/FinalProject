using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityHandler : MonoBehaviour, IFlow
{
    //3rd slot for snowballs
    private int numberOfSlots = 3;
    [HideInInspector]
    public Ability[] abilitySlots;
    private AbilityFactory slotFactory;
    private Player player;

    private CustomInputHandler inputHandler;
    [HideInInspector]
    public readonly int indexLeftSlot = 0;
    [HideInInspector]
    public readonly int indexRightSlot = 1;
    private readonly int indexSnowBallSlot = 2;

    public void PreInitialize()
    {
        slotFactory = new AbilityFactory();
        abilitySlots = new Ability[numberOfSlots];

        inputHandler = GetComponent<CustomInputHandler>();
        player = GetComponent<Player>();
    }

    public void Initialize()
    {
        FillSlots();
    }

    public void Refresh()
    {
        ManageSlots();
    }

    public void PhysicsRefresh()
    {
        
    }

    private void ManageSlots()
    {
        TryUseAbility();
    }

    private void FillSlots()
    {
        for (int i = 0; i < abilitySlots.Length; i++)
        {
            abilitySlots[i] = slotFactory.CreateRandomAbility();
            if (i == indexSnowBallSlot)
            {
                abilitySlots[i] = slotFactory.CreateAbility(AbilityType.SnowBall);
            }
            Debug.Log(abilitySlots[i]);
        }
    }

    private void TryUseAbility()
    {
        if (inputHandler.Throw)
        {
            if (abilitySlots[indexSnowBallSlot].Activate(player))
            {
                var objAbility = AbilityObjectManager.Instance.AddObjectToCollection(AbilityType.SnowBall, player);
                objAbility.InitAbility(abilitySlots[indexSnowBallSlot], player);
            }
        }

        if (inputHandler.UseLeftPowerUp)
        {
            if (abilitySlots[indexLeftSlot].Activate(player))
            {
                inputHandler.UseLeftPowerUp = false;
                abilitySlots[indexLeftSlot] = slotFactory.CreateRandomAbility();

                var objAbility = AbilityObjectManager.Instance.AddObjectToCollection(abilitySlots[indexLeftSlot].type, player);
                objAbility.InitAbility(abilitySlots[indexLeftSlot], player);
            }
        }

        if (inputHandler.UseRightPowerUp)
        {
            if (abilitySlots[indexRightSlot].Activate(player))
            {
                inputHandler.UseRightPowerUp = false;
                abilitySlots[indexRightSlot] = slotFactory.CreateRandomAbility();

                var objAbility = AbilityObjectManager.Instance.AddObjectToCollection(abilitySlots[indexRightSlot].type, player);
                objAbility.InitAbility(abilitySlots[indexRightSlot], player);
            }
        }

        if (inputHandler.Pause)
        {
            Debug.Log("Pause");
        }

    }
}
