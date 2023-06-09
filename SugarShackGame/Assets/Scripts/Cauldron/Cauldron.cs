using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IFlow, IUsable
{
    [HideInInspector] public Player player;
    [SerializeField] private float sapAmount = 0.0f;
    private float maxSapAmount = 75.0f;
    private float quantityForCan = 10.0f;

    private bool canProducing = false;
    private float timeToProduce = 5.0f;
    private float remainingTime = 0.0f;

    private Highlight highlight;

    public void Initialize() {
        highlight.Initialize();
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
        highlight = GetComponent<Highlight>();
        highlight.PreInitialize();
    }

    public void Refresh() {
        BoilingSap();

        highlight.Refresh();

        //Debug.Log(player.playerScore.Calculate());
    }

    private void BoilingSap() {
        if (sapAmount >= quantityForCan && !canProducing) { // If enough sap AND can is not already producing -> start producing a new one
            canProducing = true;
            remainingTime = timeToProduce;
        }
        else if (canProducing) { // If can is already producing...
            if (remainingTime > 0) { // If there is time remaining before producing the can -> decrease remaining time
                remainingTime -= Time.deltaTime;
            }
            else { // If time is up -> produce a can
                sapAmount -= quantityForCan;
                canProducing = false;
                player.syrupCanManager.AddCan();
            }
        }
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

        if (amountAdded > 0)
            SoundManager.Play(SoundListEnum.pouringwater);

        return amountAdded;
    }
    public void Looked(Player _player) {
        if (_player != player)
            return;

        highlight.ToggleHighlight(true);
    }

    public void Use(Player _player) {
        if (_player != player)
            return;

        _player.playerBucket.RemoveSap(AddSap(_player.playerBucket.sapAmount));

    }
}
