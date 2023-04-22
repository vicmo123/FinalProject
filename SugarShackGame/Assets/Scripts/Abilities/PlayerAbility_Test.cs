using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility_Test : MonoBehaviour
{
    public Transform abilityStartPos;
    public Transform snowballStartPos;
    private Ability ability;
    private Slot slot1;

    private void Start()
    {
        slot1 = new Slot();
        slot1.PreInitialize();
    }

    public void UsePower()
    {
        ability = slot1.GetAbility();
        if (ability != null)
        {
            ability.SpawnAbility(this);
        }
    }
    private void Update()
    {
        slot1.Refresh();
    }

}
