using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IFlow, IUsable
{
    private Player player;
    private float sapAmount = 0.0f;
    private float maxSapAmount = 75.0f;
    private float quantityForCan = 10.0f;

    private bool canProducing = false;
    private float timeToProduce = 5.0f;
    private float remainingTime = 0.0f; 

    public void Initialize() {
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
    }

    public void Refresh() {
        if (sapAmount >= quantityForCan && !canProducing) { // If enough sap AND can is not already producing -> start producing a new one
            canProducing = true;
            remainingTime = timeToProduce;
        } else if (canProducing) { // If can is already producing...
            if (remainingTime > 0) { // If there is time remaining before producing the can -> decrease remaining time
                remainingTime -= Time.deltaTime; // QUESTION : how to make it stop decreasing during pause?
            } else { // If time is up -> produce a can
                sapAmount -= quantityForCan;
                canProducing = false;
                // ADD A CAN TO THE PLAYER
            }
        }
    }

    public void AddSap(float amount) {
        sapAmount = Mathf.Clamp(sapAmount + amount, 0, maxSapAmount);
    }

    public void Use(Player player) {

    }
}
