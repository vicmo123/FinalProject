using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour, IFlow, IUsable
{
    Player player;

    [HideInInspector] public float sapAmount = 0.0f;
    private float maxSapAmount = 20.0f;
    private float sapGainSpeed = 1.0f;

    private float remainingTimeToClaim = 0.0f;
    private float timeToClaim = 3.0f;
    private float timeToSteal = 6.0f;
    private float lastUsedTime = 0.0f;

    private float cooldown = 1.0f;
    private float endOfCooldown = 0.0f;

    public void Initialize() {

        int mapleType = Int32.Parse(transform.parent.name.Substring(7, 1));
        transform.localPosition = BucketManager.Instance.bucketPositionDic[mapleType];
    }

    public void PhysicsRefresh() {
    }

    public void PreInitialize() {
    }

    public void Refresh() {
        Sap();
    }

    public void Use(Player _player) {
        if (!player) {
            // If there is no owner yet, allows them to claim the bucket
            Claim(_player);
        }
        else if (_player == player) {
            if (Time.time >= endOfCooldown) {
                // If the playing using the bucket is the owner, gets sap
                sapAmount -= _player.playerBucket.AddSap(sapAmount);
                endOfCooldown = Time.time + cooldown;
            }
        }
        else {
            // If it is not the owner, allows them to claim the bucket
            Claim(_player);
        }
    }

    private void Claim(Player _player) {
        if (Time.time - Time.deltaTime == lastUsedTime) { // If the player is already claiming
            if (remainingTimeToClaim > 0.0f)
                remainingTimeToClaim -= Time.deltaTime;
            else {
                player = _player;
                endOfCooldown = Time.time + cooldown;
            }
        } else { // If the player starts to claim
            if (!player)
                remainingTimeToClaim = timeToClaim;
            else
                remainingTimeToClaim = timeToSteal;
        }

        lastUsedTime = Time.time;
    }

    private void Sap() {
        sapAmount += sapGainSpeed * Time.deltaTime;
        if (sapAmount > maxSapAmount)
            sapAmount = maxSapAmount;
    }

    public bool CheckParent() {
        if (transform.parent)
            if (transform.parent.CompareTag("Maple"))
                return true;
            else return false;
        else
            return false;
    }
}
