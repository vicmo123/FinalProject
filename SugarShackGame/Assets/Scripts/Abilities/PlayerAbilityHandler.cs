using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

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
    private readonly int indexNextToThrow = 2;

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
        TryUseAbilities();
    }

    private void FillSlots()
    {
        for (int i = 0; i < abilitySlots.Length; i++)
        {
            abilitySlots[i] = slotFactory.CreateRandomAbility();
            if (i == indexNextToThrow)
            {
                abilitySlots[i] = slotFactory.CreateAbility(AbilityType.SnowBall);
            }
            Debug.Log(abilitySlots[i]);
        }
    }

    private void TryUseAbilities()
    {
        if (inputHandler.Throw)
        {
            if (abilitySlots[indexNextToThrow].Activate(player))
            {
                var objAbility = AbilityObjectManager.Instance.AddObjectToCollection(abilitySlots[indexNextToThrow].type, player);
                objAbility.InitAbility(abilitySlots[indexNextToThrow], player);

                //Reset
                abilitySlots[indexNextToThrow] = slotFactory.CreateAbility(AbilityType.SnowBall);
            }
        }

        inputHandler.UseLeftPowerUp = TryUseAbility(inputHandler.UseLeftPowerUp, indexLeftSlot);
        inputHandler.UseRightPowerUp = TryUseAbility(inputHandler.UseRightPowerUp, indexRightSlot);
    }

    public bool TryUseAbility(bool input, int slotIndex)
    {
        if (input)
        {
            if (abilitySlots[slotIndex].Activate(player))
            {
                input = false;
                if (abilitySlots[slotIndex].isThrowable)
                {
                    abilitySlots[indexNextToThrow] = abilitySlots[slotIndex];
                }
                else
                {
                    var objAbility = AbilityObjectManager.Instance.AddObjectToCollection(abilitySlots[slotIndex].type, player);
                    objAbility.InitAbility(abilitySlots[slotIndex], player);
                }
                Debug.Log(abilitySlots[slotIndex]);
                abilitySlots[slotIndex] = slotFactory.CreateRandomAbility();
            }
        }

        return input;
    }
}
