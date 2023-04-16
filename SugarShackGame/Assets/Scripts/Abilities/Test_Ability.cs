using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Ability : MonoBehaviour
{
    public Slot slot;
    private AbilityHolder ability1;
    private AbilityHolder ability2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ability1 = slot.GetCurrentAbility();
            if (ability1.GetState() == AbilityState.Ready)
            {
                ability1.IsActive = true;
                ability1.ability.Activate();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (ability2)
            {
                ability2.ability.Activate();
            }
        }

        slot.Refresh();
    }
}
