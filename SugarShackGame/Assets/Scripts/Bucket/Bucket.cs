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
        if (_player == player) {
            // If the playing using the bucket is the owner, gets sap
        } else {
            // If it is not the owner, allows them to claim the bucket
        }
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
