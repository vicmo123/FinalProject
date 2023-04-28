using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBucket : MonoBehaviour, IUsable, IFlow
{
    public GameObject bucketLid;
    public float sapAmount = 0.0f;
    private float maxSapAmount = 30.0f;


    public void Initialize() {
        bucketLid.SetActive(false);
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

    public IEnumerator NoSpill(float duration)
    {
        bucketLid.SetActive(true);
        //Todo no spill
        yield return new WaitForSeconds(duration);
        bucketLid.SetActive(false);
    }
}
