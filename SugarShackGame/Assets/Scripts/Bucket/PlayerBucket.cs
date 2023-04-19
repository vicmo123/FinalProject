using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBucket : MonoBehaviour, IUsable, IFlow
{
    public float sapAmount = 0.0f;
    private float maxSapAmount = 30.0f;


    public void Initialize() {
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
    }

    public void Refresh() {
    }

    public void Use(Player _player) {

    }

    public float AddSap(float amount) {
        float amountAdded = amount;
        if (amount + sapAmount > maxSapAmount) {
            amountAdded = maxSapAmount - sapAmount;
            sapAmount = maxSapAmount;
        }
        else {
            sapAmount += amount;
        }

        return amountAdded;
    }

    public void RemoveSap(float amount) {
        sapAmount = Mathf.Clamp(sapAmount - amount, 0, maxSapAmount);
    }
}
